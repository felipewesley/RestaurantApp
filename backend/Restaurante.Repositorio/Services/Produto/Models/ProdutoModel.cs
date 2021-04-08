namespace Restaurante.Repositorio.Services.Produto.Models
{
    public class ProdutoModel
    {
        public int ProdutoId { get; set; }
        public string Nome { get; set; }
        public double Valor { get; set; }
        public string TipoProduto { get; set; }
        public int QuantidadePermitida { get; set; }
    }
}
