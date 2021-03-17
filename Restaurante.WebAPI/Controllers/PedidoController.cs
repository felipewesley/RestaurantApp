using Microsoft.AspNetCore.Mvc;
using Restaurante.Repositorio.Services.Pedido;
using Restaurante.Repositorio.Services.Pedido.Models;
using System.Threading.Tasks;

namespace Restaurante.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PedidoController : ControllerBase
    {
        private readonly PedidoService _service;

        public PedidoController(PedidoService service) => _service = service;

        [HttpGet("{pedidoId}")]
        public async Task<PedidoModel> Obter(int pedidoId)
        {
            return await _service.Obter(pedidoId);
        }

        [HttpPost]
        public async Task Registrar(FormularioModel model)
        {
            await _service.Registrar(model);
        }

        [HttpDelete("{pedidoId}")]
        public async Task Cancelar(int pedidoId)
        {
            await _service.Cancelar(pedidoId);
        }
    }
}
