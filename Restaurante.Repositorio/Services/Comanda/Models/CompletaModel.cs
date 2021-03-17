using System;
using System.Collections.Generic;
using Restaurante.Repositorio.Services.Pedido.Models;

namespace Restaurante.Repositorio.Services.Comanda.Models
{
    public class CompletaModel
    {
        public int MesaId { get; set; }
        public DateTime DataHoraEntrada { get; set; }
        public int QuantidadeClientes { get; set; }
        public ICollection<ListarModel> Pedidos { get; set; }
        public double Valor { get; set; }
        public bool Paga { get; set; }
    }
}
