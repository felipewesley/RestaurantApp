using System;

namespace RestauranteApp.Entidades
{
    class Pedido
    {
        public int PedidoId { get; set; }
        public int ComandaId { get; set; }
        public int ProdutoId { get; set; }
        public int Status { get; set; }
        public int Quantidade { get; set; }
    }
}
