using System;
using System.Collections.Generic;
using System.Text;

namespace RestauranteApp.Services.Produto.Models
{
    class ProdutoMenuModel
    {
        public string Nome { get; set; }
        public float Valor { get; set; }
        public int QuantidadePermitida { get; set; }
    }
}
