using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Collections.Generic;
using Restaurante.Repositorio.Services.Pedido;
using Restaurante.Repositorio.Services.Pedido.Models;

namespace Restaurante.WebAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class PedidoController : ControllerBase
    {
        private readonly PedidoService _service;

        public PedidoController(PedidoService service) => _service = service;

        [HttpGet("{pedidoId}")]
        public async Task<ListarModel> Obter(int pedidoId)
        {
            return await _service.Obter(pedidoId);
        }

        [HttpGet("{comandaId}/comanda")]
        public async Task<ICollection<ListarModel>> ObterPorComanda(int comandaId)
        {
            return await _service.BuscarPorComanda(comandaId);
        }

        [HttpPost]
        public async Task Registrar(FormularioModel model)
        {
            await _service.Registrar(model);
        }

        [HttpPut("{pedidoId}")]
        public async Task Alterar(int pedidoId, AlterarModel model)
        {
            await _service.Alterar(pedidoId, model);
        }

        [HttpPut("{pedidoId}/cancelar")]
        public async Task Cancelar(int pedidoId)
        {
            await _service.Cancelar(pedidoId);
        }
    }
}
