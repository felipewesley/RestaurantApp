using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Restaurante.Repositorio.Services.Mesa;
using Restaurante.Repositorio.Services.Mesa.Models;

namespace Restaurante.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MesaController : ControllerBase
    {

        private readonly MesaService _service;

        public MesaController(MesaService service) => _service = service;

        [HttpGet("{mesaId}")]
        public async Task<MesaModel> Obter(int mesaId)
        {
            return await _service.Obter(mesaId);
        }

        [HttpGet("listar")]
        public async Task<ICollection<MesaModel>> Listar()
        {
            return await _service.Listar();
        }

        [HttpPut("ocupar/{mesaId}")]
        public async Task Ocupar(int mesaId)
        {
            await _service.AtualizarStatus(mesaId, true);
        }

        [HttpPut("desocupar/{mesaId}")]
        public async Task Desocupar(int mesaId)
        {
            await _service.AtualizarStatus(mesaId, false);
        }
    }
}
