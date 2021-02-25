using System;
using RestauranteApp.Interfaces;
using System.Globalization;

namespace RestauranteApp.Services.Produto
{
    class ProdutoService : ParseToEntity<ProdutoService>
    {
        public int ProdutoId { get; private set; }
        public string Nome { get; private set; }
        public float Valor { get; private set; }
        public bool Disponivel { get; private set; }
        public int QuantidadePermitida { get; private set; }
        public int Tipo { get; private set; }

        public ProdutoService ConverterEmEntidade(string dados)
        {
            string[] arr_dados = dados.Split(',');

            return new ProdutoService()
            {
                ProdutoId = int.Parse(arr_dados[0]),
                Nome = arr_dados[1],
                Valor = float.Parse(arr_dados[3], CultureInfo.InvariantCulture),
                Disponivel = int.Parse(arr_dados[4]) == 1,
                QuantidadePermitida = int.Parse(arr_dados[5]),
                Tipo = int.Parse(arr_dados[6])
            };
        }

    }
}
