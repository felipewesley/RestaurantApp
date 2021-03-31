using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Restaurante.Dominio
{
    public class Produto
    {
        [Key]
        public int ProdutoId { get; set; }
        public string Nome { get; set; }
        public string Imagem { get; set; }
        public double Valor { get; set; }
        public bool Disponivel { get; set; }
        public int QuantidadePermitida { get; set; }
        public int TipoProdutoId { get; set; }
        [ForeignKey(nameof(TipoProdutoId))]
        public TipoProduto TipoProduto { get; set; }
    }
}
