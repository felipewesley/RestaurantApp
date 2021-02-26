using System;
using System.Collections.Generic;
using System.Text;

namespace RestauranteApp.Views
{
    class ViewPrograma
    {
        public static void Welcome()
        {
            Console.WriteLine();

            ViewPrinter.Println("\tOlá, seja bem vindo ao");
            
            Console.WriteLine();
            
            ViewPrinter.Println("\t   RESTAURANTE SUTEKINA RANCH   ", ConsoleColor.Black, ConsoleColor.Yellow);
            
            Console.WriteLine();
            
            ViewPrinter.Println("\tEstamos no ramo há aproximadamente 10 anos!");
            ViewPrinter.Println("\tTrabalhamos apenas com rodizio de comida japonesa.");
            
            Console.WriteLine();
            
            ViewPrinter.Print("\tNosso espaço conta com ");
            ViewPrinter.Print("16 mesas", ConsoleColor.Yellow);
            ViewPrinter.Print(", sendo ");
            ViewPrinter.Println("4 pessoas por mesa.", ConsoleColor.Yellow);

            Console.WriteLine();

            ViewPrinter.Println("\tConfira nosso menu!");

            Console.WriteLine();

            ViewPrinter.Print("\tPressione qualquer tecla para iniciar seu atendimento! ", ConsoleColor.Blue, ConsoleColor.White);
            
            Console.ReadLine();
        }

        public static void MostrarMenu()
        {
            Console.WriteLine("--- MENU ---");
            Console.WriteLine("1 - Fazer novo pedido");
            Console.WriteLine("2 - Cancelar um pedido");
            Console.WriteLine("3 - Acompanhar pedidos");
            Console.WriteLine("4 - Valor total ate o momento");
            Console.WriteLine("5 - Tempo em atividade");
            Console.WriteLine("6 - Encerrar comanda");
        }


    }
}


