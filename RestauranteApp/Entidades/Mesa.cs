using System.ComponentModel.DataAnnotations;

namespace RestauranteApp.Entidades
{
    class Mesa
    {
        [Key]
        public int MesaId { get; set; }
        public int Capacidade { get; set; }
        public bool Ocupada { get; set; }
    }
}
