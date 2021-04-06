using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Restaurante.Dominio
{
    public class Pedido
    {
        [Key]
        public int PedidoId { get; set; }
        public int ComandaId { get; set; }
        [ForeignKey(nameof(ComandaId))]
        public Comanda Comanda { get; set; }
        public int ProdutoId { get; set; }
        [ForeignKey(nameof(ProdutoId))]
        public Produto Produto { get; set; }
        public int StatusId { get; set; }
        [ForeignKey(nameof(StatusId))]
        public Status Status { get; set; }
        public int Quantidade { get; set; }
    }
}
