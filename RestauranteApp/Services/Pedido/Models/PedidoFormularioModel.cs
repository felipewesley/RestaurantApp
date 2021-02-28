using System;
using System.Collections.Generic;
using System.Text;

namespace RestauranteApp.Services.Pedido.Models
{
    class PedidoFormularioModel
    {
        public int ComandaId { get; set; }
        public int ProdutoId { get; set; }
        public int Quantidade { get; set; }

        public void Validar()
        {

        }
    }
}
