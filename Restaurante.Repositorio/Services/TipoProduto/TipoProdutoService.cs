using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Restaurante.Repositorio.Contexto;
using Restaurante.Repositorio.Services.TipoProduto.Models;

namespace Restaurante.Repositorio.Services.TipoProduto
{
    public class TipoProdutoService : RestauranteService, ITipoProdutoService
    {
        public TipoProdutoService(RestauranteContexto context) : base(context) { }

        public void ValidarTipoProduto(int tipoProduto)
        {
            if (tipoProduto <= 0)
                throw new Exception("Tipo de produto solicitado inválido");
        }

        public async Task<ICollection<TipoProdutoModel>> BuscarTiposProduto()
        {
            var tiposColecao = await _context.TipoProduto
                            .Select(t => new TipoProdutoModel()
                            {
                                TipoProdutoId = t.TipoProdutoId,
                                Descricao = t.Descricao
                            })
                            .ToListAsync();

            return tiposColecao;
        }

        public async Task<string> ObterTipoProduto(int tipoProdutoId)
        {
            var tipoProduto = await _context.TipoProduto
                            .Where(t => t.TipoProdutoId == tipoProdutoId)
                            .Select(t => t.Descricao)
                            .FirstOrDefaultAsync();

            _ = tipoProduto ?? throw new Exception("O tipo de produto solicitado não existe");

            return tipoProduto;
        }
    }
}
