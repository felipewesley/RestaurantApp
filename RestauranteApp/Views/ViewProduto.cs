using System;
using System.Collections.Generic;
using System.Text;
using RestauranteApp.Services.Produto;
using RestauranteApp.Services.Produto.Models;
using RestauranteApp.Services.TipoProduto;

namespace RestauranteApp.Views
{
    class ViewProduto
    {

        public static void MostrarListaProdutos(List<ProdutoMenuModel> listaProdutos)
        {
            listaProdutos.ForEach(produto =>
            {
                ViewPrinter.Print($"\t[ { produto.ProdutoId } ] { produto.Nome }");
                if (produto.Valor == 0)
                    ViewPrinter.Print($" * INCLUSO * ", ConsoleColor.DarkGreen);
                else
                    ViewPrinter.Print($" R$ { produto.Valor } ");

                DivisorListaProdutos();
            });
        }

        /*public static void MostrarListaProdutos(int tipo = 0)
        {
            var listaProdutos = tipo == 0 ? ProdutoService.ObterProdutos(true) : ProdutoService.ObterProdutosPorTipo(tipo);

            listaProdutos.ForEach(produto =>
            {
                ViewPrinter.Print($"\t[ { produto.ProdutoId } ] { produto.Nome }");
                if (produto.Valor == 0)
                    ViewPrinter.Print($" * INCLUSO * ", ConsoleColor.DarkGreen);
                else
                    ViewPrinter.Print($" R$ { produto.Valor } ");

                DivisorListaProdutos();
            });
        }*/

        public static void DivisorListaProdutos()
        {
            ViewPrinter.Println("\n\t----------------------------------------------------");
        }

        public static void MostrarTiposProduto()
        {
            var listaTipos = TipoProdutoService.ObterTipoProduto();

            foreach (var tipoProduto in listaTipos)
            {
                ViewPrinter.Print($"\t[ { tipoProduto.Tipo} ] { tipoProduto.Descricao }");
                DivisorListaProdutos();
            }
        }
    }
}
