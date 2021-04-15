using Microsoft.EntityFrameworkCore;
using Restaurante.Dominio.Enum;
using Restaurante.Repositorio.Contexto;
using Restaurante.Repositorio.Services.Cozinha.Models;
using Restaurante.Repositorio.Services.Produto.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Restaurante.Repositorio.Services.Cozinha
{
    public class CozinhaService
    {
        private readonly RestauranteContexto _context;

        public CozinhaService(RestauranteContexto context) => _context = context;

        public async Task<StatusEnum> EntregarPedido(int pedidoId)
        {
            var pedido = await _context.Pedido
                        .Include(p => p.Comanda)
                        .Where(p => p.PedidoId == pedidoId && p.StatusEnum == StatusEnum.EmAndamento && !p.Comanda.Paga)
                        .FirstOrDefaultAsync();

            _ = pedido ?? throw new Exception("O pedido solicitado nao existe ou nao pode ser mais entregue");

            pedido.StatusEnum = StatusEnum.Entregue;
            pedido.DataHoraEntrega = DateTime.Now;

            await _context.SaveChangesAsync();

            return StatusEnum.Entregue;
        }

        public async Task<ICollection<ListagemCozinhaModel>> BuscarPendentes()
        {

            var pedidos = await _context.Pedido
                        .Include(p => p.Produto)
                        .Include(p => p.Comanda)
                        .ThenInclude(p => p.Mesa)
                        .Where(p => p.StatusEnum == StatusEnum.EmAndamento && !p.Comanda.Paga && p.Comanda.Mesa.Ocupada)
                        .Select(p => new ListagemCozinhaModel()
                        {
                            PedidoId = p.PedidoId,
                            ComandaId = p.ComandaId,
                            MesaId = p.Comanda.MesaId,
                            Quantidade = p.Quantidade,
                            StatusEnum = p.StatusEnum,
                            DataHoraRealizacao = p.DataHoraRealizacao,
                            Produto = new ProdutoModel()
                            {
                                ProdutoId = p.Produto.ProdutoId,
                                Nome = p.Produto.Nome,
                                Valor = p.Produto.Valor,
                                TipoProduto = p.Produto.TipoProduto.Descricao,
                                QuantidadePermitida = p.Produto.QuantidadePermitida
                            }
                        })
                        .OrderByDescending(p => p.PedidoId)
                        .ToListAsync();

            return pedidos;
        }

        public bool AutenticarUsuario(LoginModel model) 
        {
            model.Validar();

            var hashSenha = model.Senha;


            

            
            return true;
        }

        public string RegistrarUsuario(RegistrarModel model)
        {
            return string.Empty;
        }
    }
}
