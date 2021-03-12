using Restaurante.Repositorio.Services.Produto.Models;
using Restaurante.Repositorio.Services.Status.Models;

namespace Restaurante.Repositorio.Services.Pedido.Models
{
    public class PedidoModel
    {
        public int PedidoId { get; set; }
        public string Produto { get; set; }
        public int Quantidade { get; set; }
        public double Valor { get; set; }
        public string Status { get; set; }
    }
}
