using System;
using RestauranteApp.Interfaces;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace RestauranteApp.Entidades
{
    class Comanda
    {
        [Key]
        public int ComandaId { get; set; }
        public int MesaId { get; set; }
        [ForeignKey(nameof(MesaId))]
        public Mesa Mesa { get; set; }
        public DateTime DataHoraEntrada { get; set; }
        public DateTime? DataHoraSaida { get; set; }
        public float Valor { get; set; }
        public bool Paga { get; set; }
        public int QuantidadeClientes { get; set; }
        public ICollection<Pedido> Pedidos { get; set; }

        public Comanda()
        {
            this.Pedidos = new Collection<Pedido>();
        }

    }
}
