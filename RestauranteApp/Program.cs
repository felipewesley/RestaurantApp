using System;
using RestauranteApp.Views;
using RestauranteApp.Services.Mesa;
using RestauranteApp.Services.Comanda;
using RestauranteApp.Services.Comanda.Models;
using RestauranteApp.DatabaseControl;

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
            // Ocupando mesa no banco de dados
            MesaService.AtualizarStatusMesa(mesaId, true);
            Console.Clear();

            // Leitura e validacao ID Comanda
            ViewPrograma.CabecalhoDadosIniciais();
            ViewComanda.LabelObterDadosComanda();
            int comandaId = int.Parse(Console.ReadLine());
            bool comandaDisponivel = ComandaService.ValidarComanda(comandaId) && ComandaService.ComandaDisponivel(comandaId);
            if (!comandaDisponivel) comandaId = ViewComanda.ObterComandaDisponivel(comandaId);
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

            // Executa um loop mostrando o menu principal enquanto nao for explicitamente encerrado
            ViewPrograma.MostrarMenu(comandaId, tipoExibicaoCardapio);

            // Desocupando mesa no banco de dados
            MesaService.AtualizarStatusMesa(mesaId, false);

        }
    }
}

