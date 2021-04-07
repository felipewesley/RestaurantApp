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
        private readonly ComandaService _service;

        public ComandaController(ComandaService service) => _service = service;

        [HttpGet("obter/resumida/{comandaId}")]
        public async Task<ResumidaModel> ObterResumida(int comandaId)
        {
            return await _service.ObterResumida(comandaId);
        }

        [HttpGet("obter/completa/{comandaId}")]
        public async Task<CompletaModel> ObterCompleta(int comandaId)
        {
            return await _service.ObterCompleta(comandaId);
        }

        [HttpPost]
        public async Task<int> Registrar(FormularioModel model)
        {
            return await _service.Registrar(model);
        }

        [HttpPut]
        public async Task Alterar(AlterarModel model)
        {
            await _service.Alterar(model);
        }

        [HttpDelete("{comandaId}/{porcentagemGarcom?}")]
        public async Task Encerrar(int comandaId, bool porcentagemGarcom = false)
        {
            await _service.Encerrar(comandaId, porcentagemGarcom);
        }
    }
}
