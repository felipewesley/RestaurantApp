using System;

namespace RestauranteApp.Services.Comanda.Models
{
    class ComandaAtiva
    {
        public int ComandaId { get; set; }
        public int MesaId { get; set; }
        public DateTime DataHoraEntrada { get; set; }
        public float Valor { get; set; }
        public int QuantidadeClientes { get; set; }

    }
}
