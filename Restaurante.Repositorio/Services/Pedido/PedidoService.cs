using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Restaurante.Repositorio.Contexto;
using Restaurante.Repositorio.Enum;
using Restaurante.Repositorio.Services.Pedido.Models;
using Restaurante.Repositorio.Services.Produto.Models;

namespace Restaurante.Repositorio.Services.Pedido
{
    public class PedidoService
    {
        private readonly RestauranteContexto _context;
        public PedidoService(RestauranteContexto context) => _context = context;

        public async Task Registrar(FormularioModel model)
        {
            model.Validar();

            var comanda = await _context.Comanda
                        .Where(c => c.ComandaId == model.ComandaId && !c.Paga)
                        .FirstOrDefaultAsync();

            _ = comanda ?? throw new Exception("A comanda solicitada não existe ou ja foi encerrada");

            if (!_context.Produto.Any(p => 
                    p.ProdutoId == model.ProdutoId && 
                    (p.QuantidadePermitida == 0 || // Quantidade ilimititada
                    p.QuantidadePermitida >= model.Quantidade) // Quantidade solicitada menor/igual quantidade permitida
                )
            )
                throw new Exception("O produto solicitado nao existe ou a quantidade solicitada nao e permitida");

            // Se o produto não estiver incluso no rodizio, este pedido será somado ao valor total da comanda
            if (model.ProdutoValor > 0)
                comanda.Valor += model.Quantidade * model.ProdutoValor;

            _context.Pedido.Add(new Dominio.Pedido()
            {
                ComandaId = model.ComandaId,
                ProdutoId = model.ProdutoId,
                StatusId = (int)StatusEnum.EmAndamento,
                Quantidade = model.Quantidade,
            });

            await _context.SaveChangesAsync();
        }

        public async Task<ListarModel> Obter(int pedidoId)
        {
            var pedido = await _context.Pedido
                        .Where(p => p.PedidoId == pedidoId)
                        .Include(p => p.Status)
                        .Include(p => p.Produto)
                        .ThenInclude(p => p.TipoProduto)
                        .Select(p => new ListarModel()
                        {
                            ComandaId = p.ComandaId,
                            PedidoId = p.PedidoId,
                            Quantidade = p.Quantidade,
                            Status = p.Status.Descricao,
                            Produto = new BuscaModel()
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

        public async Task Cancelar(int pedidoId)
        {
            var pedido = await _context.Pedido
                        .Where(p =>
                            p.PedidoId == pedidoId &&
                            p.StatusId == (int)StatusEnum.EmAndamento // Somente pedidos em andamento podem ser cancelados
                        )
                        .Include(p => p.Produto)
                        .FirstOrDefaultAsync();

            _ = pedido ?? throw new Exception("O pedido solicitado nao existe ou seu status nao permite que seja cancelado");

            // Obtendo comanda nao paga para atualizar o valor de acordo com o cancelamento
            var comanda = await _context.Comanda
                        .Where(c => c.ComandaId == pedido.ComandaId && !c.Paga)
                        .FirstOrDefaultAsync();

            _ = comanda ?? throw new Exception("A comanda referente ao pedido nao existe ou ja foi encerrada");

            // Se o produto não estiver incluso no rodizio, o valor do pedido será subtraido do valor total da comanda
            if (pedido.Produto.Valor > 0)
                comanda.Valor -= pedido.Quantidade * pedido.Produto.Valor;

            pedido.StatusId = (int)StatusEnum.Cancelado;

            await _context.SaveChangesAsync();
        }
    }
}
