using RestauranteApp.Interfaces;
using RestauranteApp.Services.Pedido.Models;
using RestauranteApp.DatabaseControl;
using System;
using System.Collections.Generic;
using System.IO;
using RestauranteApp.Contexto;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace RestauranteApp.Services.Pedido
{
    class PedidoService
    {

        private readonly RestauranteContext _context;

        public PedidoService(RestauranteContext context)
        {
            _context = context;
        }

        /*
        private static int ObterProximoId()
        {
            var context = new RestauranteContext();

            return context.Pedido.ToList().Count + 1;
        }
        */

        public void RegistrarNovoPedido(PedidoFormularioModel pedidoModel)
        {

            _context.Pedido.Add(new Entidades.Pedido()
            {
                ComandaId = pedidoModel.ComandaId,
                ProdutoId = pedidoModel.ProdutoId,
                StatusId = 1, //Em andamento
                Quantidade = pedidoModel.Quantidade
            });

            if (_context.SaveChanges() <= 0)
                throw new Exception("Não foi possivel salvar o pedido! ");
        }

        public void CancelarPedido(int pedidoId)
        {

            var pedido = _context.Pedido
                        .Include(p => p.Status)
                        .Where(p => p.PedidoId == pedidoId)
                        .FirstOrDefault();

            pedido.Status.StatusId = 3;

            if (_context.SaveChanges() <= 0)
                throw new Exception("Não foi possível cancelar o pedido!");
        }

        public PedidoFormularioModel ObterPedido(int pedidoId)
        {

            var pedido = _context.Pedido
                        .Where(p => p.PedidoId == pedidoId)
                        .FirstOrDefault();

            return new PedidoFormularioModel()
            {
                ComandaId = pedido.ComandaId,
                ProdutoId = pedido.ProdutoId,
                Quantidade = pedido.Quantidade
            };
        }

        public ICollection<PedidoRealizadoModel> ObterPedidosPorComanda(int comandaId, bool validarEntregues = false)
        {

            var listaPedidos = _context.Pedido
                    .AsNoTracking()
                    .Include(p => p.Status)
                    .Include(p => p.Produto)
                    .Where(p => p.ComandaId == comandaId)
                    .Select(p => new PedidoRealizadoModel()
                    {
                        PedidoId = p.PedidoId,
                        Produto = p.Produto,
                        Quantidade = p.Quantidade,
                        Status = p.Status
                    });

            return validarEntregues ? listaPedidos.Where(p => p.Status.StatusId == 2).ToList() : listaPedidos.ToList();
        }

        public bool VerificarPedidosEmAberto(int comandaId)
        {
            // return context.Pedido.Find();
            // return _context.Pedido.Where(p => p.ComandaId == comandaId && p.StatusId == 1).Count() > 0;
            return _context.Pedido.Any(p => p.ComandaId == comandaId && p.StatusId == 1);
        }
    }
}
