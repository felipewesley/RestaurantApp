using System;
using System.Collections.Generic;
using System.Text;

namespace RestauranteApp.Services.Pedido.Models
{
    class PedidoRealizadoModel
    {
        public int PedidoId { get; set; }
        public Entidades.Produto Produto { get; set; }
        public int Quantidade { get; set; }
        public Entidades.Status Status { get; set; }
    }
}
