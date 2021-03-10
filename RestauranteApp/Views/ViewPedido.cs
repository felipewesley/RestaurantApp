using System;
using System.Collections.Generic;
using System.Linq;
using RestauranteApp.Services.Pedido;
using RestauranteApp.Services.Pedido.Models;
using RestauranteApp.Services.Produto;
using RestauranteApp.Services.Produto.Models;
using RestauranteApp.Services.TipoProduto;
using RestauranteApp.Services.TipoProduto.Models;

namespace RestauranteApp.Views
{
    class ViewPedido
    {

        private readonly PedidoService _pedidoService;
        private readonly ProdutoService _produtoService;
        private readonly TipoProdutoService _tipoProdutoService;

        private readonly ViewProduto _viewProduto;

        public ViewPedido(PedidoService pedidoService, ProdutoService produtoService, TipoProdutoService tipoProdutoService)
        {
            _pedidoService = pedidoService;
            _produtoService = produtoService;
            _tipoProdutoService = tipoProdutoService;

            _viewProduto = new ViewProduto(produtoService, tipoProdutoService);
        }

        public void MostrarCardapio(int comandaId, int tipoExibicao)
        {

            bool novoPedido = true;
            // var listaPedidos = new List<PedidoFormularioModel>();

            while (novoPedido)
            {
                Console.WriteLine();
                _viewProduto.MostrarListaProdutos(ExibirMenuPorTipoExibicao(tipoExibicao));

                var pedido = FazerPedido(comandaId);

                if (pedido == null)
                {
                    ViewPrinter.Println("\t Pedido cancelado! ", ConsoleColor.White, ConsoleColor.Red);
                } else
                {
                    ViewPrinter.Println("\t Pedido registrado! ", ConsoleColor.White, ConsoleColor.DarkGreen);
                    _pedidoService.RegistrarNovoPedido(pedido);
                }
                    
                ViewProduto.DivisorListaProdutos();
                ViewPrinter.Print("\tDeseja fazer outro pedido? (s/n) ");
                novoPedido = char.Parse(Console.ReadLine()) == 's';
            }

            Console.WriteLine();
        }

        public PedidoFormularioModel FazerPedido(int comandaId)
        {
            ViewPrinter.Println("\t     NOVO PEDIDO    ", ConsoleColor.White, ConsoleColor.Blue);
            
            Console.WriteLine();
            
            ViewPrinter.Print("\tProduto: ");
            int produtoId = int.Parse(Console.ReadLine());

            if (!_produtoService.ProdutoValido(produtoId))
            {
                ViewPrinter.Print("qualquer texto", ConsoleColor.Cyan, ConsoleColor.DarkBlue);
                ViewPrinter.Println("\t O produto selecionado não está contido no cardápio! \n", ConsoleColor.White, ConsoleColor.Red);
                return null;
            }

            int quantidadeMaxima = _produtoService.ObterQuantidadePermitida(produtoId);
            if (quantidadeMaxima != 0)
            {
                Console.WriteLine();
                ViewPrinter.Println($"\t Quantidade máxima permitida por pedido: { quantidadeMaxima } itens", ConsoleColor.Black, ConsoleColor.Yellow);
                Console.WriteLine();
            }

            ViewPrinter.Print("\tQuantidade: ");
            int quantidade = int.Parse(Console.ReadLine());

            if (quantidade <= 0)
            {
                ViewPrinter.Println("\t Quantidade informada inválida! \n", ConsoleColor.White, ConsoleColor.Red);
                return null;
            }
            if (quantidadeMaxima != 0)
            {
                if (_produtoService.ObterQuantidadePermitida(produtoId) < quantidade)
                {
                    ViewPrinter.Println("\t Quantidade solicitada além do permitido para este produto! \n", ConsoleColor.White, ConsoleColor.Red);
                    return null;
                }
            }

            Console.WriteLine();

            ViewPrinter.Print("\tConfirma este pedido? (s/n) ");
            bool confirmarPedido = char.Parse(Console.ReadLine()) == 's';

            if (!confirmarPedido) return null;

            return new PedidoFormularioModel()
            {
                ComandaId = comandaId,
                ProdutoId = produtoId,
                Quantidade = quantidade
            };
        }

        public void CancelarPedido(int comandaId)
        {
            Console.WriteLine();
            ViewPrinter.Println("\t              Cancelamento de pedido                ", ConsoleColor.White, ConsoleColor.Red);
            Console.WriteLine();

            var pedidos = _pedidoService.ObterPedidosPorComanda(comandaId);

            Console.WriteLine("\tPedidos disponíveis para cancelamento: ");
            Console.WriteLine();
            
            Console.WriteLine("\tCOD | QTDE x PRODUTO   --- STATUS");
            Console.WriteLine("\t------------------------------------------");

            pedidos.ToList().ForEach(p =>
            {
                Console.WriteLine($"{ p.PedidoId } ");

            });

            Console.Write("\tInforme o código do pedido: ");
            var pedidoId = int.Parse(Console.ReadLine());
        }

        public int SolicitarCategoria()
        {
            Console.WriteLine();
            ViewPrinter.Print("\tEscolha uma categoria: ", ConsoleColor.Green);
            Console.WriteLine();

            // Selecionar categoria de produto
            ViewProduto.DivisorListaProdutos();
            _viewProduto.MostrarTiposProduto();

            Console.WriteLine();
            ViewPrinter.Print("\tCategoria desejada: ");

            int categoria = int.Parse(Console.ReadLine());

            if (!_tipoProdutoService.TipoProdutoValido(categoria))
            {
                bool categoriaValida = false;
                while (!categoriaValida)
                {
                    Console.Clear();

                    Console.WriteLine();
                    ViewPrinter.Println("\tCategoria selecionada inválida! ", ConsoleColor.White, ConsoleColor.Red);

                    Console.WriteLine();
                    ViewPrinter.Print("\tEscolha uma categoria: ", ConsoleColor.Green);
                    Console.WriteLine();

                    // Selecionar categoria de produto
                    ViewProduto.DivisorListaProdutos();
                    _viewProduto.MostrarTiposProduto();

                    Console.WriteLine();
                    ViewPrinter.Print("\tCategoria desejada: ");
                    categoria = int.Parse(Console.ReadLine());
                    categoriaValida = _tipoProdutoService.TipoProdutoValido(categoria);
                }
            }

            return categoria;
        }

        public List<ProdutoMenuModel> ExibirMenuPorTipoExibicao(int tipoExibicao)
        {
            List<ProdutoMenuModel> listaProdutos;

            if (tipoExibicao == 1)
            {
                Console.Clear();

                int categoria = SolicitarCategoria();

                listaProdutos = _produtoService.ObterProdutosPorTipo(categoria);
                var tipo = _tipoProdutoService.ObterTipoProduto(categoria);

                Console.Clear();
                Console.WriteLine();

                ViewPrinter.Println($"\t      Listando: [{ tipo.TipoProdutoId }] { tipo.Descricao }       ", ConsoleColor.White, ConsoleColor.Blue);
                Console.WriteLine();
            }
            else
            {
                listaProdutos = _produtoService.ObterProdutos(true);

                Console.Clear();
                Console.WriteLine();

                ViewPrinter.Println("\t       Listando todos os produtos       ", ConsoleColor.White, ConsoleColor.Blue);
                Console.WriteLine();
            }

            return listaProdutos;
        }
    }
}
