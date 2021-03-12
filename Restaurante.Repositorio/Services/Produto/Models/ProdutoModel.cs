using Restaurante.Repositorio.Services.TipoProduto.Models;

namespace Restaurante.Repositorio.Services.Produto.Models
{
    public class ProdutoModel
    {
        public string Nome { get; set; }
        public double Valor { get; set; }
        public TipoProdutoModel TipoProduto { get; set; }
    }
}
