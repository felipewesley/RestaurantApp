using System;
using System.Collections.Generic;
using System.Text;
using RestauranteApp.Services.Comanda;
using RestauranteApp.Services.Comanda.Models;

namespace RestauranteApp.Views
{
    class ViewComanda
    {
        public static void LerComandaId()
        {

        }

        public static int ObterComandaValida(int comandaId)
        {
            return comandaId;
        }

        public static void MostrarCabecalho(int comandaId)
        {
            
            ViewPrinter.Println("\tDESCRICAO RESUMIDA DA COMANDA", ConsoleColor.White, ConsoleColor.DarkGreen);

            var comanda = ComandaService.ObterComandaResumida(comandaId);

            ViewPrinter.Print("\tComanda: ");
            ViewPrinter.Print(comandaId.ToString());

            ViewPrinter.Print("\tMesa: ");
            ViewPrinter.Println(comanda.MesaId.ToString(), ConsoleColor.Blue, ConsoleColor.White);

            ViewPrinter.Println("\t-------------------------------------------------");

            ViewPrinter.Print("\tValor atual: ");
            ViewPrinter.Print(comanda.Valor.ToString(), ConsoleColor.DarkGreen, ConsoleColor.White);

            ViewPrinter.Print("\tEntrada: ");
            ViewPrinter.Println(comanda.DataHoraEntrada.ToString());

        }
    }
}
