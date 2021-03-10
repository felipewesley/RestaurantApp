using RestauranteApp.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace RestauranteApp.Entidades
{
    class TipoProduto
    {
        [Key]
        public int TipoProdutoId { get; set; }
        public string Descricao { get; set; }

    }
}
