namespace Restaurante.Repositorio.Services.Pedido.Models
{
    public class AlterarModel
    {
        public int ComandaId { get; set; }
        public int PedidoId { get; set; }
        public int QuantidadePermitida { get; set; }
        public double ProdutoValor { get; set; }
        public int NovaQuantidade { get; set; }
    }
}
