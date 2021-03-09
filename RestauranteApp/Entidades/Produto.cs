using RestauranteApp.Interfaces;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RestauranteApp.Entidades
{
    class Produto
    {
        [Key]
        public int ProdutoId { get; set; }
        public string Nome { get; set; }
        public string Imagem { get; set; }
        public float Valor { get; set; }
        public bool Disponivel { get; set; }
        public int QuantidadePermitida { get; set; }
        public int TipoId { get; set; }
        [ForeignKey(nameof(TipoId))]
        public TipoProduto Tipo { get; set; }

    }
}
