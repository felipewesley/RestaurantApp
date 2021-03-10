using System;
using System.Collections.Generic;
using RestauranteApp.Contexto;
using RestauranteApp.DatabaseControl;
using RestauranteApp.Services.Mesa.Models;
using System.Linq;

namespace RestauranteApp.Services.Mesa
{
    class MesaService
    {
        private readonly RestauranteContext _context;
        public static readonly float ValorRodizio = 45.0F;

        public MesaService(RestauranteContext context)
        {
            _context = context;
        }

        public void AtualizarStatusMesa(int mesaId, bool ocupada)
        {

            var mesa = _context.Mesa
                        .Where(m => m.MesaId == mesaId)
                        .FirstOrDefault();

            mesa.Ocupada = ocupada;

            if (_context.SaveChanges() <= 0)
                throw new Exception("Não foi possível atualizar o status da mesa para 'Ocupada'!");
        }

        public bool ValidarMesa(int mesaId)
        {

            if (!_context.Mesa.Any(m => m.MesaId == mesaId))
                return false;

            var mesa = _context.Mesa
                        .Where(m => m.MesaId == mesaId)
                        .FirstOrDefault();

            // False se a mesa estiver ocupada
            return !mesa.Ocupada;
        }

        public ICollection<MesaFormularioModel> ObterMesas(bool apenasDisponiveis = false)
        {

            var listaMesas = _context.Mesa
                            .Where(m => m.Ocupada == false)
                            .Select(m => new MesaFormularioModel {
                                MesaId = m.MesaId,
                                Ocupada = m.Ocupada
                            })
                            .OrderBy(m => m.MesaId)
                            .ToList();

            return listaMesas;
        }

        public int ObterQuantidadeClientes(int mesaId)
        {

            var mesa = _context.Mesa
                        .Where(m => m.MesaId == mesaId)
                        .FirstOrDefault();

            return mesa.Capacidade;
        }
    }
}
