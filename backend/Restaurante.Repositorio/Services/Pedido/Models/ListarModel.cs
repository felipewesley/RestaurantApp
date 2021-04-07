using Restaurante.Dominio.Enum;
using Restaurante.Repositorio.Services.Produto.Models;

namespace Restaurante.Repositorio.Services.Pedido.Models
{
    public class ListarModel
    {
        public int ComandaId { get; set; }
        public int PedidoId { get; set; }
        public BuscaModel Produto { get; set; }
        public int Quantidade { get; set; }
        public StatusEnum StatusEnum { get; set; }
    }
}
