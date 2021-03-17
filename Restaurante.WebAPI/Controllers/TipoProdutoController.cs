using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Restaurante.Repositorio.Services.TipoProduto;
using Restaurante.Repositorio.Services.TipoProduto.Models;

namespace Restaurante.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TipoProdutoController : ControllerBase
    {

        private readonly TipoProdutoService _service;

        public TipoProdutoController(TipoProdutoService service) => _service = service;

        // GET api/<TipoProdutoController>/5
        [HttpGet("{id}")]
        public async Task<string> Obter(int id)
        {
            return await _service.ObterTipoProduto(id);
        }

        // GET api/<TipoProdutoController>/Buscar
        [HttpGet("Buscar")]
        public async Task<ICollection<TipoProdutoModel>> Buscar()
        {
            return await _service.BuscarTiposProduto();
        }
    }
}
