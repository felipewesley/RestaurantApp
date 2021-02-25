using RestauranteApp.Interfaces;
using RestauranteApp.Services.Pedido.Models;
using RestauranteApp.DatabaseControl;
using System;

namespace RestauranteApp.Services.Pedido
{
    class PedidoService
    {
        private Database db = new Database();
        public int PedidoId { get; private set; }
        public int ComandaId { get; private set; }
        public int ProdutoId { get; private set; }
        public int Status { get; private set; }
        public int Quantidade { get; private set; }

        public void RegistrarNovoPedido(PedidoFormularioModel pedidoModel)
        {
            try
            {
                pedidoModel.Validar();

                db.Insert(new Entidades.Pedido()
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

            string pedidoCsv = db.Select(Entidade.Pedido, pedidoId);
            // Criar um pedido que implemente a interface ParseToEntity

            db.Update(pedidoId, new Entidades.Pedido()
            {
                PedidoId = pedidoId,
                ComandaId = comandaId,
                //ProdutoId
            }, Entidade.Pedido);
        }
    }
}
