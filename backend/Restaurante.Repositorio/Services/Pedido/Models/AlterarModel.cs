using System;

namespace Restaurante.Repositorio.Services.Pedido.Models
{
    public class AlterarModel
    {
        public int ComandaId { get; set; }
        public int NovaQuantidade { get; set; }

        public void Validar()
        {
            if (NovaQuantidade <= 0) // Validar tambem se a quantidade estat vazia
                throw new Exception("Quantidade solicitada inválida");
        }
    }
}
