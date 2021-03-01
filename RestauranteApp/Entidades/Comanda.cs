using RestauranteApp.Interfaces;
using System;
using System.Globalization;

namespace RestauranteApp.Entidades
{
    class Comanda : ParseToCsv, ParseToEntity<Comanda>
    {
        public int ComandaId { get; set; }
        public int MesaId { get; set; }
        public DateTime DataHoraEntrada { get; set; }
        public DateTime DataHoraSaida { get; set; }
        public float Valor { get; set; }
        public bool Paga { get; set; }
        public int QuantidadeClientes { get; set; }

        public Comanda ConverterEmEntidade(string dados)
        {
            string[] arrDados = dados.Split(',');

            return new Comanda()
            {
                ComandaId = int.Parse(arrDados[0]),
                MesaId = int.Parse(arrDados[1]),
                DataHoraEntrada = DateTime.Parse(arrDados[2]),
                DataHoraSaida = DateTime.Parse(arrDados[3]),
                Valor = float.Parse(arrDados[4]),
                Paga = bool.Parse(arrDados[5]),
                QuantidadeClientes = int.Parse(arrDados[6])
            }; 
        }

        public string Imprimir()
        {
            return string.Join(",", ComandaId, MesaId, DataHoraEntrada, DataHoraSaida, Valor.ToString("F2", CultureInfo.InvariantCulture), Paga, QuantidadeClientes);
        }

        public int ObterEntidadeId(string dados)
        {
            return ConverterEmEntidade(dados).ComandaId;
        }
    }
}
