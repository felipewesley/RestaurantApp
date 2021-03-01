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
                    ViewPrinter.Println($" * INCLUSO * ", ConsoleColor.DarkGreen);
                else
                    ViewPrinter.Println($" R$ { produto.Valor } ", ConsoleColor.DarkCyan);

                DivisorListaProdutos();
            });
        }

        public static void DivisorListaProdutos()
        {
            ViewPrinter.Println("\t----------------------------------------------------");
        }

        public static void MostrarTiposProduto()
        {
            var listaTipos = TipoProdutoService.ObterTipoProduto();

            foreach (var tipoProduto in listaTipos)
            {
                ViewPrinter.Print($"\t[{ tipoProduto.Tipo }]", ConsoleColor.Cyan);
                ViewPrinter.Println($" { tipoProduto.Descricao }");
                // DivisorListaProdutos();
            }
        }
    }
}
