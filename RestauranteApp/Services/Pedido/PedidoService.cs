using RestauranteApp.Interfaces;
using RestauranteApp.Services.Pedido.Models;
using RestauranteApp.DatabaseControl;
using System;
using System.Collections.Generic;

namespace RestauranteApp.Services.Pedido
{
    class PedidoService
    {

        public int PedidoId { get; private set; }
        public int ComandaId { get; private set; }
        public int ProdutoId { get; private set; }
        public int Status { get; private set; }
        public int Quantidade { get; private set; }

        private Entidades.Pedido ObterPedidoByIdEntidade(int pedidoId)
        {
            string pedidoCsv = Database.Select(Entidade.Pedido, pedidoId);

            return new Entidades.Pedido().ConverterEmEntidade(pedidoCsv);
        }

        public void RegistrarNovoPedido(PedidoFormularioModel pedidoModel)
        {
            try
            {
                pedidoModel.Validar();

                Database.Insert(new Entidades.Pedido()
                {
                    PedidoId = 1,
                    ComandaId = ComandaId,
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

        public void CancelarPedido(int pedidoId)
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

        public PedidoFormularioModel ObterPedido(int pedidoId)
        {
            var pedido = ObterPedidoByIdEntidade(pedidoId);

            return new PedidoFormularioModel()
            {
                ProdutoId = pedido.ProdutoId,
                Quantidade = pedido.Quantidade
            };
        }

        public List<PedidoRealizadoModel> ObterPedidosPorComanda(int comandaId, bool validarEntregues = false)
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
                        ProdutoId = pedidoEntidade.ProdutoId,
                        Quantidade = pedidoEntidade.Quantidade,
                        Status = pedidoEntidade.Quantidade
                    });
                }
            }

            return pedidosRealizados;
        }

        public int VerificarStatusPedido(int pedidoId)
        {
            return ObterPedidoByIdEntidade(pedidoId).Status;
        }
    }
}
