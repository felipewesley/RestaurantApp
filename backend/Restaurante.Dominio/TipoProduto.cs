using System.ComponentModel.DataAnnotations;

namespace Restaurante.Dominio
{
    public class TipoProduto
    {
        [Key]
        public int TipoProdutoId { get; set; } // PK

        public string Descricao { get; set; }
    }
}
