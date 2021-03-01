using System;
using RestauranteApp.Interfaces;

namespace RestauranteApp.Entidades
{
    class TipoProduto : ParseToEntity<TipoProduto>
    {
        public int Tipo { get; set; }
        public string Descricao { get; set; }

        public TipoProduto ConverterEmEntidade(string dados)
        {
            string[] arrDados = dados.Split(',');

            return new TipoProduto()
            {
                Tipo = int.Parse(arrDados[0]),
                Descricao = arrDados[1]
            };
        }

        public int ObterEntidadeId(string dados)
        {
            return ConverterEmEntidade(dados).Tipo;
        }
    }
}
