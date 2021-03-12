using Microsoft.EntityFrameworkCore;
using Restaurante.Repositorio.Contexto;
using Restaurante.Repositorio.Enum;
using Restaurante.Repositorio.Services.Pedido.Models;
using Restaurante.Repositorio.Services.Produto.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Restaurante.Repositorio.Services.Pedido
{
    public class PedidoService : RestauranteService, IPedidoService
    {
        public PedidoService(RestauranteContexto context) : base(context) { }

        public void ValidarPedido(int pedidoId)
        {
            if (pedidoId < 0)
                throw new Exception("O número do pedido informado é inválido");
        }

        public async Task RegistrarPedido(PedidoFormularioModel pedidoModel)
        {
            pedidoModel.Validar();

            var comanda = await _context.Comanda
                        .Where(c => c.ComandaId == pedidoModel.ComandaId)
                        .FirstOrDefaultAsync();

            // Se a comanda já foi paga, o pedido não será registrado
            if (comanda.Paga)
                throw new Exception("Não é possível cadastrar um pedido em uma comanda que já foi paga");

            _context.Pedido.Add(new Dominio.Pedido()
            {
                ComandaId = pedidoModel.ComandaId,
                ProdutoId = pedidoModel.ProdutoId,
                StatusId = (int) StatusEnum.EmAndamento,
                Quantidade = pedidoModel.Quantidade,
            });

            var produto = await _context.Produto
                        .Where(p => p.ProdutoId == pedidoModel.ProdutoId)
                        .FirstOrDefaultAsync();

            // Se o produto não estiver incluso no rodizio, este pedido será somado ao valor total da comanda
            if (produto.Valor > 0)
                comanda.Valor += pedidoModel.Quantidade * produto.Valor;

            await SaveChangesAsync("Não foi possivel salvar o pedido");
        }

        public async Task CancelarPedido(int pedidoId, bool ignorarEntregue = false)
        {
            ValidarPedido(pedidoId);

            var pedido = await _context.Pedido
                        .Where(p => p.PedidoId == pedidoId)
                        .Include(p => p.Status)
                        .Include(p => p.Produto)
                        .FirstOrDefaultAsync();

            var comanda = await _context.Comanda
                        .Where(c => c.ComandaId == pedido.ComandaId)
                        .FirstOrDefaultAsync();

            // Se a comanda já estiver paga, o pedido não poderá ser cancelado
            if (comanda.Paga)
                throw new Exception("Não é possível cancelar um pedido de uma comanda que já foi paga");

            if (pedido.Status.StatusId == (int)StatusEnum.Entregue)
                if (!ignorarEntregue)
                    throw new Exception("O pedido não pode ser cancelado pois já foi entregue");

            if (pedido.Status.StatusId == (int)StatusEnum.Cancelado)
                throw new Exception("O pedido solicitado já foi cancelado");

            // Se o produto não estiver incluso no rodizio, o valor do pedido será subtraido do valor total da comanda
            if (pedido.Produto.Valor > 0)
                comanda.Valor -= pedido.Quantidade * pedido.Produto.Valor;

            pedido.StatusId = (int)StatusEnum.Cancelado;

            await SaveChangesAsync("Não foi possível cancelar o pedido");
        }

        public async Task<PedidoModel> ObterPedido(int pedidoId)
        {
            ValidarPedido(pedidoId);

            var pedido = await _context.Pedido
                        .Where(p => p.PedidoId == pedidoId)
                        .Include(p => p.Status)
                        .Include(p => p.Produto)
                        .ThenInclude(p => p.TipoProduto)
                        .Select(p => new PedidoModel()
                        {
                            ComandaId = p.ComandaId,
                            PedidoId = p.PedidoId,
                            Quantidade = p.Quantidade,
                            Status = p.Status.Descricao,
                            Produto = new ProdutoListagemModel()
                            {
                                Nome = p.Produto.Nome,
                                Valor = p.Produto.Valor,
                                TipoProduto = p.Produto.TipoProduto.Descricao
                            },
                            Valor = p.Produto.Valor * p.Quantidade // Verificar maneira melhor de implementar
                        }).FirstOrDefaultAsync();

            _ = pedido ?? throw new Exception("Não foi possível obter o pedido solicitado");

            return pedido;
        }

    }
}
