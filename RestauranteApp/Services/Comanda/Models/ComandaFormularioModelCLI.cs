using System;
using RestauranteApp.Services.Mesa;

namespace RestauranteApp.Services.Comanda.Models
{
    class ComandaFormularioModelCLI
    {
        public int ComandaId { get; set; }
        public int MesaId { get; set; }
        private int _quantidadeCliente;
        public int QuantidadeCliente
        {
            get => _quantidadeCliente;
            set
            {

                /*if (value <= 0 || value > MesaService.ObterQuantidadeClientes(MesaId))
                    throw new Exception("Quantidade informada de pessoas invalida!");
                else
                    _quantidadeCliente = value;
                */
                _quantidadeCliente = value;
            }
        }

        public void Validar()
        {
            if (MesaId <= 0 || MesaId > 16)
                throw new Exception("A mesa informada não existe!");

            if (_quantidadeCliente <= 0 || _quantidadeCliente > MesaService.ObterQuantidadeClientes(MesaId))
                throw new Exception("A mesa não pode comportar esta quantidade de pessoas!");
        }
    }
}
