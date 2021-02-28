using System;
using System.Collections.Generic;
using RestauranteApp.DatabaseControl;
using RestauranteApp.Services.TipoProduto.Models;

namespace RestauranteApp.Services.TipoProduto
{
    class TipoProdutoService
    {
        private static Entidades.TipoProduto ObterTipoProdutoEntidade(int tipoProduto)
        {
            string tipoProdutoCsv = Database.Select(Entidade.TipoProduto, tipoProduto);

            return new Entidades.TipoProduto().ConverterEmEntidade(tipoProdutoCsv);
        }
        public static List<TipoProdutoModel> ObterTipoProduto()
        {
            string[] tiposProdutoCsv = Database.Select(Entidade.TipoProduto);

            var listaTipoProduto = new List<TipoProdutoModel>();

            foreach (string tipoProdutoCsv in tiposProdutoCsv)
            {
                var tipoProduto = new Entidades.TipoProduto().ConverterEmEntidade(tipoProdutoCsv);
                listaTipoProduto.Add(new TipoProdutoModel()
                {
                    Tipo = tipoProduto.Tipo,
                    Descricao = tipoProduto.Descricao
                });
            }
            return listaTipoProduto;
        }

        public static TipoProdutoModel ObterTipoProduto(int tipoId)
        {
            var tipoProduto = ObterTipoProdutoEntidade(tipoId);

            return new TipoProdutoModel()
            {
                Tipo = tipoProduto.Tipo,
                Descricao = tipoProduto.Descricao
            };
        }
    }
}
