using System.Collections.Generic;
using System.Threading.Tasks;
using Restaurante.Repositorio.Services.Produto.Models;

namespace Restaurante.Repositorio.Services.Produto
{
    public interface IProdutoService
    {
        Task<ProdutoModel> ObterProduto(int produtoId);
        Task<ICollection<ProdutoListagemModel>> BuscarProdutos();
        Task<ICollection<ProdutoListagemModel>> BuscarProdutos(int tipoId);
    }
}
