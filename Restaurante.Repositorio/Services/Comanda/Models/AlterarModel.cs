using System;

namespace Restaurante.Repositorio.Services.Comanda.Models
{
    public class AlterarModel
    {
        public int MesaId { get; set; }
        public int ComandaId { get; set; }
        public int QuantidadeClientes { get; set; }

        public void Validar()
        {
            if (QuantidadeClientes <= 0) // Validar tambem se esta vazia
                throw new Exception("Nova quantidade de clientes invalida");
        }
    }
}
