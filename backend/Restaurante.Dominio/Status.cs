using Restaurante.Dominio.Enum;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Restaurante.Dominio
{
    public class Status
    {
        [Key]
        [Column("StatusId")]
        public StatusEnum StatusEnum { get; set; } // PK

        public string Descricao { get; set; }
    }
}
