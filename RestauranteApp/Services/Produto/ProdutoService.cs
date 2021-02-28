using System;
using System.Collections.Generic;
using RestauranteApp.Interfaces;
using System.Globalization;
using RestauranteApp.Services.Produto.Models;
using RestauranteApp.DatabaseControl;
using RestauranteApp.Services.TipoProduto;

namespace RestauranteApp.Services.Produto
{
    class ProdutoService
    {

        private Entidades.Produto ObterProdutoEntidade(int produtoId)
        {
            string produtoCsv = Database.Select(Entidade.Produto, produtoId);

            return new Entidades.Produto().ConverterEmEntidade(produtoCsv);
        }

        public static ProdutoMenuModel ObterProduto(int produtoId, bool validarDisponibilidade = true)
        {
            string produtoCsv = Database.Select(Entidade.Produto, produtoId);

            var produtoEntidade = new Entidades.Produto().ConverterEmEntidade(produtoCsv);

            if (validarDisponibilidade)
            {
                if (!produtoEntidade.Disponivel)
                    return null;
            }

            return new ProdutoMenuModel() { 
                ProdutoId = produtoEntidade.ProdutoId,
                Nome = produtoEntidade.Nome,
                Valor = produtoEntidade.Valor,
                QuantidadePermitida = produtoEntidade.QuantidadePermitida
            };
        }

        public static List<ProdutoMenuModel> ObterProdutos(bool apenasDisponiveis)
        {
            string[] produtosCsv = Database.Select(Entidade.Produto);

            var listaProdutos = new List<ProdutoMenuModel>();

            foreach (string produtoCsv in produtosCsv)
            {
                var produto = new Entidades.Produto().ConverterEmEntidade(produtoCsv);

                if (apenasDisponiveis)
                {
                    if (!produto.Disponivel) continue;
                }
                listaProdutos.Add(new ProdutoMenuModel()
                {
                    ProdutoId = produto.ProdutoId,
                    Nome = produto.Nome,
                    QuantidadePermitida = produto.QuantidadePermitida,
                    Valor = produto.Valor
                });
            }
            return listaProdutos;
        }

        public static List<ProdutoMenuModel> ObterProdutosPorTipo(int tipoProduto, bool validarDisponibilidade = true)
        {
            List<ProdutoMenuModel> listaProdutos = new List<ProdutoMenuModel>();

            string[] produtosCsv = Database.Select(Entidade.Produto);

            foreach (string produtoCsv in produtosCsv)
            {
                var produto = new Entidades.Produto().ConverterEmEntidade(produtoCsv);

                if (tipoProduto == produto.Tipo)
                {
                    if (validarDisponibilidade)
                    {
                        if (!produto.Disponivel) continue;
                    }
                    listaProdutos.Add(new ProdutoMenuModel()
                    {
                        ProdutoId = produto.ProdutoId,
                        Nome = produto.Nome,
                        Valor = produto.Valor,
                        QuantidadePermitida = produto.QuantidadePermitida
                    });
                }
            }

            return listaProdutos;
        }

        public int ObterQuantidadePermitida(int produtoId)
        {
            return ObterProdutoEntidade(produtoId).QuantidadePermitida;
        }

        public bool ValidarQuantidade(int produtoId, int quantidade)
        {
            return !(quantidade > ObterProdutoEntidade(produtoId).QuantidadePermitida);
        }

        public bool VerificarDisponibilidade(int produtoId)
        {
            return ObterProdutoEntidade(produtoId).Disponivel;
        }

    }
}
