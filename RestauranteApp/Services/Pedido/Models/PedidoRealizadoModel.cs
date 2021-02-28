using System;
using System.Collections.Generic;
using System.Text;

namespace RestauranteApp.Services.Pedido.Models
{
    class PedidoRealizadoModel
    {
        public int PedidoId { get; set; }
        public int ProdutoId { get; set; }
        public int Quantidade { get; set; }
        public int Status { get; set; }
    }
}
