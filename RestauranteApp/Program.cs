using System;
using RestauranteApp.Services.Comanda;
using RestauranteApp.Services.Mesa;
using RestauranteApp.Services.Pedido;
using RestauranteApp.Services.Produto;
using RestauranteApp.Services.Status;
using RestauranteApp.Services.TipoProduto;
using System.IO;
using RestauranteApp.Views;

namespace RestauranteApp
{
    class Program
    {
        static void Main(string[] args)
        {
            ViewPrograma.Welcome();

            Console.Clear();

            // Solicitando dados iniciais
            Console.WriteLine();
            ViewPrinter.Print("\tSEU ATENDIMENTO FOI INICIADO", ConsoleColor.Yellow);

            Console.WriteLine();
            Console.WriteLine();

            ViewPrinter.Println("\tPor favor, informe os seguintes dados solicitados:");

            Console.WriteLine();

            ViewPrinter.Print("\tMesa: ");

            int numeroMesa = int.Parse(Console.ReadLine());

            Console.WriteLine();

            ViewPrinter.Print("\tQuantidade de rodizios: ");

            int quantidadeRodizios = int.Parse(Console.ReadLine());
        }
    }
}

