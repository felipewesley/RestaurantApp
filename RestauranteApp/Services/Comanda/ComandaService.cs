using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using RestauranteApp.Interfaces;
using RestauranteApp.DatabaseControl;
using RestauranteApp.Contexto;
using RestauranteApp.Services.Mesa;
using RestauranteApp.Services.Pedido;
using RestauranteApp.Services.Comanda.Models;

namespace RestauranteApp.Services.Comanda
{
    class ComandaService
    {
        private readonly RestauranteContext _context;

        public ComandaService(RestauranteContext context)
        {
            _context = context;
        }

        public bool ValidarComanda(int comandaId)
        {

            // Comanda maior que zero e não existir no banco de dados
            return comandaId > 0 && !_context.Comanda.Any(c => c.ComandaId == comandaId);
        }

        public void RegistrarComanda(ComandaFormularioModelCLI comandaModel)
        {

            // comandaModel.Validar();

            _context.Comanda.Add(new Entidades.Comanda
            {
                ComandaId = comandaModel.ComandaId,
                // MesaId = comandaModel.MesaId
                MesaId = comandaModel.Mesa.MesaId,
                DataHoraEntrada = DateTime.Now,
                DataHoraSaida = null,
                Valor = 0.0F,
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

            _ = comanda ?? throw new Exception("Nenhuma comanda foi encontrada");

            comanda.Paga = true;
            comanda.DataHoraSaida = DateTime.Now;
            comanda.Valor = ObterComandaResumida(comandaId).Valor;

            if (_context.SaveChanges() <= 0)
                throw new Exception("Não foi possível encerrar a comanda!");
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
