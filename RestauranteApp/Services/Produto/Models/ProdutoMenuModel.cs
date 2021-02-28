using System;

namespace RestauranteApp.Services.Produto.Models
{
    class ProdutoMenuModel
    {
        public int ProdutoId { get; set; }
        public string Nome { get; set; }
        public float Valor { get; set; }
        public int QuantidadePermitida { get; set; }
    }
}
