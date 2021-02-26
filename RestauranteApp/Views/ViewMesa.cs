using System;
using System.Collections.Generic;
using System.Text;
using RestauranteApp.Services.Mesa;

namespace RestauranteApp.Views
{
    class ViewMesa
    {
        public static int ObterMesaDisponivel(int mesaId)
        {
            bool mesaDisponivel = false;

            while (!mesaDisponivel)
            {
                Console.Clear();
                Console.WriteLine();

                if (!MesaService.ValidarMesa(mesaId))
                    ViewPrinter.Println("\tA mesa escolhida não existe! Tente novamente.", ConsoleColor.White, ConsoleColor.Red);
                else
                    ViewPrinter.Println("\tA mesa escolhida não está disponível! Tente novamente.", ConsoleColor.White, ConsoleColor.Red);

                Console.WriteLine();

                ViewPrinter.Print("\tMesa: ");
                mesaId = int.Parse(Console.ReadLine());

                if (MesaService.ValidarMesa(mesaId) && !MesaService.MesaOcupada(mesaId)) mesaDisponivel = true;
            }

            return mesaId;
        }

        public static int ObterQuantidadeClientesValida(int mesaId, int quantidadeClientes)
        {
            bool quantidadeValida = false;

            while (!quantidadeValida)
            {
                Console.Clear();
                Console.WriteLine();

                if (quantidadeClientes <= 0)
                {
                    ViewPrinter.Println("\tQuantidade de clientes inválida! Tente novamente.", ConsoleColor.White, ConsoleColor.Red);
                } else if (quantidadeClientes > MesaService.ObterQuantidadeClientes(mesaId))
                {
                    ViewPrinter.Println("\tEsta mesa não comporta esta quantidade de pessoas! Tente novamente.", ConsoleColor.White, ConsoleColor.Red);
                } else
                {
                    ViewPrinter.Println("\tValor informado inválido! Tente novamente.", ConsoleColor.White, ConsoleColor.Red);
                }

                Console.WriteLine();

                ViewPrinter.Print("\tQuantidade de clientes: ");
                quantidadeClientes = int.Parse(Console.ReadLine());

                if (quantidadeClientes <= MesaService.ObterQuantidadeClientes(mesaId) && quantidadeClientes > 0) quantidadeValida = true;
            }

            return quantidadeClientes;
        }
    }
}
