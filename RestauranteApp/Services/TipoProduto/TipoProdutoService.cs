using System;
using System.Collections.Generic;
using RestauranteApp.Contexto;
using RestauranteApp.DatabaseControl;
using RestauranteApp.Services.TipoProduto.Models;
using System.Linq;

namespace RestauranteApp.Services.TipoProduto
{
    class TipoProdutoService
    {
        public static List<TipoProdutoModel> ObterTipoProduto()
        {
            var context = new RestauranteContext();

            return context.TipoProduto
                    .Select(t => new TipoProdutoModel() 
                    { 
                        Tipo = t.Tipo,
                        Descricao = t.Descricao
                    })
                    .ToList();
        }

        public static TipoProdutoModel ObterTipoProduto(int tipoId)
        {
            var context = new RestauranteContext();

            return context.TipoProduto
                    .Where(t => t.Tipo == tipoId)
                    .Select(t => new TipoProdutoModel()
                    {
                        Tipo = t.Tipo,
                        Descricao = t.Descricao
                    })
                    .FirstOrDefault();
        }

        public static bool TipoProdutoValido(int tipoId)
        {
            var context = new RestauranteContext();

            return context.TipoProduto
                    .ToList()
                    .Exists(t => t.Tipo == tipoId);
        }
    }
}
