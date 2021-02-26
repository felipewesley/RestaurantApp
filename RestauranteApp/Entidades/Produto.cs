using System;
using RestauranteApp.Interfaces;
using System.Globalization;

namespace RestauranteApp.Entidades
{
    class Produto : ParseToEntity<Produto>
    {
        public int ProdutoId { get; set; }
        public string Nome { get; set; }
        public string Imagem { get; set; }
        public float Valor { get; set; }
        public bool Disponivel { get; set; }
        public int QuantidadePermitida { get; set; }
        public int Tipo { get; set; }

        public Produto ConverterEmEntidade(string dados)
        {
            string[] arrDados = dados.Split(',');

            return new Produto()
            {
                ProdutoId = int.Parse(arrDados[0]),
                Nome = arrDados[1],
                Imagem = arrDados[2],
                Valor = float.Parse(arrDados[3], CultureInfo.InvariantCulture),
                Disponivel = bool.Parse(arrDados[4]),
                QuantidadePermitida = int.Parse(arrDados[5]),
                Tipo = int.Parse(arrDados[6])
            };
        }
    }
}
