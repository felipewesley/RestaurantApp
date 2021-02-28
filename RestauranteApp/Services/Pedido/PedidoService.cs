using RestauranteApp.Interfaces;
using RestauranteApp.Services.Pedido.Models;
using RestauranteApp.DatabaseControl;
using System;
using System.Collections.Generic;

namespace RestauranteApp.Services.Pedido
{
    class PedidoService
    {

        private static Entidades.Pedido ObterPedidoByIdEntidade(int pedidoId)
        {
            string pedidoCsv = Database.Select(Entidade.Pedido, pedidoId);

            return new Entidades.Pedido().ConverterEmEntidade(pedidoCsv);
        }

        public static void RegistrarNovoPedido(PedidoFormularioModel pedidoModel)
        {
            try
            {
                pedidoModel.Validar();

                Database.Insert(new Entidades.Pedido()
                {
                    PedidoId = 1,
                    ComandaId = pedidoModel.ComandaId,
                    ProdutoId = pedidoModel.ProdutoId,
                    // Status = StatusEnum.Produzindo,
                    Status = 3,
                    Quantidade = pedidoModel.Quantidade
                }, Entidade.Pedido);

            } catch (Exception e)
            {
                Console.WriteLine("Ocorreu um erro! " + e.Message);
            }
        }

        public static void CancelarPedido(int pedidoId)
        {
            // Implementar chamada de ID comanda
            int comandaId = 123;

            string pedidoCsv = Database.Select(Entidade.Pedido, pedidoId);
            // Criar um pedido que implemente a interface ParseToEntity

            Database.Update(pedidoId, new Entidades.Pedido()
            {
                PedidoId = pedidoId,
                ComandaId = comandaId,
                //ProdutoId
            }, Entidade.Pedido);
        }

        public static PedidoFormularioModel ObterPedido(int pedidoId)
        {
            var pedido = ObterPedidoByIdEntidade(pedidoId);

            return new PedidoFormularioModel()
            {
                ComandaId = pedido.ComandaId,
                ProdutoId = pedido.ProdutoId,
                Quantidade = pedido.Quantidade
            };
        }

        public static List<PedidoRealizadoModel> ObterPedidosPorComanda(int comandaId, bool validarEntregues = false)
        {
            List<PedidoRealizadoModel> pedidosRealizados = new List<PedidoRealizadoModel>();

            string[] pedidos = Database.Select(Entidade.Pedido);

            foreach (string pedidoCsv in pedidos)
            {
                var pedidoEntidade = new Entidades.Pedido().ConverterEmEntidade(pedidoCsv);

                if (pedidoEntidade.ComandaId == comandaId)
                {
                    if (validarEntregues)
                    {
                        if (pedidoEntidade.Status != 3) continue;
                    }
                    pedidosRealizados.Add(new PedidoRealizadoModel()
                    {
                        PedidoId = pedidoEntidade.PedidoId,
                        ProdutoId = pedidoEntidade.ProdutoId,
                        Quantidade = pedidoEntidade.Quantidade,
                        Status = pedidoEntidade.Status
                    });
                }
            }

            return pedidosRealizados;
        }

        public static int VerificarStatusPedido(int pedidoId)
        {
            return ObterPedidoByIdEntidade(pedidoId).Status;
        }
    }
}
