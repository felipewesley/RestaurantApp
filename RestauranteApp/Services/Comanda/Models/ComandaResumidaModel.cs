using System;

namespace RestauranteApp.Services.Comanda.Models
{
    class ComandaResumidaModel
    {
        public int MesaId { get; set; }
        public DateTime DataHoraEntrada { get; set; }
        public int QuantidadeClientes { get; set; }
        public float Valor { get; set; }

    }
}
