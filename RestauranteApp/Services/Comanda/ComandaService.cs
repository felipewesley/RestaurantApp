using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using RestauranteApp.Interfaces;
using RestauranteApp.DatabaseControl;
using RestauranteApp.Contexto;
using RestauranteApp.Services.Mesa;
using RestauranteApp.Services.Pedido;
using RestauranteApp.Services.Comanda.Models;

namespace RestauranteApp.Services.Comanda
{
    class ComandaService
    {

        public static void RegistrarComanda(ComandaFormularioModelCLI comandaModel)
        {

            var context = new RestauranteContext();

            try
            {
                comandaModel.Validar();

                context.Comanda.Add(new Entidades.Comanda
                {
                    ComandaId = comandaModel.ComandaId,
                    MesaId = comandaModel.MesaId,
                    DataHoraEntrada = DateTime.Now,
                    DataHoraSaida = null,
                    Valor = 0.0F,
                    Paga = false,
                    QuantidadeClientes = comandaModel.QuantidadeCliente
                });

                if (context.SaveChanges() <= 0)
                    throw new Exception("A comanda não pode ser salva!");

            } catch (Exception e)
            {
                Console.WriteLine("Ocorreu um erro ao registrar a comanda! " + e.Message);
            }
        }

        public static void EncerrarComanda(int comandaId, bool porcentagemGarcom = false)
        {
            var context = new RestauranteContext();

            var comanda = context.Comanda
                            .Where(c => c.ComandaId == comandaId)
                            .FirstOrDefault();

            _ = comanda ?? throw new Exception("Nenhuma comanda foi encontrada");

            comanda.Paga = true;
            comanda.DataHoraSaida = DateTime.Now;
            comanda.Valor = ObterComandaResumida(comandaId).Valor;

            if (context.SaveChanges() <= 0)
                throw new Exception("Não foi possível encerrar a comanda!");
        }
        
        /*
        public static TimeSpan CalcularTempoAtividade(int comandaId)
        {
            var context = new RestauranteContext();

            var comanda = context.Comanda
                            .Where(c => c.ComandaId == comandaId)
                            .FirstOrDefault();

            if (comanda.DataHoraSaida == null || comanda.DataHoraSaida > comanda.DataHoraEntrada)
                return DateTime.Now.Subtract(comanda.DataHoraEntrada);

            return (TimeSpan)comanda.DataHoraSaida.Subtract(comanda.DataHoraEntrada);
        }
        */

        public static ComandaResumidaModel ObterComandaResumida(int comandaId)
        {
            var context = new RestauranteContext();

            var valorRodizio = MesaService.ObterValorRodizio();

            var comanda = context.Comanda
                        .AsQueryable()
                        .Include(c => c.Pedidos)
                        .ThenInclude(c => c.Produto)
                        .Where(c => c.ComandaId == comandaId)
                        .Select(c => new ComandaResumidaModel()
                        {
                            MesaId = c.MesaId,
                            DataHoraEntrada = c.DataHoraEntrada,
                            QuantidadeClientes = c.QuantidadeClientes,
                            Valor = c.Pedidos.Sum(p => p.Quantidade * p.Produto.Valor) + c.QuantidadeClientes * valorRodizio
                        })
                        .FirstOrDefault();

            return comanda;
        }
    }
}
