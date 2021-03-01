using System;
using System.Collections.Generic;
using System.Text;
using RestauranteApp.Services.Comanda;
using RestauranteApp.Services.Pedido;
using RestauranteApp.Services.Produto;
using RestauranteApp.Services.Status;
using RestauranteApp.Services.Comanda.Models;
using System.Globalization;

namespace RestauranteApp.Views
{
    class ViewComanda
    {

        public static void LabelObterDadosComanda()
        {
            ViewPrinter.Println("\tObtendo dados da comanda \t", ConsoleColor.Yellow);

            Console.WriteLine();

            ViewPrinter.Print("\tNº Comanda: ");
        }

        public static void MostrarComandaSelecionada(int comandaId)
        {
            // Console.Clear();

            ViewPrograma.ShowSucesso();

            ViewPrinter.Print("\tCODIGO COMANDA: ");
            ViewPrinter.Println($" { comandaId } ", ConsoleColor.White, ConsoleColor.DarkGreen);
            Console.WriteLine();
            ViewPrograma.MensagemContinuarAtendimento();
        }

        public static void MostrarComandaResumida(int comandaId)
        {
            Console.WriteLine();

            ViewPrinter.Println("\t             DESCRICAO RESUMIDA DA COMANDA            ", ConsoleColor.White, ConsoleColor.DarkGreen);

            var comanda = ComandaService.ObterComandaResumida(comandaId);

            ViewPrinter.Println("\t------------------------------------------------------");

            ViewPrinter.Print("\tComanda: ");
            ViewPrinter.Print(comandaId.ToString(), ConsoleColor.Cyan);

            ViewPrinter.Print("\t\t\t\tMesa: ");
            ViewPrinter.Println($" [{ comanda.MesaId }] ", ConsoleColor.Blue, ConsoleColor.White);

            ViewPrinter.Println("\t------------------------------------------------------");

            ViewPrinter.Print("\tValor atual: ");
            ViewPrinter.Print($" R$ { comanda.Valor.ToString("F2", CultureInfo.InvariantCulture) } ", ConsoleColor.DarkBlue, ConsoleColor.White);

            ViewPrinter.Print("\t  Entrada: ");
            ViewPrinter.Println(comanda.DataHoraEntrada.ToString());

            Console.WriteLine();
        }

        public static void MostrarAcompanhamento(int comandaId)
        {
            Console.WriteLine();

            ViewPrinter.Println("\t              ACOMPANHAMENTO DA COMANDA               ", ConsoleColor.White, ConsoleColor.DarkGreen);

            var comanda = ComandaService.ObterComandaResumida(comandaId);

            ViewPrinter.Println("\t------------------------------------------------------");

            ViewPrinter.Print("\tNº Comanda: ");
            ViewPrinter.Println(comandaId.ToString(), ConsoleColor.Cyan);

            ViewPrinter.Print("\tMesa: ");
            ViewPrinter.Println($" [{ comanda.MesaId }] ", ConsoleColor.Blue, ConsoleColor.White);

            ViewPrinter.Print("\tQuantidade de pessoas: ");
            ViewPrinter.Print(comanda.QuantidadeClientes.ToString());
            if (comanda.QuantidadeClientes == 1)
                ViewPrinter.Println(" pessoa");
            else
                ViewPrinter.Println(" pessoas");

            ViewPrinter.Print("\tTempo em atividade: ");
            TimeSpan tempo = ComandaService.CalcularTempoAtividade(comandaId);
            string strTempo = string.Join(':', tempo.Hours, tempo.Minutes, tempo.Seconds);
            ViewPrinter.Println(strTempo, ConsoleColor.Cyan);

            ViewPrinter.Println("\t------------------------------------------------------");

            ViewPrinter.Println("\tPedidos relacionados a esta comanda: ");

            var listaPedidos = PedidoService.ObterPedidosPorComanda(comandaId);

            Console.WriteLine();

            if (listaPedidos.Count == 0)
            {
                ViewPrinter.Println("\t  Ainda não há pedidos relacionados a esta comanda  ", ConsoleColor.Black, ConsoleColor.Yellow);
            } else
            {
                listaPedidos.ForEach(pedido => { 
                
                    var produto = ProdutoService.ObterProduto(pedido.ProdutoId, false);
                    var status = StatusService.ObterStatus(pedido.Status);

                    ViewPrinter.Print($"\t   { pedido.PedidoId } - ");
                    ViewPrinter.Print($"{ pedido.Quantidade } x { produto.Nome } --- ");
                    switch (pedido.Status)
                    {
                        case 1: ViewPrinter.Println($" { status.Descricao } ", ConsoleColor.Black, ConsoleColor.Yellow); break;
                        case 2: ViewPrinter.Println($" { status.Descricao } ", ConsoleColor.White, ConsoleColor.Red); break;
                        case 3: ViewPrinter.Println($" { status.Descricao } ", ConsoleColor.White, ConsoleColor.Green); break;
                        default: ViewPrinter.Println($" { status.Descricao } ", ConsoleColor.Black, ConsoleColor.Gray); break;
                    }
                });
            }

            ViewPrinter.Println("\t------------------------------------------------------", ConsoleColor.Cyan);

            ViewPrinter.Print("\tValor atual da comanda: ");
            ViewPrinter.Print($" R$ { comanda.Valor.ToString("F2", CultureInfo.InvariantCulture) } ", ConsoleColor.DarkBlue, ConsoleColor.White);

            Console.WriteLine();
        }
    }
}
