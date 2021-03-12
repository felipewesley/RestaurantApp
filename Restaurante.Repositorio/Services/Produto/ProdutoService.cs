using Microsoft.EntityFrameworkCore;
using Restaurante.Repositorio.Contexto;
using Restaurante.Repositorio.Services.Produto.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurante.Repositorio.Services.Produto
{
    public class ProdutoService : RestauranteService, IProdutoService
    {
        public ProdutoService(RestauranteContexto context) : base(context) { }

        public void ValidarProduto(int produtoId)
        {
            if (produtoId <= 0)
                throw new Exception("O produto solicitado é inválido");
        }

        public async Task<ICollection<ProdutoListagemModel>> BuscarProdutos()
        {
            var produtosCollection = await _context.Produto
                                    .Where(p => p.Disponivel == true)
                                    .Include(p => p.TipoProduto)
                                    .Select(p => new ProdutoListagemModel()
                                    {
                                        ProdutoId = p.ProdutoId,
                                        Nome = p.Nome,
                                        Valor = p.Valor,
                                        TipoProduto = p.TipoProduto.Descricao
                                    }).ToListAsync();

            return produtosCollection;
        }

        public async Task<ICollection<ProdutoListagemModel>> BuscarProdutos(int tipoId)
        {
            var produtosCollection = await _context.Produto
                                    .Include(p => p.TipoProduto)
                                    .Where(p => p.Disponivel == true && p.TipoProduto.TipoProdutoId == tipoId)
                                    .Select(p => new ProdutoListagemModel()
                                    {
                                        ProdutoId = p.ProdutoId,
                                        Nome = p.Nome,
                                        Valor = p.Valor,
                                        TipoProduto = p.TipoProduto.Descricao
                                    }).ToListAsync();

            return produtosCollection;
        }

        public async Task<ProdutoModel> ObterProduto(int produtoId)
        {
            ValidarProduto(produtoId);

            var produto = await _context.Produto
                        .Where(p => p.ProdutoId == produtoId)
                        .Include(p => p.TipoProduto)
                        .Select(p => new ProdutoModel()
                        {
                            Nome = p.Nome,
                            TipoProduto = p.TipoProduto.Descricao,
                            QuantidadePermitida = p.QuantidadePermitida,
                            Valor = p.Valor
                        })
                        .FirstOrDefaultAsync();

            _ = produto ?? throw new Exception("Não foi possível obter o produto solicitado");

            return produto;
        }
    }
}
