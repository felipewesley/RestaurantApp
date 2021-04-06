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

        [HttpGet("{produtoId}")]
        public async Task<BuscaModel> Obter(int produtoId)
        {
            return await _service.Obter(produtoId);
        }

        [HttpGet("buscar")]
        public async Task<ICollection<BuscaModel>> Buscar()
        {
            return await _service.Buscar();
        }

        [HttpGet("buscar/{tipoProdutoId}")]
        public async Task<ICollection<BuscaModel>> BuscarPorTipo(int tipoProdutoId)
        {
            return await _service.BuscarPorTipo(tipoProdutoId);
        }
    }
}
