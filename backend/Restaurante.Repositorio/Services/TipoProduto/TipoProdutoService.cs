using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Restaurante.Repositorio.Contexto;
using Restaurante.Repositorio.Services.TipoProduto.Models;

namespace Restaurante.Repositorio.Services.TipoProduto
{
    public class TipoProdutoService
    {
        private readonly RestauranteContexto _context;
        public TipoProdutoService(RestauranteContexto context) => _context = context;

        public async Task<ICollection<BuscaModel>> Listar()
        {
            var tiposProduto = await _context.TipoProduto
                            .Select(t => new BuscaModel()
                            {
                                TipoProdutoId = t.TipoProdutoId,
                                Descricao = t.Descricao
                            })
                            .ToListAsync();

            return tiposProduto;
        }
    }
}
