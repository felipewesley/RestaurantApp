using Microsoft.EntityFrameworkCore;
using Restaurante.Repositorio.Contexto;
using Restaurante.Repositorio.Services.Comanda.Models;
using Restaurante.Repositorio.Services.Mesa;
using Restaurante.Repositorio.Services.Pedido.NewFolder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurante.Repositorio.Services.Comanda
{
    public class ComandaService : IComandaService
    {
        private readonly RestauranteContexto _context;

        public ComandaService(RestauranteContexto context)
        {
            // Injetando contexto no escopo da classe
            _context = context;
        }

        public bool ValidarComanda(int comandaId)
        {
            // Comanda maior que zero e não existir no banco de dados
            return comandaId > 0 && !_context.Comanda.Any(c => c.ComandaId == comandaId);
        }

        public void RegistrarComanda(ComandaFormularioModel comandaModel)
        {
            // Lançamento de excecao caso dados sejam invalidos
            comandaModel.Validar();

            _context.Comanda.Add(new Dominio.Comanda
            {
                MesaId = comandaModel.MesaId,
                DataHoraEntrada = DateTime.Now,
                DataHoraSaida = null,
                Valor = comandaModel.QuantidadeCliente * MesaService.ValorRodizio,
                Paga = false,
                QuantidadeClientes = comandaModel.QuantidadeCliente
            });

            if (_context.SaveChanges() <= 0)
                throw new Exception("Não foi possível salvar a comanda!");
        }

        public void EncerrarComanda(int comandaId, bool porcentagemGarcom = false)
        {
            var comanda = _context.Comanda
                            .Where(c => c.ComandaId == comandaId)
                            .FirstOrDefault();

            _ = comanda ?? throw new Exception("Nenhuma comanda foi encontrada!");

            if (comanda.Paga)
                throw new Exception("A comanda requisitada já foi encerrada!");

            if (comanda.Valor != CalcularValorFinal(comandaId))
                throw new Exception("O cálculo completo retornou um valor diferente!");

            if (porcentagemGarcom)
                comanda.Valor *= 1.1;

            comanda.Paga = true;
            comanda.DataHoraSaida = DateTime.Now;

            if (_context.SaveChanges() <= 0)
                throw new Exception("Não foi possível registrar o encerramento da comanda!");
        }

        public double CalcularValorFinal(int comandaId)
        {
            return 0.0;
        }

        public async Task<ComandaCompletaModel> ObterComandaCompleta(int comandaId)
        {
            var comanda = await _context.Comanda
                        .Where(c => c.ComandaId == comandaId)
                        .Include(c => c.Pedidos)
                        .Select(c => new
                        {
                            c.MesaId,
                            c.DataHoraEntrada,
                            c.QuantidadeClientes,
                            c.Pedidos,
                            c.Valor
                        })
                        .FirstOrDefaultAsync();

            var res = new ComandaCompletaModel()
            {
                MesaId = comanda.MesaId,
                DataHoraEntrada = comanda.DataHoraEntrada,
                QuantidadeClientes = comanda.QuantidadeClientes,
                Valor = comanda.Valor
            };


            res.Pedidos = comanda.Pedidos.Select(p => new PedidoModel()
            {
                PedidoId = p.PedidoId,
                Quantidade = p.Quantidade
            }).ToList();

            return res;
        }

        public ComandaResumidaModel ObterComandaResumida(int comandaId)
        {
            var valorRodizio = MesaService.ValorRodizio;

            var comanda = _context.Comanda
                        .AsQueryable()
                        .Include(c => c.Pedidos)
                        .ThenInclude(c => c.Produto)
                        .Where(c => c.ComandaId == comandaId)
                        .Select(c => new ComandaResumidaModel()
                        {
                            MesaId = c.MesaId,
                            DataHoraEntrada = c.DataHoraEntrada,
                            QuantidadeClientes = c.QuantidadeClientes,
                            Valor = c.Pedidos.Sum(p => p.Quantidade * p.Produto.Valor) + c.QuantidadeClientes * valorRodizio
                        })
                        .FirstOrDefault();

            return comanda;
        }
    }
}
