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

        private static int ObterProximoId()
        {
            var context = new RestauranteContext();

            return context.Pedido.ToList().Count + 1;
        }

        public static void RegistrarNovoPedido(PedidoFormularioModel pedidoModel)
        {

            var context = new RestauranteContext();

            context.Pedido.Add(new Entidades.Pedido()
            {
                PedidoId = ObterProximoId(),
                ComandaId = pedidoModel.ComandaId,
                ProdutoId = pedidoModel.ProdutoId,
                StatusId = 1, //Em andamento
                Quantidade = pedidoModel.Quantidade
            });

            if (context.SaveChanges() <= 0)
                throw new Exception("Não foi possivel salvar o pedido! ");
        }

        public static void CancelarPedido(int pedidoId)
        {
            var context = new RestauranteContext();

            var pedido = context.Pedido
                        .Include(p => p.Status)
                        .Where(p => p.PedidoId == pedidoId)
                        .FirstOrDefault();

            pedido.Status.StatusId = 3;

            if (context.SaveChanges() <= 0)
                throw new Exception("Não foi possível cancelar o pedido!");
        }

        public static PedidoFormularioModel ObterPedido(int pedidoId)
        {
            var context = new RestauranteContext();

            var pedido = context.Pedido
                        .Where(p => p.PedidoId == pedidoId)
                        .FirstOrDefault();

            return new PedidoFormularioModel()
            {
                ComandaId = pedido.ComandaId,
                ProdutoId = pedido.ProdutoId,
                Quantidade = pedido.Quantidade
            };
        }

        public static List<PedidoRealizadoModel> ObterPedidosPorComanda(int comandaId, bool validarEntregues = false)
        {

            var context = new RestauranteContext();

            var listaPedidos = context.Pedido
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

        public static bool VerificarPedidosEmAberto(int comandaId)
        {
            var context = new RestauranteContext();

            // return context.Pedido.Find();
            return context.Pedido.Where(p => p.ComandaId == comandaId && p.StatusId == 1).Count() > 0;
        }
    }
}
