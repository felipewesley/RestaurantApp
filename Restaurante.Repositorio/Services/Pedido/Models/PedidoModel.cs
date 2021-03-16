using Restaurante.Repositorio.Services.Produto.Models;

namespace Restaurante.Repositorio.Services.Pedido.Models
{
    public class PedidoModel
    {
        public int ComandaId { get; set; }
        public int PedidoId { get; set; }
        public ProdutoListagemModel Produto { get; set; }
        public int Quantidade { get; set; }
        public string Status { get; set; }
    }
}
