using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Restaurante.Repositorio.Contexto;
using Restaurante.Repositorio.Services.Produto.Models;

namespace Restaurante.Repositorio.Services.Produto
{
    public class ProdutoService
    {
        private readonly RestauranteContexto _context;
        public ProdutoService(RestauranteContexto context) => _context = context;

        public async Task<ICollection<BuscaModel>> Buscar()
        {
            var produtos = await _context.Produto
                        .Where(p => p.Disponivel == true) // Apenas disponiveis
                        .Include(p => p.TipoProduto)
                        .Select(p => new BuscaModel()
                        {
                            ProdutoId = p.ProdutoId,
                            Nome = p.Nome,
                            Valor = p.Valor,
                            TipoProduto = p.TipoProduto.Descricao,
                            QuantidadePermitida = p.QuantidadePermitida
                        }).ToListAsync();

            return produtos;
        }

        public async Task<ICollection<BuscaModel>> BuscarPorTipo(int tipoId)
        {
            var produtos = await _context.Produto
                        .Where(p => p.Disponivel == true && p.TipoProdutoId == tipoId) // Disponiveis do tipo solicitado
                        .Include(p => p.TipoProduto)
                        .Select(p => new BuscaModel()
                        {
                            ProdutoId = p.ProdutoId,
                            Nome = p.Nome,
                            Valor = p.Valor,
                            TipoProduto = p.TipoProduto.Descricao,
                            QuantidadePermitida = p.QuantidadePermitida
                        }).ToListAsync();

            return produtos;
        }

        public async Task<BuscaModel> Obter(int produtoId)
        {
            var produto = await _context.Produto
                        .Where(p => p.ProdutoId == produtoId)
                        .Include(p => p.TipoProduto)
                        .Select(p => new BuscaModel()
                        {
                            ProdutoId = p.ProdutoId,
                            Nome = p.Nome,
                            Valor = p.Valor,
                            TipoProduto = p.TipoProduto.Descricao,
                            QuantidadePermitida = p.QuantidadePermitida
                        })
                        .FirstOrDefaultAsync();

            _ = produto ?? throw new Exception("O produto solicitado nao existe");

            return produto;
        }
    }
}