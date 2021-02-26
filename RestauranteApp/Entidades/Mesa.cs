using System;
using RestauranteApp.Interfaces;

namespace RestauranteApp.Entidades
{
    class Mesa : ParseToEntity<Mesa>
    {
        public int MesaId { get; set; }
        public int Capacidade { get; set; }
        public bool Ocupada { get; set; }

        public Mesa ConverterEmEntidade(string dados)
        {
            string[] arrDados = dados.Split(',');

            return new Mesa()
            {
                MesaId = int.Parse(arrDados[0]),
                Capacidade = int.Parse(arrDados[1]),
                Ocupada = bool.Parse(arrDados[2])
            };
        }
    }
}
