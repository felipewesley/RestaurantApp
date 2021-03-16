using System;

namespace Restaurante.Repositorio.Services.Comanda.Models
{
    public class ResumidaModel
    {
        public int MesaId { get; set; }
        public DateTime DataHoraEntrada { get; set; }
        public int QuantidadeClientes { get; set; }
        public double Valor { get; set; }
    }
}
