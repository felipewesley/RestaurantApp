using System;
using RestauranteApp.Interfaces;

namespace RestauranteApp.Entidades
{
    class Status : ParseToEntity<Status>
    {
        public int StatusId { get; set; }
        public string Descricao { get; set; }

        public Status ConverterEmEntidade(string dados)
        {
            string[] arrDados = dados.Split(',');

            return new Status()
            {
                StatusId = int.Parse(arrDados[0]),
                Descricao = arrDados[1]
            };
        }
    }
}
