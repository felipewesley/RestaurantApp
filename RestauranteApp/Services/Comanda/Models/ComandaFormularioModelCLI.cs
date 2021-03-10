using System;
using RestauranteApp.Services.Mesa;
using RestauranteApp.Services.Mesa.Models;

namespace RestauranteApp.Services.Comanda.Models
{
    class ComandaFormularioModelCLI
    {
        public int ComandaId { get; set; }
        public int MesaId { get; set; }
        public MesaFormularioModel Mesa { get; set; }

        private int _quantidadeCliente;
        public int QuantidadeCliente
        {
            get => _quantidadeCliente;
            set => _quantidadeCliente = value;
        }

        public void Validar()
        {

            if (Mesa.MesaId <= 0 || Mesa.MesaId > 16)
                throw new Exception("A mesa informada não existe!");

            if (_quantidadeCliente <= 0 || _quantidadeCliente > Mesa.Capacidade)
                throw new Exception("A mesa não comporta esta quantidade de pessoas!");
        }
    }
}
