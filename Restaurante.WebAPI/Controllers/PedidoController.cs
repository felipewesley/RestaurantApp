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

        private readonly IPedidoService _service;

        public PedidoController(IPedidoService service) => _service = service;

        // GET: api/<PedidoController>/Obter/5
        [HttpGet("Obter/{id}")]
        public async Task<PedidoModel> Obter(int id)
        {
            return await _service.ObterPedido(id);
        }

        // POST api/<PedidoController>
        [HttpPost]
        public async Task Novo(PedidoFormularioModel pedido)
        {
            await _service.RegistrarPedido(pedido);
        }

        // DELETE api/<PedidoController>/5
        [HttpDelete("{id}")]
        public async Task Cancelar(int id)
        {
            await _service.CancelarPedido(id);
        }
    }
}
