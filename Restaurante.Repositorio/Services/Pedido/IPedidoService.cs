using Restaurante.Repositorio.Services.Pedido.Models;
using System.Threading.Tasks;

namespace Restaurante.Repositorio.Services.Pedido
{
    public interface IPedidoService
    {
        Task Registrar(FormularioModel model);
        Task<PedidoModel> Obter(int pedidoId);
        Task Cancelar(int pedidoId);
    }
}
