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

            // Leitura e validacao ID Mesa
            ViewPrograma.CabecalhoDadosIniciais();
            ViewMesa.LabelObterDadosMesa();
            int mesaId = int.Parse(Console.ReadLine());
            bool mesaDisponivel = !MesaService.ValidarMesa(mesaId) || !MesaService.MesaOcupada(mesaId);
            if (!mesaDisponivel) mesaId = ViewMesa.ObterMesaDisponivel(mesaId);
            ViewMesa.MostrarMesaSelecionada(mesaId);
            Console.Clear();

            // Leitura e validacao ID Comanda
            ViewPrograma.CabecalhoDadosIniciais();
            ViewComanda.LabelObterDadosComanda();
            int comandaId = int.Parse(Console.ReadLine());
            // bool comandaExistente = !ComandaService.JaExisteComanda(comandaId);
            // if (!comandaExistente) comandaId = ViewComanda.ObterComandaValida(comandaId);
            ViewComanda.MostrarComandaSelecionada(comandaId);
            Console.Clear();

            // Leitura e validacao Quantidade de Clientes
            ViewPrograma.CabecalhoDadosIniciais();
            ViewMesa.LabelObterQuantidadeClientes(mesaId);
            int quantidadeClientes = int.Parse(Console.ReadLine());
            bool quantidadeClientesValida = MesaService.QuantidadeClientesValida(mesaId, quantidadeClientes);
            if (!quantidadeClientesValida) quantidadeClientes = ViewMesa.ObterQuantidadeClientesValida(mesaId, quantidadeClientes);
            ViewMesa.MostrarQuantidadeClientesSelecionada(quantidadeClientes);
            Console.Clear();

            var comanda = new ComandaFormularioModelCLI()
            {
                ComandaId = comandaId,
                MesaId = mesaId,
                QuantidadeCliente = quantidadeClientes
            };

            ComandaService.RegistrarComanda(comanda);

            // Mostrando o cabecalho da comanda
            Console.WriteLine();
            ViewComanda.MostrarComandaResumida(comandaId);
            Console.WriteLine();

            Console.ReadLine();


            // Iniciar processo de fazer pedidos

        }
    }
}

