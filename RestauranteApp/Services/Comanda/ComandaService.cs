using System;
using RestauranteApp.DatabaseControl;
using RestauranteApp.Entidades;
using RestauranteApp.Services.Comanda.Models;
using RestauranteApp.Services.Pedido;
using RestauranteApp.Services.Produto;
using RestauranteApp.Services.Mesa;
using RestauranteApp.Interfaces;
using System.Globalization;

namespace RestauranteApp.Services.Comanda
{
    class ComandaService
    {

        private static Entidades.Comanda ObterComandaEntidade(int comandaId)
        {
            string comandaCsv = Database.Select(Entidade.Comanda, comandaId);

            if (comandaCsv == null || comandaCsv == string.Empty) return null;

            return new Entidades.Comanda().ConverterEmEntidade(comandaCsv);
        }

        public static bool JaExisteComanda(int comandaId)
        {
            return ObterComandaEntidade(comandaId) != null;
        }

        public static void RegistrarComanda(ComandaFormularioModelCLI comandaModel)
        {

            try
            {
                comandaModel.Validar();

                Database.Insert(new Entidades.Comanda()
                {
                    ComandaId = comandaModel.ComandaId,
                    MesaId = comandaModel.MesaId,
                    DataHoraEntrada = DateTime.Now,
                    //DataHoraSaida = null,
                    Valor = 0.0F,
                    Paga = false,
                    QuantidadeClientes = comandaModel.QuantidadeCliente
                }, Entidade.Comanda);

            } catch (Exception e)
            {
                Console.WriteLine("Ocorreu um erro! " + e.Message);
            }
        }

        public static void EncerrarComanda(int comandaId)
        {
            var comanda = ObterComandaEntidade(comandaId);

            comanda.Paga = true;
            comanda.DataHoraSaida = DateTime.Now;
            comanda.Valor = CalcularValorComanda(comandaId);

            Database.Insert(comanda, Entidade.Comanda);
        }
        
        public static TimeSpan CalcularTempoAtividade(int comandaId)
        {
            var comanda = ObterComandaEntidade(comandaId);

            if (comanda.DataHoraSaida == null || comanda.DataHoraSaida > comanda.DataHoraEntrada)
                return DateTime.Now.Subtract(comanda.DataHoraEntrada);

            return comanda.DataHoraSaida.Subtract(comanda.DataHoraEntrada);
        }

        public static ComandaCompletaModel ObterComandaCompleta(int comandaId)
        {
            var comanda = ObterComandaEntidade(comandaId);

            return new ComandaCompletaModel()
            {
                ComandaId = comanda.ComandaId,
                MesaId = comanda.MesaId,
                DataHoraEntrada = comanda.DataHoraEntrada,
                DataHoraSaida = comanda.DataHoraSaida,
                Valor = CalcularValorComanda(comandaId),
                Paga = comanda.Paga,
                QuantidadeClientes = comanda.QuantidadeClientes
            };
        }

        public static ComandaResumidaModel ObterComandaResumida(int comandaId)
        {
            var comanda = ObterComandaEntidade(comandaId);

            return new ComandaResumidaModel()
            {
                MesaId = comanda.MesaId,
                DataHoraEntrada = comanda.DataHoraEntrada,
                QuantidadeClientes = comanda.QuantidadeClientes,
                Valor = CalcularValorComanda(comandaId)
            };
        }

        public static float CalcularValorComanda(int comandaId, bool porcentagemGarcom = false)
        {
            var comanda = ObterComandaEntidade(comandaId);
            var pedidos = PedidoService.ObterPedidosPorComanda(comandaId);

            float valorTotal = comanda.QuantidadeClientes * MesaService.ValorRodizio;

            foreach (var pedido in pedidos)
            {
                // Em andamento ou entregue
                if (pedido.Status == 1 || pedido.Status == 3)
                {
                    var produto = ProdutoService.ObterProduto(pedido.ProdutoId);
                    
                    if (produto.Valor == 0) continue;

                    valorTotal += pedido.Quantidade * produto.Valor;
                }
            }

            return porcentagemGarcom ? (valorTotal * 1.1F) : valorTotal;
        }

    }
}
