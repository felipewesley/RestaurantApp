using System;
using System.Collections.Generic;
using System.Text;
using RestauranteApp.Services.Comanda;
using RestauranteApp.Services.Comanda.Models;
using System.Globalization;

namespace RestauranteApp.Views
{
    class ViewComanda
    {
        
        public static void MostrarComandaSelecionada(int comandaId)
        {
            Console.Clear();
            Console.WriteLine();
            ViewPrinter.Print("\tCODIGO COMANDA: ");
            ViewPrinter.Println($" { comandaId } ", ConsoleColor.White, ConsoleColor.DarkGreen);
            Console.WriteLine();
            ViewPrograma.MensagemContinuarAtendimento();
        }

        public static void LabelObterDadosComanda()
        {
            ViewPrinter.Println("\tObtendo dados da comanda \t", ConsoleColor.Yellow);

            Console.WriteLine();

            ViewPrinter.Print("\tNº Comanda: ");
        }

        public static int ObterComandaValida(int comandaId)
        {
            return comandaId;
        }

        public static void MostrarComandaResumida(int comandaId)
        {
            
            ViewPrinter.Println("\t     DESCRICAO RESUMIDA DA COMANDA     ", ConsoleColor.White, ConsoleColor.DarkGreen);

            var comanda = ComandaService.ObterComandaResumida(comandaId);

            ViewPrinter.Println("\t------------------------------------------------------");

            ViewPrinter.Print("\tComanda: ");
            ViewPrinter.Print(comandaId.ToString(), ConsoleColor.Cyan);

            ViewPrinter.Print("\tMesa: ");
            ViewPrinter.Println($" [{ comanda.MesaId.ToString() }] ", ConsoleColor.Blue, ConsoleColor.White);

            ViewPrinter.Println("\t------------------------------------------------------");

            ViewPrinter.Print("\tValor atual: ");
            ViewPrinter.Print($" R$ { comanda.Valor.ToString("F2", CultureInfo.InvariantCulture) } ", ConsoleColor.DarkBlue, ConsoleColor.White);

            ViewPrinter.Print("\tEntrada: ");
            ViewPrinter.Println(comanda.DataHoraEntrada.ToString());

        }
    }
}
