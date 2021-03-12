using System.Threading.Tasks;
using System.Collections.Generic;
using Restaurante.Repositorio.Services.TipoProduto.Models;

namespace Restaurante.Repositorio.Services.TipoProduto
{
    public interface ITipoProdutoService
    {
        Task<ICollection<TipoProdutoModel>> BuscarTiposProduto();
        Task<string> ObterTipoProduto(int tipoProdutoId);
    }
}
