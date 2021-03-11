using System;

namespace Restaurante.Repositorio.Services.Comanda.Models
{
    public class ComandaFormularioModel
    {
        public int MesaId { get; set; }
        public int QuantidadeCliente { get; set; }

        public void Validar()
        {
            if (MesaId <= 0 || MesaId > 16)
                throw new Exception("A mesa informada não existe!");

            if (QuantidadeCliente <= 0 || QuantidadeCliente > 4)
                throw new Exception("A mesa não pode comportar esta quantidade de pessoas!");
        }
    }
}