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

        private readonly RestauranteContext _context;

        // Injetando contexto através do construtor da classe
        public TipoProdutoService(RestauranteContext context)
        {
            _context = context;
        }

        public List<TipoProdutoModel> ObterTipoProduto()
        {

            return _context.TipoProduto
                    .Select(t => new TipoProdutoModel() 
                    { 
                        TipoProdutoId = t.TipoProdutoId,
                        Descricao = t.Descricao
                    })
                    .ToList();
        }

        public TipoProdutoModel ObterTipoProduto(int tipoId)
        {

            return _context.TipoProduto
                    .Where(t => t.TipoProdutoId == tipoId)
                    .Select(t => new TipoProdutoModel()
                    {
                        TipoProdutoId = t.TipoProdutoId,
                        Descricao = t.Descricao
                    })
                    .FirstOrDefault();
        }

        public bool TipoProdutoValido(int tipoId)
        {
            return _context.TipoProduto.Any(t => t.TipoProdutoId == tipoId);
        }
    }
}
