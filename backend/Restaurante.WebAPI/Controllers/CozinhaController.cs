using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Restaurante.Repositorio.Services.Cozinha;
using Restaurante.Repositorio.Services.Cozinha.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Restaurante.WebAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CozinhaController : ControllerBase
    {
        private readonly CozinhaService _service;

        public CozinhaController(CozinhaService service) => _service = service;

        [HttpGet("pedido/pendentes")]
        public async Task<ICollection<ListagemCozinhaModel>> BuscarPedidosPendentes()
        {
            return await _service.BuscarPendentes();
        }

        // [HttpPost]
        // public async Task<> CriarUsuario() { }

        [HttpPost("autenticar")]
        public async Task<HttpStatusCode> AutenticarUsuario(LoginModel model)
        {
            return await _service.AutenticarUsuario(model);
        }

        [HttpPut("pedido/{pedidoId}/entregar")]
        public async Task<int> EntegarPedido(int pedidoId)
        {
            return await _service.EntregarPedido(pedidoId);
        }
    }
}
