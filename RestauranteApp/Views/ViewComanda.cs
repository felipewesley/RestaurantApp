using System;
using System.Collections.Generic;
using System.Text;
using RestauranteApp.Services.Mesa;
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

        public static void MostrarAcompanhamento(int comandaId, bool comandaCompleta = false)
        {
            Console.WriteLine();

            if (comandaCompleta)
                ViewPrinter.Println("\t                  COMANDA DETALHADA                   ", ConsoleColor.White, ConsoleColor.DarkGreen);
            else
                ViewPrinter.Println("\t              ACOMPANHAMENTO DA COMANDA               ", ConsoleColor.White, ConsoleColor.DarkGreen);

            var comanda = ComandaService.ObterComandaResumida(comandaId);

            ViewPrinter.Println("\t------------------------------------------------------");

            ViewPrinter.Print("\tNº Comanda: ");
            ViewPrinter.Println(comandaId.ToString(), ConsoleColor.Cyan);

            ViewPrinter.Print("\tMesa: ");
            ViewPrinter.Println($" [{ comanda.MesaId }] ", ConsoleColor.Blue, ConsoleColor.White);

            ViewPrinter.Print("\tQuantidade de pessoas: ");
            ViewPrinter.Print(comanda.QuantidadeClientes.ToString(), ConsoleColor.Cyan);
            if (comanda.QuantidadeClientes == 1)
                ViewPrinter.Println(" pessoa");
            else
                ViewPrinter.Println(" pessoas");

            /*ViewPrinter.Print("\tTempo em atividade: ");
            TimeSpan tempo = ComandaService.CalcularTempoAtividade(comandaId);
            string strTempo = string.Join(':', tempo.Hours, tempo.Minutes, tempo.Seconds);
            ViewPrinter.Println(strTempo, ConsoleColor.Cyan);
            */
            ViewPrinter.Print("\tData/hora entrada: ");
            ViewPrinter.Println(comanda.DataHoraEntrada.ToString(), ConsoleColor.Cyan);

            ViewPrinter.Println("\t----------------------------------------------------------------");

            ViewPrinter.Println("\tPedidos relacionados a esta comanda: ");

            var listaPedidos = PedidoService.ObterPedidosPorComanda(comandaId);

            Console.WriteLine();

            ViewPrinter.Println("\t  #ID - Qtde  x  Produto   -   Valor   ---  Status ");
            ViewPrinter.Println("\t----------------------------------------------------------------");

            // Imprimindo os rodizios como pedidos
            ViewPrinter.Print($"\t   # - ");
            ViewPrinter.Print($"{ comanda.QuantidadeClientes } x Rodízio  -  R$ { MesaService.ValorRodizio.ToString("F2", CultureInfo.InvariantCulture) } --- ");
            ViewPrinter.Println(" Ativo ", ConsoleColor.White, ConsoleColor.Green);

            if (listaPedidos.Count == 0)
            {
                Console.WriteLine();
                ViewPrinter.Println("\t  Ainda não há pedidos relacionados a esta comanda  ", ConsoleColor.Black, ConsoleColor.Yellow);
            } else
            {
                listaPedidos.ForEach(pedido => { 
                
                    var produto = ProdutoService.ObterProduto(pedido.ProdutoId, false);
                    var status = StatusService.ObterStatus(pedido.Status);

                    ViewPrinter.Print($"\t   { pedido.PedidoId } - ");
                    ViewPrinter.Print($"{ pedido.Quantidade } x { produto.Nome }  -  ");
                    
                    if (produto.Valor == 0)
                        ViewPrinter.Print("INCLUSO");
                    else
                        ViewPrinter.Print($"R$ { produto.Valor.ToString("F2", CultureInfo.InvariantCulture) }");

                    ViewPrinter.Print("  --- ");
                    switch (pedido.Status)
                    {
                        case 1: ViewPrinter.Print($" { status.Descricao } ", ConsoleColor.Black, ConsoleColor.Yellow); break;
                        case 2: ViewPrinter.Print($" { status.Descricao } ", ConsoleColor.White, ConsoleColor.Red); break;
                        case 3: ViewPrinter.Print($" { status.Descricao } ", ConsoleColor.White, ConsoleColor.Green); break;
                        default: ViewPrinter.Print($" { status.Descricao } ", ConsoleColor.Black, ConsoleColor.Gray); break;
                    }
                    Console.WriteLine();
                });
            }

            ViewPrinter.Println("\t------------------------------------------------------", ConsoleColor.Cyan);

            ViewPrinter.Print("\tValor parcial da comanda: ");
            ViewPrinter.Print($" R$ { comanda.Valor.ToString("F2", CultureInfo.InvariantCulture) } ", ConsoleColor.DarkBlue, ConsoleColor.White);

            Console.WriteLine();
        }

        public static bool EncerramentoComanda(int comandaId)
        {
            bool pedidosEmAberto = PedidoService.VerificarPedidosEmAberto(comandaId);

            Console.WriteLine();

            if (pedidosEmAberto)
            {
                ViewPrinter.Println("\tAinda há pedidos em aberto relacionados a esta comanda.", ConsoleColor.Black, ConsoleColor.Yellow);
                Console.WriteLine();
                ViewPrinter.Println("\tSe você continuar, estes pedidos serão incluídos no valor total!", ConsoleColor.Yellow);
                Console.WriteLine();
                ViewPrinter.Print("\tDeseja continuar o encerramento? (s/n) ");
                char continuar = char.Parse(Console.ReadLine());
                if (continuar == 'n')
                    return false;

                Console.WriteLine();
                ViewPrinter.Print("\tPressione 'Enter' para visualizar o valor total a ser pago...");
                Console.ReadLine();
                Console.Clear();
            } else
            {
                ViewPrinter.Println("\t      ENCERRAMENTO DA COMANDA     ", ConsoleColor.White, ConsoleColor.DarkGreen);
                
                Console.WriteLine();

                ViewPrinter.Print("\tDeseja continuar com o encerramento? (s/n) ");
                char continuar = char.Parse(Console.ReadLine());
                if (continuar == 'n')
                    return false;

                Console.WriteLine();
                ViewPrinter.Print("\tPressione 'Enter' para visualizar o valor total a ser pago...");
                Console.ReadLine();
                Console.Clear();
            }

            Console.WriteLine();

            MostrarComandaCompleta(comandaId);

            Console.WriteLine();
            ViewPrinter.Print("\tPressione 'Enter' para confirmar o pagamento! ", ConsoleColor.DarkGreen);
            Console.ReadLine();

            return true;
        }

        public static void MostrarComandaCompleta(int comandaId)
        {
            MostrarAcompanhamento(comandaId, true);
            
            ViewPrinter.Println("\t--------------------------------------------------------", ConsoleColor.Cyan);

            ViewPrinter.Print("\tValor Final: ");
            ViewPrinter.Print($" R$ { ComandaService.CalcularValorComanda(comandaId, true).ToString("F2", CultureInfo.InvariantCulture) } ", ConsoleColor.White, ConsoleColor.Green);
            ViewPrinter.Print("\t * Incluído 10% garçom", ConsoleColor.Yellow);

            Console.WriteLine();
        }
    }
}
