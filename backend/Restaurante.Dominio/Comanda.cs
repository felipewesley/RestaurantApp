using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Restaurante.Dominio
{
    public class Comanda
    {
        [Key]
        public int ComandaId { get; set; }
        public int MesaId { get; set; }
        [ForeignKey(nameof(MesaId))]
        public Mesa Mesa { get; set; }
        public DateTime DataHoraEntrada { get; set; }
        public DateTime? DataHoraSaida { get; set; }
        public double Valor { get; set; }
        public bool Paga { get; set; }
        public int QuantidadeClientes { get; set; }
        public ICollection<Pedido> Pedidos { get; set; }

        public Comanda()
        {
            this.Pedidos = new Collection<Pedido>();
        }
    }
}
