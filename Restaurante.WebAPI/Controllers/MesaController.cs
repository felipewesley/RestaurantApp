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

        private readonly IMesaService _service;

        public MesaController(IMesaService service) => _service = service;

        // GET api/<MesaController>/Obter/5
        [HttpGet("Obter/{id}")]
        public async Task<MesaModel> Obter(int id)
        {
            return await _service.ObterMesa(id);
        }

        // GET api/<MesaController>/Listar
        [HttpGet("Buscar")]
        public async Task<ICollection<MesaModel>> Buscar()
        {
            return await _service.BuscarMesas();
        }

        // PUT api/<MesaController>/Ocupar/5
        [HttpPut("Ocupar/{id}")]
        public async Task Ocupar(int id)
        {
            await _service.AtualizarStatusMesa(id, true);
        }

        // PUT api/<MesaController>/Desocupar/5
        [HttpPut("Desocupar/{id}")]
        public async Task Desocupar(int id)
        {
            await _service.AtualizarStatusMesa(id, false);
        }
    }
}
