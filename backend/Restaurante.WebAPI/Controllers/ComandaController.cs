using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Restaurante.Repositorio.Services.Comanda;
using Restaurante.Repositorio.Services.Comanda.Models;

namespace Restaurante.WebAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ComandaController : ControllerBase
    {
        private readonly ComandaService _service;

        public ComandaController(ComandaService service) => _service = service;

        [HttpGet("{comandaId}")]
        public async Task<ComandaModel> Obter(int comandaId)
        {
            return await _service.Obter(comandaId);
        }

        [HttpGet("{mesaId}/retomar")]
        public async Task<int> Retomar(int mesaId)
        {
            return await _service.Retomar(mesaId);
        }

        [HttpPost]
        public async Task<int> Registrar(FormularioModel model)
        {
            return await _service.Registrar(model);
        }

        [HttpPut("{comandaId}/encerrar")]
        public async Task Encerrar(int comandaId, EncerrarModel model)
        {
            await _service.Encerrar(comandaId, model);
        }
    }
}
