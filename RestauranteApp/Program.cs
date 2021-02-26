using System;
using RestauranteApp.Services.Comanda;
using RestauranteApp.Services.Mesa;
using RestauranteApp.Services.Pedido;
using RestauranteApp.Services.Produto;
using RestauranteApp.Services.Status;
using RestauranteApp.Services.TipoProduto;
using System.IO;
using RestauranteApp.Views;
using RestauranteApp.Services.Comanda.Models;

namespace RestauranteApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Clear();

            ViewPrograma.Welcome();

            Console.Clear();

            // Solicitando dados iniciais
            Console.WriteLine();
            ViewPrinter.Print("\tSEU ATENDIMENTO FOI INICIADO", ConsoleColor.Yellow);

            Console.WriteLine();
            Console.WriteLine();

            ViewPrinter.Println("\tPor favor, informe os seguintes dados solicitados:");

            Console.WriteLine();

            // Leitura e validacao ID Mesa
            ViewPrinter.Print("\tMesa: ");
            int mesaId = int.Parse(Console.ReadLine());

            bool mesaDisponivel = !MesaService.ValidarMesa(mesaId) || !MesaService.MesaOcupada(mesaId);

            if (!mesaDisponivel) mesaId = ViewMesa.ObterMesaDisponivel(mesaId);

            // Leitura e validacao ID Comanda
            ViewPrinter.Print("\tNº Comanda: ");
            int comandaId = int.Parse(Console.ReadLine());

            bool comandaExistente = !MesaService.ValidarMesa(mesaId) || !MesaService.MesaOcupada(mesaId);

            if (!comandaExistente) comandaId = ViewComanda.ObterComandaValida(comandaId);

            // Leitura e validacao Quantidade de Clientes
            ViewPrinter.Print("\tQuantidade de clientes: ");
            int quantidadeClientes = int.Parse(Console.ReadLine());

            bool quantidadeClientesValida = !MesaService.QuantidadeClientesValida(mesaId, quantidadeClientes);

            if (!quantidadeClientesValida) quantidadeClientes = ViewMesa.ObterQuantidadeClientesValida(mesaId, quantidadeClientes);

            var comanda = new ComandaFormularioModelCLI()
            {
                ComandaId = comandaId,
                MesaId = mesaId,
                QuantidadeCliente = quantidadeClientes
            };

            Console.WriteLine("Comanda instanciada!");
        }
    }
}

