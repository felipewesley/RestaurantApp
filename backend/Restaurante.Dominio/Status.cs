using System.ComponentModel.DataAnnotations;


namespace Restaurante.Dominio
{
    public class Status
    {
        [Key]
        public int StatusId { get; set; }
        public string Descricao { get; set; }
    }
}
