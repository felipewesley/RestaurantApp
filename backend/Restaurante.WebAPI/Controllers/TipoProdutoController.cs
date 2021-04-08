using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Restaurante.Repositorio.Services.TipoProduto;
using Restaurante.Repositorio.Services.TipoProduto.Models;

namespace Restaurante.WebAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class TipoProdutoController : ControllerBase
    {
        private readonly TipoProdutoService _service;

        public TipoProdutoController(TipoProdutoService service) => _service = service;

        [HttpGet]
        public async Task<ICollection<BuscaModel>> Buscar()
        {
            return await _service.Listar();
        }
    }
}
