using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Restaurante.Repositorio.Services.Comanda;
using Restaurante.Repositorio.Services.Comanda.Models;

namespace Restaurante.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ComandaController : ControllerBase
    {

        private readonly IComandaService _service;

        public ComandaController(IComandaService service) => _service = service;

        // GET api/<ComandaController>/ObterComandaResumida/5
        [HttpGet("ObterComandaResumida/{id}")]
        public async Task<ComandaResumidaModel> ObterComandaResumida(int id)
        {
            return await _service.ObterComandaResumida(id);
        }

        // GET api/<ComandaController>/ObterComandaCompleta/5
        [HttpGet("ObterComandaCompleta/{id}")]
        public async Task<ComandaCompletaModel> ObterComandaCompleta(int id)
        {
            return await _service.ObterComandaCompleta(id);
        }

        // POST api/<ComandaController>/Registrar
        [HttpPost("Registrar")]
        public async Task Registrar(ComandaFormularioModel comanda)
        {
            await _service.RegistrarComanda(comanda);
        }

        // PUT api/<ComandaController>/Encerrar/5/true?
        [HttpPut("EncerrarComanda/{id}/{porcentagemGarcom?}")]
        public async Task Encerrar(int id, bool porcentagemGarcom = false)
        {
            await _service.EncerrarComanda(id, porcentagemGarcom);
        }
    }
}
