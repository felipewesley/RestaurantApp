using System;
using RestauranteApp.Services.Comanda;
using RestauranteApp.Services.Mesa;
using RestauranteApp.Services.Pedido;
using RestauranteApp.Services.Produto;
using RestauranteApp.Services.Status;
using RestauranteApp.Services.TipoProduto;
using System.IO;

namespace RestauranteApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Bem vindo ao Restaurante Sutekina Ranch!\n");

            Console.Write("Numero comanda: ");
            int comandaId = int.Parse(Console.ReadLine());

            Console.Write("Numero da mesa: ");
            int mesa = int.Parse(Console.ReadLine());

            Console.Write("Quantidade de clientes: ");
            int quantidadeClientes = int.Parse(Console.ReadLine());

            ComandaService comanda = new ComandaService(comandaId, mesa, quantidadeClientes);

            bool loop = true;

            do
            {
                
                int option = ShowMenu();

                comanda.RegistrarComanda();

                switch (option)
                {
                    case 1:
                        Console.WriteLine("Ainda nao é possivel realizar um pedido!");
                        break;

                    case 2:
                        Console.WriteLine("Ainda nao é possivel cancelar um pedido!");
                        break;

                    case 3:
                        Console.WriteLine("Ainda nao é possivel acompanhar pedidos!");
                        break;

                    case 4:
                        Console.Write("Valor total da comanda ate o momento: ");
                        Console.WriteLine(comanda.CalcularValorComanda());
                        break;

                    case 5:
                        Console.Write("Tempo em atividade: ");
                        Console.WriteLine(comanda.CalcularTempoAtividade());
                        break;

                    case 6:
                        Console.Write("Deseja realmente encerrar esta comanda? (s/n)");
                        char opt = char.Parse(Console.ReadLine());
                        if (opt == 's')
                        {
                            Console.WriteLine("Sua comanda foi encerrada no valor de R$ " + comanda.CalcularValorComanda());
                            loop = false;
                        } else
                        {
                            Console.WriteLine("Qualquer tecla para continuar...");
                        }
                        break;
                }

                Console.ReadLine();
                Console.Clear();

            } while (loop);

            /*using (StreamReader sr = new StreamReader(@"C:\workspace\RestauranteApp\RestauranteApp\Dados\ProdutoDados.csv"))
            {
                while (!sr.EndOfStream)
                {
                    Console.WriteLine(sr.ReadLine());
                }
            }*/

            Console.WriteLine();
        }

        static int ShowMenu()
        {
            Console.WriteLine("--- MENU ---");
            Console.WriteLine("1 - Fazer novo pedido");
            Console.WriteLine("2 - Cancelar um pedido");
            Console.WriteLine("3 - Acompanhar pedidos");
            Console.WriteLine("4 - Valor total ate o momento");
            Console.WriteLine("5 - Tempo em atividade");
            Console.WriteLine("6 - Encerrar comanda");

            return int.Parse(Console.ReadLine());
        }
    }
}

