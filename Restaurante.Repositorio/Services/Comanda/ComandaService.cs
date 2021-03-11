using Microsoft.EntityFrameworkCore;
using Restaurante.Repositorio.Contexto;
using Restaurante.Repositorio.Services.Comanda.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Restaurante.Repositorio.Services.Comanda
{
    public class ComandaService : IComandaService
    {
        private readonly RestauranteContexto _context;

        public ComandaService(RestauranteContexto context)
        {
            _context = context;
        }

        /*
        public ComandaCompletaModel ObterComandaCompleta(int comandaId)
        {
            var valorRodizio = 45;

            var comanda = _context.Comanda
                        .AsQueryable()
                        .Include(c => c.Pedidos)
                        .ThenInclude(c => c.Produto)
                        .Where(c => c.ComandaId == comandaId)
                        .Select(c => new ComandaCompletaModel()
                        {
                            MesaId = c.MesaId,
                            DataHoraEntrada = c.DataHoraEntrada,
                            QuantidadeClientes = c.QuantidadeClientes,
                            Valor = c.Pedidos.Sum(p => p.Quantidade * p.Produto.Valor) + c.QuantidadeClientes * valorRodizio
                        })
                        .FirstOrDefault();

            return comanda;
        }
        */

        public ComandaResumidaModel ObterComandaResumida(int comandaId)
        {
            // var valorRodizio = MesaService.ValorRodizio;
            var valorRodizio = 45.0;

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
