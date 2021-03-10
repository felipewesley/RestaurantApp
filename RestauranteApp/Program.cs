using System;
using RestauranteApp.Views;
using RestauranteApp.Services.Mesa;
using RestauranteApp.Services.Mesa.Models;
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

            // Criação do modelo de mesa recebido via formulario
            var mesa = new MesaFormularioModel();

            // Leitura e validacao ID Mesa
            ViewPrograma.CabecalhoDadosIniciais();
            ViewMesa.LabelObterDadosMesa();
            mesa.MesaId = int.Parse(Console.ReadLine());
            if (!MesaService.ValidarMesa(mesa.MesaId)) mesa.MesaId = ViewMesa.ObterMesaDisponivel(mesa.MesaId);
            ViewMesa.MostrarMesaSelecionada(mesa.MesaId);
            Console.Clear();

            // Criação do modelo de comanda recebido via formulario
            var comanda = new ComandaFormularioModelCLI()
            {
                Mesa = mesa
            };

            // Leitura e validacao ID Comanda
            ViewPrograma.CabecalhoDadosIniciais();
            ViewComanda.LabelObterDadosComanda();
            comanda.ComandaId = int.Parse(Console.ReadLine());
            if (!ComandaService.ValidarComanda(comanda.ComandaId)) comanda.ComandaId = ViewComanda.ObterComandaDisponivel(comanda.ComandaId);
            ViewComanda.MostrarComandaSelecionada(comanda.ComandaId);
            Console.Clear();

            // Leitura e validacao Quantidade de Clientes
            ViewPrograma.CabecalhoDadosIniciais();
            ViewMesa.LabelObterQuantidadeClientes(mesa.MesaId);
            comanda.QuantidadeCliente = int.Parse(Console.ReadLine());
            if (comanda.QuantidadeCliente <= 0 || comanda.QuantidadeCliente > comanda.Mesa.Capacidade) 
                comanda.QuantidadeCliente = ViewMesa.ObterQuantidadeClientesValida(mesa.MesaId, comanda.QuantidadeCliente);
            ViewMesa.MostrarQuantidadeClientesSelecionada(comanda.QuantidadeCliente);
            Console.Clear();

            // Ocupando mesa no banco de dados
            MesaService.AtualizarStatusMesa(comanda.Mesa.MesaId, true);

            // Salvando comanda no banco de dados
            ComandaService.RegistrarComanda(comanda);

            int tipoExibicaoCardapio = ViewPrograma.EscolhaFormatoExibicaoCardapio();

            Console.Clear();

            // Executa um loop mostrando o menu principal enquanto nao for explicitamente encerrado
            ViewPrograma.MostrarMenu(comanda.ComandaId, tipoExibicaoCardapio);

            // Desocupando mesa no banco de dados
            MesaService.AtualizarStatusMesa(comanda.Mesa.MesaId, false);

        }
    }
}

