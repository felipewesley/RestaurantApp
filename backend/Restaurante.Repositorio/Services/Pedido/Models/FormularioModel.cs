using System;

namespace Restaurante.Repositorio.Services.Pedido.Models
{
    public class FormularioModel
    {
        public int ComandaId { get; set; }
        public int ProdutoId { get; set; }
        public int Quantidade { get; set; }

        public void Validar()
        {
            if (Quantidade <= 0)
                throw new Exception("Quantidade informada inválida");
        }
    }
}
