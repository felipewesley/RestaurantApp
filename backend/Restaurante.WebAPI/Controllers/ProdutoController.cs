using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Restaurante.Repositorio.Services.Produto;
using Restaurante.Repositorio.Services.Produto.Models;

namespace Restaurante.WebAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ProdutoController : ControllerBase
    {

        private readonly ProdutoService _service;

        public ProdutoController(ProdutoService service) => _service = service;

        [HttpGet]
        public async Task<ICollection<ProdutoModel>> Buscar()
        {
            return await _service.Buscar();
        }

        [HttpGet("{produtoId}")]
        public async Task<ProdutoModel> Obter(int produtoId)
        {
            return await _service.Obter(produtoId);
        }

        [HttpGet("tipo/{tipoProdutoId}")]
        public async Task<ICollection<ProdutoModel>> BuscarPorTipo(int tipoProdutoId)
        {
            return await _service.BuscarPorTipo(tipoProdutoId);
        }
    }
}
