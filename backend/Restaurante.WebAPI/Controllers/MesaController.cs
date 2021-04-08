using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Restaurante.Repositorio.Services.Mesa;
using Restaurante.Repositorio.Services.Mesa.Models;
using Restaurante.Repositorio.Enum;

namespace Restaurante.WebAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class MesaController : ControllerBase
    {

        private readonly MesaService _service;

        public MesaController(MesaService service) => _service = service;

        [HttpGet]
        public async Task<ICollection<BuscarModel>> Listar()
        {
            return await _service.Buscar();
        }

        [HttpGet("{mesaId}")]
        public async Task<BuscarModel> Obter(int mesaId)
        {
            return await _service.Obter(mesaId);
        }

        [HttpPut("{mesaId}/ocupar")]
        public async Task Ocupar(int mesaId)
        {
            await _service.AtualizarStatus(mesaId, MesaEnum.Ocupar);
        }

        [HttpPut("{mesaId}/desocupar")]
        public async Task Desocupar(int mesaId)
        {
            await _service.AtualizarStatus(mesaId, MesaEnum.Desocupar);
        }
    }
}
