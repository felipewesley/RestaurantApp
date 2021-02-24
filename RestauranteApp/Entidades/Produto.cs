using System;

namespace RestauranteApp.Entidades
{
    class Produto
    {
        public int ProdutoId { get; set; }
        public string Nome { get; set; }
        public string Imagem { get; set; }
        public float Valor { get; set; }
        public bool Disponivel { get; set; }
        public int QuantidadePermitida { get; set; }
        public int Tipo { get; set; }
    }
}
