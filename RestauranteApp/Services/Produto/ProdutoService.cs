using System;
using System.Collections.Generic;
using RestauranteApp.Interfaces;
using System.Globalization;
using RestauranteApp.Services.Produto.Models;
using RestauranteApp.DatabaseControl;
using RestauranteApp.Services.TipoProduto;

namespace RestauranteApp.Services.Produto
{
    class ProdutoService : ParseToEntity<ProdutoService>
    {
        public int ProdutoId { get; private set; }
        public string Nome { get; private set; }
        public float Valor { get; private set; }
        public bool Disponivel { get; private set; }
        public int QuantidadePermitida { get; private set; }
        public int Tipo { get; private set; }

        public ProdutoService ConverterEmEntidade(string dados)
        {
            string[] arr_dados = dados.Split(',');

            return new ProdutoService()
            {
                ProdutoId = int.Parse(arr_dados[0]),
                Nome = arr_dados[1],
                Valor = float.Parse(arr_dados[3], CultureInfo.InvariantCulture),
                Disponivel = int.Parse(arr_dados[4]) == 1,
                QuantidadePermitida = int.Parse(arr_dados[5]),
                Tipo = int.Parse(arr_dados[6])
            };
        }

        private Entidades.Produto ObterProdutoEntidade(int produtoId)
        {
            string produtoCsv = Database.Select(Entidade.Produto, produtoId);

            return new Entidades.Produto().ConverterEmEntidade(produtoCsv);
        }

        public ProdutoMenuModel ObterProduto(int produtoId, bool validarDisponibilidade = true)
        {
            string produtoCsv = Database.Select(Entidade.Produto, produtoId);

            var produtoEntidade = new Entidades.Produto().ConverterEmEntidade(produtoCsv);

            if (validarDisponibilidade)
            {
                if (!produtoEntidade.Disponivel)
                    return null;
            }

            return new ProdutoMenuModel() { 
                Nome = produtoEntidade.Nome,
                Valor = produtoEntidade.Valor,
                QuantidadePermitida = produtoEntidade.QuantidadePermitida
            };
        }

        public List<ProdutoMenuModel> ObterProdutosPorTipo(TipoProdutoEnum tipoProduto, bool validarDisponibilidade = true)
        {
            List<ProdutoMenuModel> listaProdutos = new List<ProdutoMenuModel>();

            string[] produtosCsv = Database.Select(Entidade.Produto);

            foreach (string produtoCsv in produtosCsv)
            {
                var produto = new Entidades.Produto().ConverterEmEntidade(produtoCsv);

                if (((int)tipoProduto) == produto.Tipo)
                {
                    if (validarDisponibilidade)
                    {
                        if (!produto.Disponivel) continue;
                    }
                    listaProdutos.Add(new ProdutoMenuModel()
                    {
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
