using System;
using System.Collections.Generic;
using System.Text;

namespace RestauranteApp.Services.Mesa.Models
{
    class MesaFormularioModel
    {
        public int MesaId { get; set; }
        public bool Ocupada { get; set; }
        public int Capacidade { get; set; }
    }
}
