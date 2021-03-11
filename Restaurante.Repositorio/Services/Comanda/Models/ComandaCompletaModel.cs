using Restaurante.Repositorio.Services.Pedido.NewFolder;
using System;
using System.Collections.Generic;

namespace Restaurante.Repositorio.Services.Comanda.Models
{
    public class ComandaCompletaModel
    {
        public int MesaId { get; set; }
        public DateTime DataHoraEntrada { get; set; }
        public int QuantidadeClientes { get; set; }
        public ICollection<PedidoModel> Pedidos { get; set; }
        public double Valor { get; set; }
    }
}
