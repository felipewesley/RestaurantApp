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

        public Entidades.Comanda ObterComandaEntidade(int comandaId)
        {
            string comandaCsv = Database.Select(Entidade.Comanda, comandaId);

            return new Entidades.Comanda().ConverterEmEntidade(comandaCsv);
        }

        public void RegistrarComanda(ComandaFormularioModelCLI comandaModel)
        {

            try
            {
                comandaModel.Validar();

                Database.Insert(new Entidades.Comanda()
                {
                    ComandaId = comandaModel.ComandaId,
                    MesaId = comandaModel.MesaId,
                    DataHoraEntrada = DateTime.Now,
                    DataHoraSaida = null,
                    Valor = 0.0F,
                    Paga = false,
                    QuantidadeClientes = comandaModel.QuantidadeCliente
                }, Entidade.Comanda);

            } catch (Exception e)
            {
                Console.WriteLine("Ocorreu um erro! " + e.Message);
            }
        }

        public void EncerrarComanda(int comandaId)
        {
            var comanda = ObterComandaEntidade(comandaId);

            comanda.Paga = true;
            comanda.DataHoraSaida = DateTime.Now;
            comanda.Valor = CalcularValorComanda(comandaId);

            Database.Insert(comanda, Entidade.Comanda);
        }
        
        public TimeSpan CalcularTempoAtividade(int comandaId)
        {
            var comanda = ObterComandaEntidade(comandaId);

            if (comanda.DataHoraSaida == null)
                return DateTime.Now - comanda.DataHoraEntrada;

            return (TimeSpan)(comanda.DataHoraSaida - comanda.DataHoraEntrada);
        }

        public ComandaCompletaModel ObterComanda(int comandaId)
        {
            var comanda = ObterComandaEntidade(comandaId);

            return new ComandaCompletaModel()
            {
                ComandaId = comanda.ComandaId,
                MesaId = comanda.MesaId,
                DataHoraEntrada = comanda.DataHoraEntrada,
                DataHoraSaida = comanda.DataHoraSaida,
                Valor = comanda.Valor,
                Paga = comanda.Paga,
                QuantidadeClientes = comanda.QuantidadeClientes
            };
        }

        public float CalcularValorComanda(int comandaId)
        {
            return 0.0F;
        }

    }
}
