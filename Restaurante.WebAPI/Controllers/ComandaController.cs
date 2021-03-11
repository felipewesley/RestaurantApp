using Microsoft.AspNetCore.Mvc;
using Restaurante.Repositorio.Services.Comanda;
using Restaurante.Repositorio.Services.Comanda.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Restaurante.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ComandaController : ControllerBase
    {

        private readonly IComandaService _service;

        public ComandaController(IComandaService service)
        {
            _service = service;
        }

        // GET api/<ComandaController>/5
        [HttpGet("ObterComandaResumida/{id}")]
        public ComandaResumidaModel ObterComandaResumida(int id)
        {
            return _service.ObterComandaResumida(id);
        }

        // GET api/<ComandaController>/5
        [HttpGet("ObterComandaCompleta/{id}")]
        public async Task<ComandaCompletaModel> ObterComandaCompleta(int id)
        {
            return await _service.ObterComandaCompleta(id);
        }

        // POST api/<ComandaController>
        [HttpPost("NovaComanda")]
        public void Post(ComandaFormularioModel comanda)
        {
            _service.RegistrarComanda(comanda);
        }

        // PUT api/<ComandaController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<ComandaController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
