using System;

namespace RestauranteApp.Services.Comanda.Models
{
    class ComandaCompletaModel
    {
        public int ComandaId { get; set; }
        public int MesaId { get; set; }
        public DateTime DataHoraEntrada { get; set; }
        public DateTime? DataHoraSaida { get; set; }
        public float Valor { get; set; }
        public bool Paga { get; set; }
        public int QuantidadeClientes { get; set; }
    }
}
