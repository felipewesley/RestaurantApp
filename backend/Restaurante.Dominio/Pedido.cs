using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Restaurante.Dominio.Enum;

namespace Restaurante.Dominio
{
    public class Pedido
    {
        [Key]
        public int PedidoId { get; set; } // PK

        public int ComandaId { get; set; } // FK
        [ForeignKey(nameof(ComandaId))]
        public Comanda Comanda { get; set; }

        public int ProdutoId { get; set; } // FK
        [ForeignKey(nameof(ProdutoId))]
        public Produto Produto { get; set; }

        public int StatusId { get; set; } // FK
        [ForeignKey(nameof(StatusId))]
        public StatusEnum StatusEnum { get; set; }

        public DateTime DataHoraRealizacao { get; set; }
        public int Quantidade { get; set; }
    }
}
