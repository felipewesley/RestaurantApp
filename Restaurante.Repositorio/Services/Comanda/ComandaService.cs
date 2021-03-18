using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Restaurante.Repositorio.Contexto;
using Restaurante.Repositorio.Services.Mesa;
using Restaurante.Repositorio.Services.Comanda.Models;
using Restaurante.Repositorio.Services.Pedido.Models;
using Restaurante.Repositorio.Services.Produto.Models;
using Restaurante.Repositorio.Enum;
using System.Collections.Generic;

namespace Restaurante.Repositorio.Services.Comanda
{
    public class ComandaService
    {
        private readonly RestauranteContexto _context;
        private readonly MesaService _mesaService;
        
        public ComandaService(RestauranteContexto context, MesaService mesaService)
        {
            _context = context;
            _mesaService = mesaService;
        }

        public async Task Registrar(Models.FormularioModel model)
        {
            model.Validar();

            if (!_context.Mesa.Any(m => m.MesaId == model.MesaId && !m.Ocupada && m.Capacidade >= model.QuantidadeCliente))
                throw new Exception("A mesa solicitada nao existe, ja esta ocupada ou nao suporta esta quantidade de pessoas");

            // Ocupando mesa
            await _mesaService.AtualizarStatus(model.MesaId, MesaEnum.Ocupar);

            _context.Comanda.Add(new Dominio.Comanda
            {
                MesaId = model.MesaId,
                DataHoraEntrada = DateTime.Now,
                DataHoraSaida = null,
                Valor = model.QuantidadeCliente * _mesaService.ValorRodizio,
                Paga = false,
                QuantidadeClientes = model.QuantidadeCliente
            });

            await _context.SaveChangesAsync();
        }

        public async Task Alterar(AlterarModel model)
        {
            var mesa = await _mesaService.Obter(model.MesaId);

            if (model.QuantidadeClientes <= 0 || model.QuantidadeClientes > mesa.Capacidade)
                throw new Exception("Esta mesa nao suporta a quantidade de pessoas informada");
            
            var comanda = await _context.Comanda
                        .Where(c => c.ComandaId == model.ComandaId && !c.Paga)
                        .FirstOrDefaultAsync();

            _ = comanda ?? throw new Exception("A comanda solicitada nao existe ou ja foi finalizada");

            comanda.Valor += (model.QuantidadeClientes - comanda.QuantidadeClientes) * _mesaService.ValorRodizio;

            comanda.QuantidadeClientes = model.QuantidadeClientes;

            await _context.SaveChangesAsync();
        }

        public async Task Encerrar(int comandaId, bool porcentagemGarcom = false)
        {
            var comanda = await _context.Comanda
                            .Where(c => c.ComandaId == comandaId && !c.Paga)
                            .Include(c => c.Pedidos)
                            .ThenInclude(c => c.Produto)
                            .FirstOrDefaultAsync();

            _ = comanda ?? throw new Exception("A comanda solicitada não foi encontrada ou ja foi encerrada");

            if (!RecalcularValorTotal(comanda.Valor, comanda.QuantidadeClientes, comanda.Pedidos))
                throw new Exception("O cálculo completo retornou um valor diferente");

            if (porcentagemGarcom)
                comanda.Valor *= 1.1;

            // Atualizando: 'Em andamento' para 'Entregue'
            comanda.Pedidos
                .Where(p => p.StatusId == (int)StatusEnum.EmAndamento)
                .ToList()
                .ForEach(p => p.StatusId = (int)StatusEnum.Entregue);

            comanda.Paga = true;
            comanda.DataHoraSaida = DateTime.Now;

            // Desocupando mesa
            await _mesaService.AtualizarStatus(comanda.MesaId, MesaEnum.Desocupar);

            await _context.SaveChangesAsync();
        }

        public bool RecalcularValorTotal(double valorInicial, int quantidadeClientes, ICollection<Dominio.Pedido> pedidos)
        {
            var valorFinal = _mesaService.ValorRodizio * quantidadeClientes;

            valorFinal += pedidos
                .Where(p => p.StatusId != (int)StatusEnum.Cancelado && p.Produto.Valor > 0)
                .Sum(p => p.Produto.Valor * p.Quantidade);

            return valorInicial == valorFinal;
        }

        public async Task<ResumidaModel> ObterResumida(int mesaId)
        {
            var comanda = await _context.Comanda
                        .Where(c => c.MesaId == mesaId && !c.Paga)
                        .Select(c => new ResumidaModel()
                        {
                            ComandaId = c.ComandaId,
                            MesaId = c.MesaId,
                            DataHoraEntrada = c.DataHoraEntrada,
                            QuantidadeClientes = c.QuantidadeClientes,
                            Valor = c.Valor
                        })
                        .OrderBy(c => c.ComandaId)
                        .LastOrDefaultAsync();

            _ = comanda ?? throw new Exception("A mesa informada nao tem nenhuma comanda ativa");

            return comanda;
        }

        public async Task<CompletaModel> ObterCompleta(int mesaId)
        {
            var comanda = await _context.Comanda
                        .Where(c => c.MesaId == mesaId && !c.Paga)
                        .Include(c => c.Pedidos)
                        .ThenInclude(c => c.Status)
                        .Include(c => c.Pedidos)
                        .ThenInclude(c => c.Produto)
                        .ThenInclude(c => c.TipoProduto)
                        .Select(c => new
                        {
                            c.ComandaId,
                            c.MesaId,
                            c.DataHoraEntrada,
                            c.QuantidadeClientes,
                            c.Pedidos,
                            c.Valor,
                            c.Paga
                        })
                        .OrderBy(c => c.ComandaId)
                        .LastOrDefaultAsync();

            _ = comanda ?? throw new Exception("A mesa informada nao tem nenhuma comanda ativa");

            // Cria uma model de Comanda sem a listagem de pedidos
            var model = new CompletaModel()
            {
                ComandaId = comanda.ComandaId,
                MesaId = comanda.MesaId,
                DataHoraEntrada = comanda.DataHoraEntrada,
                QuantidadeClientes = comanda.QuantidadeClientes,
                Valor = comanda.Valor,
                Paga = comanda.Paga
            };

            // Cria uma listagem de PedidoModel dentro da model de Comanda
            model.Pedidos = comanda.Pedidos
                .Select(p => new ListarModel()
                {
                    ComandaId = p.ComandaId,
                    PedidoId = p.PedidoId,
                    Produto = new BuscaModel()
                    {
                        ProdutoId = p.ProdutoId,
                        Nome = p.Produto.Nome,
                        Valor = p.Produto.Valor,
                        TipoProduto = p.Produto.TipoProduto.Descricao,
                        QuantidadePermitida = p.Produto.QuantidadePermitida
                    },
                    Quantidade = p.Quantidade,
                    Status = p.Status.Descricao
                })
                .ToList();

            return model;
        }
    }
}
