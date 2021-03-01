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
            ViewPrinter.Print("\tSEU ATENDIMENTO FOI INICIADO", ConsoleColor.Green);
            Console.WriteLine();

            // Leitura e validacao ID Mesa
            ViewPrograma.CabecalhoDadosIniciais();
            ViewMesa.LabelObterDadosMesa();
            int mesaId = int.Parse(Console.ReadLine());
            bool mesaDisponivel = MesaService.ValidarMesa(mesaId) && !MesaService.MesaOcupada(mesaId);
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

            // Criacao do modelo de comanda recebido via formulario
            var comanda = new ComandaFormularioModelCLI()
            {
                ComandaId = comandaId,
                MesaId = mesaId,
                QuantidadeCliente = quantidadeClientes
            };

            int tipoExibicaoCardapio = ViewPrograma.EscolhaFormatoExibicaoCardapio();

            Console.Clear();

            // Salvando comanda no banco de dados
            ComandaService.RegistrarComanda(comanda);

            /*
            // Mostrando comanda resumida antes de iniciar o loop principal do programa
            ViewComanda.MostrarComandaResumida(comandaId);

            ViewPrograma.MensagemContinuarAtendimento();
            */

            // Executa um loop mostrando o menu principal enquanto nao for explicitamente encerrado
            ViewPrograma.MostrarMenu(comandaId, tipoExibicaoCardapio);

        }
    }
}

