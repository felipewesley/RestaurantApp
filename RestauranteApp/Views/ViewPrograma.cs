using System;
using System.Collections.Generic;
using RestauranteApp.Services.Produto;
using RestauranteApp.Services.Produto.Models;

namespace RestauranteApp.Views
{
    class ViewPrograma
    {

        public static void ShowSucesso()
        {
            Console.WriteLine();
            ViewPrinter.Print("\t  > Sucesso!      ", ConsoleColor.White, ConsoleColor.DarkGreen);
            Console.WriteLine();
            Console.WriteLine();
        }
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

            ViewPrinter.Print("\t Pressione ", ConsoleColor.Blue, ConsoleColor.White);
            ViewPrinter.Print("'Enter'", ConsoleColor.DarkGreen, ConsoleColor.White);
            ViewPrinter.Print(" tecla para iniciar seu atendimento! ", ConsoleColor.Blue, ConsoleColor.White);
            Console.ReadLine();
        }

        public static int EscolhaFormatoExibicaoCardapio()
        {
            Console.WriteLine();

            ViewPrinter.Println("\tAntes de ir ao menu principal, gostaríamos de ");
            ViewPrinter.Println("\tsaber se você prefere ver o cardápio com: ");

            Console.WriteLine();

            ViewPrinter.Print("\t [1] ", ConsoleColor.DarkCyan);
            ViewPrinter.Println("Produtos separados por categoria;");

            ViewPrinter.Print("\t [2] ", ConsoleColor.DarkCyan);
            ViewPrinter.Println("Listagem de todos os produtos;");

            Console.WriteLine();

            ViewPrinter.Print("\t Obs.: ", ConsoleColor.Black, ConsoleColor.Yellow);
            ViewPrinter.Println(" Qualquer outro valor listará todos os produtos.", ConsoleColor.Yellow);

            Console.WriteLine();

            ViewPrinter.Print("\tSua escolha: ");
            return int.Parse(Console.ReadLine());
        }

        public static void MostrarMenu(int comandaId, int tipoExibicao)
        {
            bool mostrarMenuNovamente = true;

            while (mostrarMenuNovamente)
            {

                ViewComanda.MostrarComandaResumida(comandaId);

                ViewPrinter.Println("\t         MENU PRINCIPAL         ", ConsoleColor.White, ConsoleColor.DarkBlue);

                Console.WriteLine();

                MostrarOpcaoMenu(1, "Ver meus pedidos");
                MostrarOpcaoMenu(2, "Fazer novo pedido");
                MostrarOpcaoMenu(3, "Cancelar um pedido");
                MostrarOpcaoMenu(4, "Acompanhamento da comanda");
                MostrarOpcaoMenu(5, "Encerrar minha comanda");

                Console.WriteLine();

                ViewPrinter.Print("\tEscolha: ");
                int escolha = int.Parse(Console.ReadLine());

                if (!ChamarOpcaoEscolhida(comandaId, escolha)) mostrarMenuNovamente = false;

            }
        }

        public static bool ChamarOpcaoEscolhida(int comandaId, int opcaoEscolhida)
        {
            ViewPrinter.Print("\tOPCAO ESCOLHIDA: ");

            switch (opcaoEscolhida)
            {
                case 1:
                    ViewPrinter.Println(" Ver meus pedidos ", ConsoleColor.White, ConsoleColor.DarkGreen);
                    break;
                case 2:
                    ViewPrinter.Println(" Fazer novo pedido ", ConsoleColor.White, ConsoleColor.DarkGreen);
                    break;
                case 3:
                    ViewPrinter.Println(" Cancelar um pedido ", ConsoleColor.White, ConsoleColor.DarkGreen);
                    break;
                case 4:
                    ViewPrinter.Println(" Acompanhamento da comanda ", ConsoleColor.White, ConsoleColor.DarkGreen);
                    PressioneEnterParaContinuar("visualizar o acompanhamento da comanda");
                    ViewComanda.MostrarAcompanhamento(comandaId);
                    break;
                case 5:
                    ViewPrinter.Println(" Encerrar comanda ", ConsoleColor.White, ConsoleColor.DarkGreen);
                    return false;
                default:
                    ViewPrinter.Println(" Opcao escolhida invalida! ", ConsoleColor.White, ConsoleColor.Red);
                    break;
            }

            Console.WriteLine();
            MensagemContinuarAtendimento();

            return true;
        }

        public static void PressioneEnterParaContinuar(string message)
        {
            Console.WriteLine();
            ViewPrinter.Print("\t Pressione ", ConsoleColor.Blue, ConsoleColor.White);
            ViewPrinter.Print("'Enter'", ConsoleColor.Green, ConsoleColor.White);
            ViewPrinter.Print(" para " + message + "... ", ConsoleColor.Blue, ConsoleColor.White);
            Console.ReadLine();
            Console.Clear();
        }

        public static void MostrarOpcaoMenu(int opcaoId, string descricao)
        {
            ViewPrinter.Print($"\t[{ opcaoId }] ", ConsoleColor.Yellow);
            ViewPrinter.Println(descricao);
        }

        public static void MostrarCardapio(int tipoExibicao)
        {
            List<ProdutoMenuModel> listaProdutos;

            if (tipoExibicao == 1)
            {
                Console.Clear();
                ViewPrinter.Print("\tEscolha uma categoria: ");
                Console.WriteLine();

                // Selecionar categoria de produto
                ViewProduto.DivisorListaProdutos();
                ViewProduto.MostrarTiposProduto();
                int categoria = int.Parse(Console.ReadLine());

                listaProdutos = ProdutoService.ObterProdutosPorTipo(categoria);

            }
            else
            {
                listaProdutos = ProdutoService.ObterProdutos(true);
            }

            ViewProduto.MostrarListaProdutos(listaProdutos);
        }

        public static void MensagemContinuarAtendimento()
        {
            ViewPrinter.Print("\tPressione ", ConsoleColor.Black, ConsoleColor.White);
            ViewPrinter.Print("Enter", ConsoleColor.Blue, ConsoleColor.White);
            ViewPrinter.Print(" para continuar seu atendimento...", ConsoleColor.Black, ConsoleColor.White);
            Console.ReadLine();
            Console.Clear();
        }

        public static void CabecalhoDadosIniciais()
        {
            Console.WriteLine();
            ViewPrinter.Println("\tPor favor, informe os dados solicitados a seguir:");
            Console.WriteLine();
        }
    }
}


