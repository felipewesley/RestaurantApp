using System;
using RestauranteApp.Views;
using RestauranteApp.Services.Mesa;
using RestauranteApp.Services.Mesa.Models;
using RestauranteApp.Services.Comanda;
using RestauranteApp.Services.Comanda.Models;
using RestauranteApp.DatabaseControl;
using RestauranteApp.Contexto;
using RestauranteApp.Services.Pedido;
using RestauranteApp.Services.Produto;
using RestauranteApp.Services.Status;
using RestauranteApp.Services.TipoProduto;

namespace RestauranteApp
{
    class Program
    {
        static void Main(string[] args)
        {
            // Instanciando o Contexto da aplicação
            var context = new RestauranteContext();

            // Instanciando as Services
            var mesaService = new MesaService(context);
            var comandaService = new ComandaService(context);
            var pedidoService = new PedidoService(context);
            var produtoService = new ProdutoService(context);
            var tipoProdutoService = new TipoProdutoService(context);

            // Instanciando as views que serão utilizadas
            var viewMesa = new ViewMesa(mesaService);
            var viewComanda = new ViewComanda(mesaService, comandaService, pedidoService, produtoService);
            var viewPedido = new ViewPedido(pedidoService, produtoService, tipoProdutoService);

            var viewPrograma = new ViewPrograma(comandaService, viewComanda, viewPedido);

            Console.Clear();

            viewPrograma.Welcome();

            Console.Clear();

            // Solicitando dados iniciais
            Console.WriteLine();
            ViewPrinter.Print("\tSEU ATENDIMENTO FOI INICIADO", ConsoleColor.Green);
            Console.WriteLine();

            // Criação do modelo de mesa recebido via formulario
            var mesa = new MesaFormularioModel();

            // Leitura e validacao ID Mesa
            ViewPrograma.CabecalhoDadosIniciais();
            viewMesa.LabelObterDadosMesa();
            mesa.MesaId = int.Parse(Console.ReadLine());
            if (!mesaService.ValidarMesa(mesa.MesaId)) mesa.MesaId = viewMesa.ObterMesaDisponivel(mesa.MesaId);
            viewMesa.MostrarMesaSelecionada(mesa.MesaId);
            Console.Clear();

            // Criação do modelo de comanda recebido via formulario
            var comanda = new ComandaFormularioModelCLI()
            {
                Mesa = mesa
            };

            // Leitura e validacao ID Comanda
            ViewPrograma.CabecalhoDadosIniciais();
            viewComanda.LabelObterDadosComanda();
            comanda.ComandaId = int.Parse(Console.ReadLine());
            if (!comandaService.ValidarComanda(comanda.ComandaId)) comanda.ComandaId = viewComanda.ObterComandaDisponivel(comanda.ComandaId);
            viewComanda.MostrarComandaSelecionada(comanda.ComandaId);
            Console.Clear();

            // Leitura e validacao Quantidade de Clientes
            ViewPrograma.CabecalhoDadosIniciais();
            viewMesa.LabelObterQuantidadeClientes(mesa.MesaId);
            comanda.QuantidadeCliente = int.Parse(Console.ReadLine());
            if (comanda.QuantidadeCliente <= 0 || comanda.QuantidadeCliente > comanda.Mesa.Capacidade) 
                comanda.QuantidadeCliente = viewMesa.ObterQuantidadeClientesValida(mesa.MesaId, comanda.QuantidadeCliente);
            viewMesa.MostrarQuantidadeClientesSelecionada(comanda.QuantidadeCliente);
            Console.Clear();

            // Ocupando mesa no banco de dados
            mesaService.AtualizarStatusMesa(comanda.Mesa.MesaId, true);

            // Salvando comanda no banco de dados
            comandaService.RegistrarComanda(comanda);

            int tipoExibicaoCardapio = viewPrograma.EscolhaFormatoExibicaoCardapio();

            Console.Clear();

            // Executa um loop mostrando o menu principal enquanto nao for explicitamente encerrado
            viewPrograma.MostrarMenu(comanda.ComandaId, tipoExibicaoCardapio);

            // Desocupando mesa no banco de dados
            mesaService.AtualizarStatusMesa(comanda.Mesa.MesaId, false);

        }
    }
}

