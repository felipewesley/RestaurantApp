using System;

namespace Restaurante.Repositorio.Services.Comanda.Models
{
    public class FormularioModel
    {
        public int MesaId { get; set; }
        public int QuantidadeCliente { get; set; }

        public void Validar()
        {
            if (QuantidadeCliente <= 0)
                throw new Exception("Quantidade de clientes inválida");
        }
    }
}