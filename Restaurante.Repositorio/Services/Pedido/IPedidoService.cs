using Restaurante.Repositorio.Services.Pedido.Models;
using System.Threading.Tasks;

namespace Restaurante.Repositorio.Services.Pedido
{
    public interface IPedidoService
    {
        Task<PedidoModel> ObterPedido(int pedidoId);
        Task RegistrarPedido(PedidoFormularioModel pedidoModel);
        Task CancelarPedido(int pedidoId, bool ignorarEntregue = false);
    }
}
