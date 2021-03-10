using System;
using System.Collections.Generic;
using RestauranteApp.Interfaces;
using System.Globalization;
using RestauranteApp.Services.Produto.Models;
using RestauranteApp.DatabaseControl;
using RestauranteApp.Services.TipoProduto;
using RestauranteApp.Contexto;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace RestauranteApp.Services.Produto
{
    class ProdutoService
    {

        private readonly RestauranteContext _context;

        public ProdutoService(RestauranteContext context)
        {
            _context = context;
        }

        public ProdutoMenuModel ObterProduto(int produtoId, bool validarDisponibilidade = true)
        {

            return _context.Produto
                    .Where(p => p.ProdutoId == produtoId)
                    .Select(p => new ProdutoMenuModel()
                    {
                        ProdutoId = p.ProdutoId,
                        Nome = p.Nome,
                        QuantidadePermitida = p.QuantidadePermitida,
                        Valor = p.Valor
                    })
                    .FirstOrDefault();
        }

        public List<ProdutoMenuModel> ObterProdutos(bool apenasDisponiveis)
        {

            return _context.Produto
                    .Where(p => p.Disponivel == true)
                    .Select(p => new ProdutoMenuModel()
                    {
                        ProdutoId = p.ProdutoId,
                        Nome = p.Nome,
                        QuantidadePermitida = p.QuantidadePermitida,
                        Valor = p.Valor
                    })
                    .ToList();
        }

        public List<ProdutoMenuModel> ObterProdutosPorTipo(int tipoProduto, bool validarDisponibilidade = true)
        {

            return _context.Produto
                    .Include(p => p.TipoProduto)
                    .Where(p => p.TipoProduto.TipoProdutoId == tipoProduto)
                    .Select(p => new ProdutoMenuModel()
                    {
                        ProdutoId = p.ProdutoId,
                        Nome = p.Nome,
                        QuantidadePermitida = p.QuantidadePermitida,
                        Valor = p.Valor
                    })
                    .ToList();
        }

        public int ObterQuantidadePermitida(int produtoId)
        {

            return _context.Produto
                    .Where(p => p.ProdutoId == produtoId)
                    .Select(p => p.QuantidadePermitida)
                    .FirstOrDefault();
        }

        public bool ProdutoValido(int produtoId)
        {

            return _context.Produto
                    .ToList()
                    .Exists(p => p.ProdutoId == produtoId);
        }

    }
}
