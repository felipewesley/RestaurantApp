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

        public static ProdutoMenuModel ObterProduto(int produtoId, bool validarDisponibilidade = true)
        {
            var context = new RestauranteContext();

            return context.Produto
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

        public static List<ProdutoMenuModel> ObterProdutos(bool apenasDisponiveis)
        {
            var context = new RestauranteContext();

            return context.Produto
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

        public static List<ProdutoMenuModel> ObterProdutosPorTipo(int tipoProduto, bool validarDisponibilidade = true)
        {
            var context = new RestauranteContext();

            return context.Produto
                    .Include(p => p.Tipo)
                    .Where(p => p.Tipo.Tipo == tipoProduto)
                    .Select(p => new ProdutoMenuModel()
                    {
                        ProdutoId = p.ProdutoId,
                        Nome = p.Nome,
                        QuantidadePermitida = p.QuantidadePermitida,
                        Valor = p.Valor
                    })
                    .ToList();
        }

        public static int ObterQuantidadePermitida(int produtoId)
        {
            var context = new RestauranteContext();

            return context.Produto
                    .Where(p => p.ProdutoId == produtoId)
                    .Select(p => p.QuantidadePermitida)
                    .FirstOrDefault();
        }

        public static bool ProdutoValido(int produtoId)
        {
            var context = new RestauranteContext();

            return context.Produto
                    .ToList()
                    .Exists(p => p.ProdutoId == produtoId);
        }

    }
}
