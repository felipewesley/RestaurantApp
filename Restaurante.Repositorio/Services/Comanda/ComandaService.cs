using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Restaurante.Repositorio.Contexto;
using Restaurante.Repositorio.Services.Comanda.Models;
using Restaurante.Repositorio.Services.Mesa;
using Restaurante.Repositorio.Services.Pedido.Models;


namespace Restaurante.Repositorio.Services.Comanda
{
    public class ComandaService : RestauranteService, IComandaService
    {
        public ComandaService(RestauranteContexto context) : base(context) { }

        public void ValidarComanda(int comandaId)
        {
            // Comanda maior que zero e não existir no banco de dados

            if (comandaId < 0)
                throw new Exception("O código da comanda solicitada é inválido");

            /*
            if (!_context.Comanda.Any(c => c.ComandaId == comandaId))
                throw new Exception("A comanda solicitada não existe");
            */
        }

        public async Task RegistrarComanda(ComandaFormularioModel comandaModel)
        {
            comandaModel.Validar();

            _context.Comanda.Add(new Dominio.Comanda
            {
                MesaId = comandaModel.MesaId,
                DataHoraEntrada = DateTime.Now,
                DataHoraSaida = null,
                Valor = comandaModel.QuantidadeCliente * MesaService.ValorRodizio, // Verificar se há outra maneira de implementar
                Paga = false,
                QuantidadeClientes = comandaModel.QuantidadeCliente
            });

            await SaveChangesAsync("Não foi possível salvar a comanda");
        }

        public async Task EncerrarComanda(int comandaId, bool porcentagemGarcom = false)
        {
            ValidarComanda(comandaId);

            var comanda = _context.Comanda
                            .Where(c => c.ComandaId == comandaId)
                            .FirstOrDefault();

            _ = comanda ?? throw new Exception("A comanda solicitada não foi encontrada");

            if (comanda.Paga)
                throw new Exception("A comanda solicitada já foi encerrada em: " + comanda.DataHoraSaida.ToString());

            /*
            if (comanda.Valor != CalcularValorFinal(comandaId))
                throw new Exception("O cálculo completo retornou um valor diferente!");
            */

            if (porcentagemGarcom)
                comanda.Valor *= 1.1;

            comanda.Paga = true;
            comanda.DataHoraSaida = DateTime.Now;

            await SaveChangesAsync("Não foi possível registrar o encerramento da comanda");
        }

        public double CalcularValorFinal(int comandaId)
        {
            ValidarComanda(comandaId);

            /* Implementar validação de re-cálculo do valor total da comanda, 
            para garantir que os valores obtidos realmente estão corretos */

            return 0.0;
        }

        public async Task<ComandaResumidaModel> ObterComandaResumida(int comandaId)
        {
            ValidarComanda(comandaId);

            var comanda = await _context.Comanda
                        .Where(c => c.ComandaId == comandaId)
                        .Select(c => new ComandaResumidaModel()
                        {
                            MesaId = c.MesaId,
                            DataHoraEntrada = c.DataHoraEntrada,
                            QuantidadeClientes = c.QuantidadeClientes,
                            Valor = c.Valor
                        })
                        .FirstOrDefaultAsync();

            return comanda;
        }

        public async Task<ComandaCompletaModel> ObterComandaCompleta(int comandaId)
        {
            ValidarComanda(comandaId);

            var comanda = await _context.Comanda
                        .Where(c => c.ComandaId == comandaId)
                        .Include(c => c.Pedidos)
                        .ThenInclude(c => c.Status)
                        .Include(c => c.Pedidos) // Verificar se há uma maneira melhor de realizar esta inclusão
                        .ThenInclude(c => c.Produto)
                        .Select(c => new
                        {
                            c.MesaId,
                            c.DataHoraEntrada,
                            c.QuantidadeClientes,
                            c.Pedidos,
                            c.Valor,
                            c.Paga
                        })
                        .FirstOrDefaultAsync();

            _ = comanda ?? throw new Exception("A comanda solicitada não foi encontrada");

            // Cria uma model de Comanda sem a listagem de pedidos
            var model = new ComandaCompletaModel()
            {
                MesaId = comanda.MesaId,
                DataHoraEntrada = comanda.DataHoraEntrada,
                QuantidadeClientes = comanda.QuantidadeClientes,
                Valor = comanda.Valor,
                Paga = comanda.Paga
            };

            // Cria uma listagem de PedidoModel dentro da model de Comanda
            model.Pedidos = comanda.Pedidos
                .Select(p => new PedidoModel()
                {
                    PedidoId = p.PedidoId,
                    Produto = p.Produto.Nome,
                    Quantidade = p.Quantidade,
                    Valor = p.Quantidade * p.Produto.Valor,  // Verificar se há outra maneira de implementar
                    Status = p.Status.Descricao
                }).ToList();

            return model;
        }
    }
}
