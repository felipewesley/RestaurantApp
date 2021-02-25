using System;
using RestauranteApp.Interfaces;

namespace RestauranteApp.Entidades
{
    class Pedido : ParseToCsv
    {
        public int PedidoId { get; set; }
        public int ComandaId { get; set; }
        public int ProdutoId { get; set; }
        public int Status { get; set; }
        public int Quantidade { get; set; }

        public string Imprimir()
        {
            return $"{ PedidoId },{ ComandaId },{ ProdutoId },{ Status },{ Quantidade }";
        }
    }
}
