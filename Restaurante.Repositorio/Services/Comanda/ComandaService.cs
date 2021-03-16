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
    public class ComandaService : IComandaService
    {
        private readonly RestauranteContexto _context;
        private readonly MesaService _mesaService;
        public ComandaService(RestauranteContexto context) => _context = context;
        public ComandaService(MesaService mesaService) => _mesaService = mesaService;

        public async Task Registrar(FormularioModel model)
        {
            if (!_context.Mesa.Any(m => m.MesaId == model.MesaId && !m.Ocupada && m.Capacidade >= model.QuantidadeCliente))
                throw new Exception("A mesa solicitada nao existe, ja esta ocupada ou nao suporta esta quantidade de pessoas");

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

        public async Task Encerrar(int comandaId, bool porcentagemGarcom = false)
        {
            var comanda = _context.Comanda
                            .Where(c => c.ComandaId == comandaId && !c.Paga)
                            .Include(c => c.Pedidos)
                            .ThenInclude(c => c.Produto)
                            .FirstOrDefault();

            _ = comanda ?? throw new Exception("A comanda solicitada não foi encontrada ou ja foi encerrada");

            if (RecalcularValorTotal(comanda.Valor, comanda.QuantidadeClientes, comanda.Pedidos))
                throw new Exception("O cálculo completo retornou um valor diferente");

            if (porcentagemGarcom)
                comanda.Valor *= 1.1;

            comanda.Paga = true;
            comanda.DataHoraSaida = DateTime.Now;

            await _context.SaveChangesAsync();
        }

        public bool RecalcularValorTotal(double valorInicial, int quantidadeClientes, ICollection<Dominio.Pedido> pedidos)
        {
            var valorFinal = _mesaService.ValorRodizio * quantidadeClientes;

            valorFinal += pedidos.Sum(p => p.Produto.Valor * p.Quantidade);

            return valorInicial == valorFinal;
        }

        public async Task<ResumidaModel> ObterResumida(int comandaId)
        {
            var comanda = await _context.Comanda
                        .Where(c => c.ComandaId == comandaId)
                        .Select(c => new ResumidaModel()
                        {
                            MesaId = c.MesaId,
                            DataHoraEntrada = c.DataHoraEntrada,
                            QuantidadeClientes = c.QuantidadeClientes,
                            Valor = c.Valor
                        })
                        .FirstOrDefaultAsync();

            _ = comanda ?? throw new Exception("A comanda solicitada nao existe");

            return comanda;
        }

        public async Task<CompletaModel> ObterCompleta(int comandaId)
        {
            var comanda = await _context.Comanda
                        .Where(c => c.ComandaId == comandaId)
                        .Include(c => c.Pedidos)
                        .ThenInclude(c => c.Status)
                        .Include(c => c.Pedidos)
                        .ThenInclude(c => c.Produto)
                        .ThenInclude(c => c.TipoProduto)
                        .Select(c => new
                        {
                            c.MesaId,
                            c.DataHoraEntrada,
                            c.QuantidadeClientes,
                            c.Pedidos,
                            c.Valor,
                            c.Paga
                        })
                        .FirstOrDefaultAsync();

            _ = comanda ?? throw new Exception("A comanda solicitada não existe");

            // Cria uma model de Comanda sem a listagem de pedidos
            var model = new CompletaModel()
            {
                MesaId = comanda.MesaId,
                DataHoraEntrada = comanda.DataHoraEntrada,
                QuantidadeClientes = comanda.QuantidadeClientes,
                Valor = comanda.Valor,
                Paga = comanda.Paga
            };

            // Cria uma listagem de PedidoModel dentro da model de Comanda
            model.Pedidos = comanda.Pedidos
                .Select(p => new PedidoModel()
                {
                    PedidoId = p.PedidoId,
                    Produto = new ProdutoListagemModel()
                    {
                        Nome = p.Produto.Nome,
                        Valor = p.Produto.Valor,
                        TipoProduto = p.Produto.TipoProduto.Descricao
                    },
                    Quantidade = p.Quantidade,
                    Status = p.Status.Descricao
                })
                .ToList();

            return model;
        }
    }
}
