using System.ComponentModel.DataAnnotations;

namespace Restaurante.Dominio
{
    public class Mesa
    {
        [Key]
        public int MesaId { get; set; } // PK

        public int Capacidade { get; set; }
        public bool Ocupada { get; set; }
    }
}
