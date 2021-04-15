using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Restaurante.Dominio.Enum;
using Restaurante.Repositorio.Contexto;
using Restaurante.Repositorio.Services.Pedido.Models;
using Restaurante.Repositorio.Services.Produto.Models;

namespace Restaurante.Repositorio.Services.Pedido
{
    public class PedidoService
    {
        private readonly RestauranteContexto _context;
        public PedidoService(RestauranteContexto context) => _context = context;

        public async Task<ListarModel> Obter(int pedidoId)
        {
            var pedido = await _context.Pedido
                        .Where(p => p.PedidoId == pedidoId)
                        .Include(p => p.Produto)
                        .ThenInclude(p => p.TipoProduto)
                        .Select(p => new ListarModel()
                        {
                            PedidoId = p.PedidoId,
                            ComandaId = p.ComandaId,
                            Quantidade = p.Quantidade,
                            DataHoraRealizacao = p.DataHoraRealizacao,
                            DataHoraEntrega = p.DataHoraEntrega,
                            DataHoraCancelamento = p.DataHoraCancelamento,
                            StatusEnum = p.StatusEnum,
                            Produto = new ProdutoModel()
                            {
                                ProdutoId = p.ProdutoId,
                                Nome = p.Produto.Nome,
                                Valor = p.Produto.Valor,
                                TipoProduto = p.Produto.TipoProduto.Descricao,
                                QuantidadePermitida = p.Produto.QuantidadePermitida
                            }
                        }).FirstOrDefaultAsync();

            _ = pedido ?? throw new Exception("O pedido solicitado nao existe");

            return pedido;
        }

        public async Task<ICollection<ListarModel>> BuscarPorComanda(int comandaId)
        {
            var comanda = await _context.Comanda
                        .Where(c => c.ComandaId == comandaId && !c.Paga)
                        .Include(c => c.Pedidos)
                        .ThenInclude(c => c.Produto)
                        .ThenInclude(c => c.TipoProduto)
                        .FirstOrDefaultAsync();

            _ = comanda ?? throw new Exception("A comanda solicitada não existe ou já foi encerrada");

            var pedidos = comanda.Pedidos
                        .Select(p => new ListarModel
                        {
                            ComandaId = p.ComandaId,
                            PedidoId = p.PedidoId,
                            Quantidade = p.Quantidade,
                            StatusEnum = p.StatusEnum,
                            DataHoraRealizacao = p.DataHoraRealizacao,
                            DataHoraEntrega = p.DataHoraEntrega,
                            DataHoraCancelamento = p.DataHoraCancelamento,
                            Produto = new ProdutoModel()
                            {
                                ProdutoId = p.ProdutoId,
                                Nome = p.Produto.Nome,
                                Valor = p.Produto.Valor,
                                TipoProduto = p.Produto.TipoProduto.Descricao,
                                QuantidadePermitida = p.Produto.QuantidadePermitida
                            }
                        })
                        .OrderByDescending(p => p.PedidoId)
                        .ToList();

            return pedidos;
        }

        public async Task<ListarModel> Registrar(FormularioModel model)
        {
            model.Validar();

            var comanda = await _context.Comanda
                        .Where(c => c.ComandaId == model.ComandaId && !c.Paga)
                        .FirstOrDefaultAsync();

            _ = comanda ?? throw new Exception("A comanda solicitada não existe ou já foi encerrada");

            var produto = await _context.Produto
                        .Where(p => p.ProdutoId == model.ProdutoId)
                        .Include(p => p.TipoProduto)
                        .FirstOrDefaultAsync();

            _ = produto ?? throw new Exception("O produto solicitado não existe");

            // Quantidade solicitada maior que quantidade permitida)
            if (model.Quantidade > produto.QuantidadePermitida && produto.QuantidadePermitida != 0)
                throw new Exception("Quantidade solicitada não é permitida");

            // Se o produto não estiver incluso no rodizio, este pedido será somado ao valor total da comanda
            if (produto.Valor > 0)
                comanda.Valor += model.Quantidade * produto.Valor;

            var pedido = new Dominio.Pedido()
            {
                ProdutoId = model.ProdutoId,
                ComandaId = model.ComandaId,
                Quantidade = model.Quantidade,
                DataHoraRealizacao = DateTime.Now,
                StatusEnum = StatusEnum.EmAndamento
            };

            _context.Pedido.Add(pedido);

            await _context.SaveChangesAsync();

            var pedidoModel = new ListarModel()
            {
                PedidoId = pedido.PedidoId,
                ComandaId = pedido.ComandaId,
                Quantidade = pedido.Quantidade,
                DataHoraRealizacao = pedido.DataHoraRealizacao,
                DataHoraEntrega = pedido.DataHoraEntrega,
                DataHoraCancelamento = pedido.DataHoraCancelamento,
                StatusEnum = pedido.StatusEnum,
                Produto = new ProdutoModel()
                {
                    ProdutoId = pedido.Produto.ProdutoId,
                    Nome = pedido.Produto.Nome,
                    Valor = pedido.Produto.Valor,
                    QuantidadePermitida = pedido.Produto.QuantidadePermitida,
                    TipoProduto = pedido.Produto.TipoProduto.Descricao,
                },
                NovoValorComanda = pedido.Comanda.Valor
            };

            return pedidoModel;
        }

        public async Task<ListarModel> Alterar(int pedidoId, AlterarModel model)
        {
            model.Validar();

            var pedido = await _context.Pedido
                        .Include(p => p.Comanda)
                        .Include(p => p.Produto)
                        .ThenInclude(p => p.TipoProduto)
                        .Where(p => p.ComandaId == model.ComandaId && !p.Comanda.Paga && p.PedidoId == pedidoId && p.StatusEnum == StatusEnum.EmAndamento)
                        .FirstOrDefaultAsync();

            _ = pedido ?? throw new Exception("O pedido solicitado nao existe ou nao pode mais ser alterado");

            if (model.NovaQuantidade > pedido.Produto.QuantidadePermitida && pedido.Produto.QuantidadePermitida != 0)
                throw new Exception("A nova quantidade solicitada nao e permitida");

            // Atualizar valor da comanda se o produto tiver valor
            if (pedido.Produto.Valor > 0)
                pedido.Comanda.Valor += (model.NovaQuantidade - pedido.Quantidade) * pedido.Produto.Valor;

            pedido.Quantidade = model.NovaQuantidade;

            await _context.SaveChangesAsync();

            var pedidoAtualizado = new ListarModel()
            {
                PedidoId = pedido.PedidoId,
                ComandaId = pedido.ComandaId,
                Quantidade = pedido.Quantidade,
                DataHoraRealizacao = pedido.DataHoraRealizacao,
                DataHoraEntrega = pedido.DataHoraEntrega,
                DataHoraCancelamento = pedido.DataHoraCancelamento,
                StatusEnum = pedido.StatusEnum,
                Produto = new ProdutoModel
                {
                    ProdutoId = pedido.Produto.ProdutoId,
                    Nome = pedido.Produto.Nome,
                    QuantidadePermitida = pedido.Produto.QuantidadePermitida,
                    Valor = pedido.Produto.Valor,
                    TipoProduto = pedido.Produto.TipoProduto.Descricao
                },
                NovoValorComanda = pedido.Comanda.Valor
            };

            return pedidoAtualizado;
        }

        public async Task<ListarModel> Cancelar(int pedidoId)
        {
            var pedido = await _context.Pedido
                        .Include(p => p.Comanda)
                        .Include(p => p.Produto)
                        .ThenInclude(p => p.TipoProduto)
                        .Where(p =>
                            p.PedidoId == pedidoId &&
                            !p.Comanda.Paga &&
                            p.StatusEnum == StatusEnum.EmAndamento // Somente pedidos em andamento podem ser cancelados
                        )
                        .FirstOrDefaultAsync();

            _ = pedido ?? throw new Exception("O pedido solicitado nao existe ou nao pode mais ser cancelado");

            // Se o produto não estiver incluso no rodizio, o valor do pedido será subtraido do valor total da comanda
            if (pedido.Produto.Valor > 0)
                pedido.Comanda.Valor -= pedido.Quantidade * pedido.Produto.Valor;

            pedido.StatusEnum = StatusEnum.Cancelado;
            pedido.DataHoraCancelamento = DateTime.Now;

            await _context.SaveChangesAsync();

            var pedidoAtualizado = new ListarModel()
            {
                PedidoId = pedido.PedidoId,
                ComandaId = pedido.ComandaId,
                Quantidade = pedido.Quantidade,
                DataHoraRealizacao = pedido.DataHoraRealizacao,
                DataHoraEntrega = pedido.DataHoraEntrega,
                DataHoraCancelamento = pedido.DataHoraCancelamento,
                StatusEnum = pedido.StatusEnum,
                Produto = new ProdutoModel
                {
                    ProdutoId = pedido.Produto.ProdutoId,
                    Nome = pedido.Produto.Nome,
                    QuantidadePermitida = pedido.Produto.QuantidadePermitida,
                    Valor = pedido.Produto.Valor,
                    TipoProduto = pedido.Produto.TipoProduto.Descricao
                },
                NovoValorComanda = pedido.Comanda.Valor
            };

            return pedidoAtualizado;
        }

    }
}
