using System;
using RestauranteApp.DatabaseControl;

namespace RestauranteApp.Services.Comanda.Models
{
    class ComandaEncerramento
    {
        private Database db = new Database();
        public int MesaId { get; set; }

        public float CalcularValorComanda()
        {
            return 0.0F;
        }
    }
}
