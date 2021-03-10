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
using System.Linq;
using RestauranteApp.Contexto;

namespace RestauranteApp.Views
{
    class ViewComanda
    {

        public static RestauranteContext context { get; set; }

        private readonly MesaService _mesaService;
        private readonly ComandaService _comandaService;
        private readonly PedidoService _pedidoService;
        private readonly ProdutoService _produtoService;

        public ViewComanda(MesaService mesaService, ComandaService comandaService, PedidoService pedidoService, ProdutoService produtoService)
        {
            _mesaService = mesaService;
            _comandaService = comandaService;
            _pedidoService = pedidoService;
            _produtoService = produtoService;
        }

        public void LabelObterDadosComanda()
        {
            ViewPrinter.Println("\tObtendo dados da comanda \t", ConsoleColor.Yellow);

            Console.WriteLine();

            ViewPrinter.Print("\tNº Comanda: ");
        }

        public int ObterComandaDisponivel(int comandaId)
        {
            bool comandaDisponivel = false;

            while (!comandaDisponivel)
            {
                Console.Clear();

                ViewPrograma.CabecalhoDadosIniciais();

                if (!_comandaService.ValidarComanda(comandaId))
                    ViewPrinter.Println("\t A comanda informada não pode ser utilizada! Utilize outro código. ", ConsoleColor.White, ConsoleColor.Red);

                Console.WriteLine();

                LabelObterDadosComanda();
                comandaId = int.Parse(Console.ReadLine());

                if (_comandaService.ValidarComanda(comandaId)) comandaDisponivel = true;
            }

            return comandaId;
        }

        public void MostrarComandaSelecionada(int comandaId)
        {
            // Console.Clear();

            ViewPrograma.ShowSucesso();

            ViewPrinter.Print("\tCODIGO COMANDA: ");
            ViewPrinter.Println($" { comandaId } ", ConsoleColor.White, ConsoleColor.DarkGreen);
            Console.WriteLine();
            ViewPrograma.MensagemContinuarAtendimento();
        }

        public void MostrarComandaResumida(int comandaId)
        {
            Console.WriteLine();

            ViewPrinter.Println("\t             DESCRICAO RESUMIDA DA COMANDA            ", ConsoleColor.White, ConsoleColor.DarkGreen);

            var comanda = _comandaService.ObterComandaResumida(comandaId);

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

        public void MostrarAcompanhamento(int comandaId, bool comandaCompleta = false)
        {
            Console.WriteLine();

            if (comandaCompleta)
                ViewPrinter.Println("\t                  COMANDA DETALHADA                   ", ConsoleColor.White, ConsoleColor.DarkGreen);
            else
                ViewPrinter.Println("\t              ACOMPANHAMENTO DA COMANDA               ", ConsoleColor.White, ConsoleColor.DarkGreen);

            var comanda = _comandaService.ObterComandaResumida(comandaId);

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

            /*
            ViewPrinter.Print("\tTempo em atividade: ");
            TimeSpan tempo = ComandaService.CalcularTempoAtividade(comandaId);
            string strTempo = string.Join(':', tempo.Hours, tempo.Minutes, tempo.Seconds);
            ViewPrinter.Println(strTempo, ConsoleColor.Cyan);
            */

            ViewPrinter.Print("\tData/hora entrada: ");
            ViewPrinter.Println(comanda.DataHoraEntrada.ToString(), ConsoleColor.Cyan);

            ViewPrinter.Println("\t----------------------------------------------------------------");

            ViewPrinter.Println("\tPedidos relacionados a esta comanda: ");

            var listaPedidos = _pedidoService.ObterPedidosPorComanda(comandaId);

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

                if (comandaCompleta)
                    ViewPrinter.Println("\t  Não houveram pedidos nesta comanda  ", ConsoleColor.Black, ConsoleColor.Yellow);
                else
                    ViewPrinter.Println("\t  Ainda não há pedidos relacionados a esta comanda  ", ConsoleColor.Black, ConsoleColor.Yellow);

            } else
            {
                // Imprimindo listagem de pedidos realizados
                listaPedidos.ToList().ForEach(pedido => { 
                
                    var produto = _produtoService.ObterProduto(pedido.Produto.ProdutoId, false);

                    ViewPrinter.Print($"\t   { pedido.PedidoId } - ");
                    ViewPrinter.Print($"{ pedido.Quantidade } x { produto.Nome }  -  ");
                    
                    if (produto.Valor == 0)
                        ViewPrinter.Print("INCLUSO");
                    else
                        ViewPrinter.Print($"R$ { produto.Valor.ToString("F2", CultureInfo.InvariantCulture) }");

                    ViewPrinter.Print("  --- ");
                    switch (pedido.Status.StatusId)
                    {
                        case 1: ViewPrinter.Print($" { pedido.Status.Descricao } ", ConsoleColor.Black, ConsoleColor.Yellow); break;
                        case 2: ViewPrinter.Print($" { pedido.Status.Descricao } ", ConsoleColor.White, ConsoleColor.Green); break;
                        case 3: ViewPrinter.Print($" { pedido.Status.Descricao } ", ConsoleColor.White, ConsoleColor.Red); break;
                        default: ViewPrinter.Print($" { pedido.Status.Descricao } ", ConsoleColor.Black, ConsoleColor.Gray); break;
                    }
                    Console.WriteLine();
                });
            }

            ViewPrinter.Println("\t------------------------------------------------------", ConsoleColor.Cyan);

            if (comandaCompleta)
                ViewPrinter.Print("\tValor final da comanda: ");
            else
                ViewPrinter.Print("\tValor parcial da comanda: ");

            ViewPrinter.Print($" R$ { comanda.Valor.ToString("F2", CultureInfo.InvariantCulture) } ", ConsoleColor.DarkBlue, ConsoleColor.White);

            Console.WriteLine();
        }

        public bool EncerramentoComanda(int comandaId)
        {
            bool pedidosEmAberto = _pedidoService.VerificarPedidosEmAberto(comandaId);

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

        public void MostrarComandaCompleta(int comandaId)
        {
            MostrarAcompanhamento(comandaId, true);
            
            ViewPrinter.Println("\t--------------------------------------------------------", ConsoleColor.Cyan);

            Console.WriteLine();
        }
    }
}
