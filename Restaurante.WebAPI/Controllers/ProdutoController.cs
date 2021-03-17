using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Restaurante.Repositorio.Services.Produto;
using Restaurante.Repositorio.Services.Produto.Models;

namespace Restaurante.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProdutoController : ControllerBase
    {

        private readonly ProdutoService _service;

        public ProdutoController(ProdutoService service) => _service = service;

        // GET api/<ProdutoController>/5
        [HttpGet("{id}")]
        public async Task<ProdutoModel> Obter(int id)
        {
            return await _service.ObterProduto(id);
        }

        // GET api/<ProdutoController>/Buscar
        [HttpGet("Buscar")]
        public async Task<ICollection<ProdutoListagemModel>> Buscar()
        {
            return await _service.BuscarProdutos();
        }

        // GET api/<ProdutoController>/Buscar/5
        [HttpGet("Buscar/{tipoId}")]
        public async Task<ICollection<ProdutoListagemModel>> Buscar(int tipoId)
        {
            return await _service.BuscarProdutos(tipoId);
        }
    }
}
