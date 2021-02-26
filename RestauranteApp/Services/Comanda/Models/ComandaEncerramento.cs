using System;
using RestauranteApp.DatabaseControl;

namespace RestauranteApp.Services.Comanda.Models
{
    class ComandaEncerramento
    {
        public int MesaId { get; set; }

        public float CalcularValorComanda()
        {
            return 0.0F;
        }
    }
}
