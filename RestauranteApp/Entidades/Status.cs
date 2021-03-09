using RestauranteApp.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace RestauranteApp.Entidades
{
    class Status
    {
        [Key]
        public int StatusId { get; set; }
        public string Descricao { get; set; }
    }
}
