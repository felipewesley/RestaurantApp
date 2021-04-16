using Microsoft.EntityFrameworkCore;
using Restaurante.Dominio.Enum;
using Restaurante.Repositorio.Contexto;
using Restaurante.Repositorio.Services.Cozinha.Models;
using Restaurante.Repositorio.Services.Produto.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace Restaurante.Repositorio.Services.Cozinha
{
    public class CozinhaService
    {
        private readonly RestauranteContexto _context;

        public CozinhaService(RestauranteContexto context) => _context = context;

        public async Task<int> EntregarPedido(int pedidoId)
        {
            var pedido = await _context.Pedido
                        .Include(p => p.Comanda)
                        .Where(p => p.PedidoId == pedidoId && p.StatusEnum == StatusEnum.EmAndamento && !p.Comanda.Paga)
                        .FirstOrDefaultAsync();

            _ = pedido ?? throw new Exception("O pedido solicitado nao existe ou nao pode ser mais entregue");

            pedido.StatusEnum = StatusEnum.Entregue;
            pedido.DataHoraEntrega = DateTime.Now;

            await _context.SaveChangesAsync();

            var mesaId = pedido.Comanda.MesaId;

            return mesaId;
        }

        public async Task<ICollection<ListagemCozinhaModel>> BuscarPendentes()
        {

            var pedidos = await _context.Pedido
                        .Include(p => p.Produto)
                        .Include(p => p.Comanda)
                        .ThenInclude(p => p.Mesa)
                        .Where(p => p.StatusEnum == StatusEnum.EmAndamento && !p.Comanda.Paga && p.Comanda.Mesa.Ocupada && DateTime.Compare(p.DataHoraRealizacao.Date, DateTime.Now.Date) == 0)
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

        public async Task<HttpStatusCode> AutenticarUsuario(LoginModel model) 
        {
            model.Validar();

            var DEFAULT_USER = "admin.cozinha";
            var DEFAULT_PASSWORD = CreateMD5("abc123");

            if (model.Username != DEFAULT_USER || CreateMD5(model.Senha) != DEFAULT_PASSWORD)
            {
                var msg = new HttpResponseMessage(HttpStatusCode.Unauthorized) { ReasonPhrase = "Credenciais invalidas" };
                throw new HttpResponseException(msg);
            }

            return HttpStatusCode.OK;
        }

        public string RegistrarUsuario(RegistrarModel model)
        {
            return string.Empty;
        }

        public string CreateMD5(string input)
        {
            // Use input string to calculate MD5 hash
            using (System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create())
            {
                byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
                byte[] hashBytes = md5.ComputeHash(inputBytes);

                // Convert the byte array to hexadecimal string
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < hashBytes.Length; i++)
                {
                    sb.Append(hashBytes[i].ToString("X2"));
                }
                return sb.ToString();
            }
        }
    }
}
