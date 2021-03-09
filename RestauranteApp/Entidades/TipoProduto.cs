using RestauranteApp.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace RestauranteApp.Entidades
{
    class TipoProduto
    {
        [Key]
        public int Tipo { get; set; }
        public string Descricao { get; set; }

    }
}
