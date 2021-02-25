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
        private Database db = new Database();
        public int ComandaId { get; private set; }
        public int MesaId { get; private set; }
        public DateTime DataHoraEntrada { get; private set; }
        public DateTime? DataHoraSaida { get; private set; }
        public float Valor { get; private set; }
        public bool Paga { get; private set; }
        public int QuantidadeCliente { get; set; }

        // Construtor temporario
        public ComandaService(int id, int mesa, int quantidadeClientes)
        {
            ComandaId = id;
            MesaId = mesa;
            DataHoraEntrada = DateTime.Now;
            DataHoraSaida = null;
            Valor = 0;
            Paga = false;
            QuantidadeCliente = quantidadeClientes;
        }

        public void RegistrarNovaComanda(ComandaFormularioModelCLI comandaModel)
        {

            try
            {
                comandaModel.Validar();

                db.Insert(new Entidades.Comanda()
                {
                    ComandaId = comandaModel.ComandaId,
                    MesaId = comandaModel.MesaId,
                    DataHoraEntrada = DateTime.Now,
                    DataHoraSaida = null,
                    Valor = 0.0F,
                    Paga = false,
                    QuantidadeClientes = comandaModel.QuantidadeCliente
                }, DatabaseControl.Comanda.Comanda);

            } catch (Exception e)
            {
                Console.WriteLine("Ocorreu um erro! " + e.Message);
            }
        }

        public float? EncerrarComanda(ComandaEncerramento comandaModel)
        {
            Valor = comandaModel.CalcularValorComanda();
            Paga = true;
            DataHoraSaida = DateTime.Now;

            try
            {
                db.Update(ComandaId, new Entidades.Comanda()
                {
                    ComandaId = ComandaId,
                    MesaId = MesaId,
                    DataHoraEntrada = DataHoraEntrada,
                    DataHoraSaida = DateTime.Now,
                    Valor = Valor,
                    Paga = true,
                    QuantidadeClientes = QuantidadeCliente
                }, DatabaseControl.Comanda.Comanda);

                return comandaModel.CalcularValorComanda();

            } catch (Exception e)
            {
                Console.WriteLine("Ocorreu um erro! " + e.Message);
            }
            return null;
        }
        

        public TimeSpan CalcularTempoAtividade()
        {
            if (DataHoraSaida == null)
                return DateTime.Now - DataHoraEntrada;

            return (TimeSpan)(DataHoraSaida - DataHoraEntrada);
        }

    }
}
