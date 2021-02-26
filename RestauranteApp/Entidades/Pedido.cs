using System;
using RestauranteApp.Interfaces;

namespace RestauranteApp.Entidades
{
    class Pedido : ParseToCsv, ParseToEntity<Pedido>
    {
        public int PedidoId { get; set; }
        public int ComandaId { get; set; }
        public int ProdutoId { get; set; }
        public int Status { get; set; }
        public int Quantidade { get; set; }

        public Pedido ConverterEmEntidade(string dados)
        {
            string[] arrDados = dados.Split(',');

            return new Pedido()
            {
                PedidoId = int.Parse(arrDados[0]),
                ComandaId = int.Parse(arrDados[1]),
                ProdutoId = int.Parse(arrDados[2]),
                Status = int.Parse(arrDados[3]),
                Quantidade = int.Parse(arrDados[4])
            };
        }

        public string Imprimir()
        {
            return string.Join(",", PedidoId, ComandaId, ProdutoId, Status, Quantidade);
        }
    }
}
