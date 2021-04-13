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
using Restaurante.Dominio.Enum;

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

        public async Task<ComandaModel> Obter(int comandaId)
        {
            var comanda = await _context.Comanda
                        .Where(c => c.ComandaId == comandaId && !c.Paga)
                        .Include(c => c.Pedidos)
                        .ThenInclude(c => c.Produto)
                        .ThenInclude(c => c.TipoProduto)
                        .Select(c => new
                        {
                            ComandaId = c.ComandaId,
                            MesaId = c.MesaId,
                            DataHoraEntrada = c.DataHoraEntrada,
                            QuantidadeClientes = c.QuantidadeClientes,
                            Pedidos = c.Pedidos,
                            Valor = c.Valor,
                            Paga = c.Paga
                        })
                        .OrderBy(c => c.ComandaId)
                        .LastOrDefaultAsync();

            _ = comanda ?? throw new Exception("A comanda solicitada não existe ou já foi encerrada");

            // Cria uma model de Comanda sem a listagem de pedidos
            var model = new ComandaModel()
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
                    Quantidade = p.Quantidade,
                    DataHoraRealizacao = p.DataHoraRealizacao,
                    StatusEnum = p.StatusEnum,
                    Produto = new ProdutoModel()
                    {
                        ProdutoId = p.ProdutoId,
                        Nome = p.Produto.Nome,
                        Valor = p.Produto.Valor,
                        TipoProduto = p.Produto.TipoProduto.Descricao,
                        QuantidadePermitida = p.Produto.QuantidadePermitida
                    }
                })
                .ToList();

            return model;
        }

        public async Task<int> Retomar(int mesaId)
        {
            var comanda = await _context.Comanda
                            .Where(c => c.MesaId == mesaId)
                            .OrderBy(c => c.ComandaId)
                            .LastOrDefaultAsync();

            _ = comanda ?? throw new Exception("A mesa solicitada não tem comanda em aberto");

            if (comanda.Paga)
                throw new Exception("A comanda referente a mesa solicitada ja foi encerrada");

            var comandaId = comanda.ComandaId;

            return comandaId;
        }

        public async Task<int> Registrar(Models.FormularioModel model)
        {
            model.Validar();

            if (!_context.Mesa.Any(m => m.MesaId == model.MesaId && !m.Ocupada && m.Capacidade >= model.QuantidadeCliente))
                throw new Exception("A mesa solicitada nao existe, ja esta ocupada ou nao suporta esta quantidade de pessoas");

            // Colocar funcionalidade dentro de uma transaction
            try
            {
                // Ocupando mesa
                await _mesaService.AtualizarStatus(model.MesaId, MesaEnum.Ocupar);

            } catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw new Exception("A mesa solicitada ja esta ocupada");
            }

            var comanda = new Dominio.Comanda
            {
                MesaId = model.MesaId,
                DataHoraEntrada = DateTime.Now,
                DataHoraSaida = null,
                Valor = model.QuantidadeCliente * _mesaService.ValorRodizio,
                Paga = false,
                QuantidadeClientes = model.QuantidadeCliente
            };

            _context.Comanda.Add(comanda);

            await _context.SaveChangesAsync();

            return comanda.ComandaId;
        }

        public async Task<int> Encerrar(int comandaId, EncerrarModel model)
        {
            var comanda = await _context.Comanda
                            .Where(c => c.ComandaId == comandaId && !c.Paga)
                            .Include(c => c.Pedidos)
                            .ThenInclude(c => c.Produto)
                            .FirstOrDefaultAsync();

            _ = comanda ?? throw new Exception("A comanda solicitada não foi encontrada ou ja foi encerrada");

            // Colocar funcionalidade dentro de uma transaction
            try
            {
                // Desocupando mesa
                await _mesaService.AtualizarStatus(comanda.MesaId, MesaEnum.Desocupar);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw new Exception("A comanda está em aberto mas a mesa relacionada já foi desocupada");
            }

            if (model.PorcentagemGarcom)
                comanda.Valor *= 1.1;

            // Atualizando: 'Em andamento' para 'Entregue'
            comanda.Pedidos
                .Where(p => p.StatusEnum == StatusEnum.EmAndamento)
                .ToList()
                .ForEach(p => p.StatusEnum = StatusEnum.Entregue);

            comanda.Paga = true;
            comanda.DataHoraSaida = DateTime.Now;

            await _context.SaveChangesAsync();

            return comandaId;
        }
    }
}
