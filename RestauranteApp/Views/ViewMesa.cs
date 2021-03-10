using System;
using System.Collections.Generic;
using System.Text;
using RestauranteApp.Services.Mesa;
using RestauranteApp.Services.Mesa.Models;

namespace RestauranteApp.Views
{
    class ViewMesa
    {

        private readonly ViewPrograma _viewPrograma;
        private readonly MesaService _mesaService;

        public ViewMesa(MesaService mesaService)
        {
            _mesaService = mesaService;
        }

        public void LabelObterDadosMesa()
        {
            ViewPrinter.Println("\tObtendo dados da mesa \t", ConsoleColor.Yellow);
            
            Console.WriteLine();

            MostrarMesasDisponiveis();

            Console.WriteLine();

            ViewPrinter.Print("\tMesa: ");
        }

        public void MostrarMesasDisponiveis()
        {
            var listaMesas = _mesaService.ObterMesas(true);

            ViewPrinter.Print("\tMesas disponiveis: \n\t");

            ViewPrinter.Print(" # ", ConsoleColor.White, ConsoleColor.DarkGreen);

            foreach (MesaFormularioModel mesa in listaMesas)
            {
                ViewPrinter.Print($"[{ mesa.MesaId }] ", ConsoleColor.White, ConsoleColor.DarkGreen);
            }

            ViewPrinter.Print("# ", ConsoleColor.White, ConsoleColor.DarkGreen);
            Console.WriteLine();

            // ViewPrinter.Print("] ", ConsoleColor.White, ConsoleColor.DarkGreen);
        }

        public int ObterMesaDisponivel(int mesaId)
        {
            bool mesaDisponivel = false;

            while (!mesaDisponivel)
            {
                Console.Clear();

                ViewPrograma.CabecalhoDadosIniciais();

                if (!_mesaService.ValidarMesa(mesaId))
                    ViewPrinter.Println("\t A mesa escolhida não está disponível! ", ConsoleColor.White, ConsoleColor.Red);

                Console.WriteLine();

                LabelObterDadosMesa();
                mesaId = int.Parse(Console.ReadLine());

                if (_mesaService.ValidarMesa(mesaId)) mesaDisponivel = true;
            }

            return mesaId;
        }

        public void LabelObterQuantidadeClientes(int mesaId)
        {
            ViewPrinter.Println("\tObtendo quantidade de clientes \t", ConsoleColor.Yellow);
            
            Console.WriteLine();

            int quantidadeClientes = _mesaService.ObterQuantidadeClientes(mesaId);
            ViewPrinter.Println($"\t  * A mesa [{ mesaId }] comporta, no máximo, { quantidadeClientes } pessoas  ", ConsoleColor.Black, ConsoleColor.Yellow);

            Console.WriteLine();

            ViewPrinter.Print("\tQuantidade de clientes: ");
        }

        public void MostrarQuantidadeClientesSelecionada(int quantidadeClientes)
        {
            // Console.Clear();

            ViewPrograma.ShowSucesso();

            ViewPrinter.Print("\tQUANTIDADE DE CLIENTES: ");
            ViewPrinter.Println($" { quantidadeClientes } ", ConsoleColor.White, ConsoleColor.DarkGreen);
            Console.WriteLine();
            ViewPrograma.MensagemContinuarAtendimento();
        }

        public void MostrarMesaSelecionada(int mesaId)
        {
            // Console.Clear();

            ViewPrograma.ShowSucesso();

            ViewPrinter.Print("\tSELECIONADA: ");
            ViewPrinter.Println($" MESA [ { mesaId } ] ", ConsoleColor.White, ConsoleColor.DarkGreen);
            Console.WriteLine();
            ViewPrograma.MensagemContinuarAtendimento();
        }

        public int ObterQuantidadeClientesValida(int mesaId, int quantidadeClientes)
        {
            bool quantidadeValida = false;

            while (!quantidadeValida)
            {
                Console.Clear();

                ViewPrograma.CabecalhoDadosIniciais();

                if (quantidadeClientes <= 0)
                {
                    ViewPrinter.Println("\t Quantidade de clientes inválida! ", ConsoleColor.White, ConsoleColor.Red);
                } else if (quantidadeClientes > _mesaService.ObterQuantidadeClientes(mesaId))
                {
                    ViewPrinter.Println("\t Esta mesa não comporta esta quantidade de pessoas! ", ConsoleColor.White, ConsoleColor.Red);
                } else
                {
                    ViewPrinter.Println("\t Valor informado inválido! ", ConsoleColor.White, ConsoleColor.Red);
                }

                Console.WriteLine();

                LabelObterQuantidadeClientes(mesaId);
                quantidadeClientes = int.Parse(Console.ReadLine());

                if (quantidadeClientes <= _mesaService.ObterQuantidadeClientes(mesaId) && quantidadeClientes > 0) quantidadeValida = true;
            }

            return quantidadeClientes;
        }
    }
}
