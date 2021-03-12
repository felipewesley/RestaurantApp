using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Restaurante.Repositorio.Contexto;
using Restaurante.Repositorio.Services.Mesa.Models;

namespace Restaurante.Repositorio.Services.Mesa
{
    public class MesaService : RestauranteService, IMesaService
    {
        public static double ValorRodizio = 45.0;

        public MesaService(RestauranteContexto context) : base(context) { }

        public void ValidarMesa(int mesaId)
        {
            if (mesaId <= 0)
                throw new Exception("A mesa solicitada não existe");

            /*
            if (!_context.Mesa.Any(m => m.MesaId == mesaId))
                throw new Exception("Não existe uma mesa correspondente à solicitada");
            */
        }

        public async Task AtualizarStatusMesa(int mesaId, bool ocupada)
        {
            ValidarMesa(mesaId);

            var mesa = await _context.Mesa
                        .Where(m => m.MesaId == mesaId)
                        .FirstOrDefaultAsync();

            _ = mesa ?? throw new Exception("Não foi possível obter a mesa solicitada");

            if (mesa.Ocupada == ocupada)
                throw new Exception("Não foi possível atualizar o status desta mesa pois ela já está " + (ocupada ? "ocupada" : "desocupada"));

            mesa.Ocupada = ocupada;

            await SaveChangesAsync("Não foi possível atualizar o status da mesa");
        }

        public async Task<ICollection<MesaModel>> BuscarMesas()
        {
            var colecaoMesas = await _context.Mesa
                            .Where(m => m.Ocupada == false) // Apenas desocupadas
                            .Select(m => new MesaModel()
                            {
                                MesaId = m.MesaId,
                                Capacidade = m.Capacidade,
                                Ocupada = m.Ocupada
                            })
                            .OrderBy(m => m.MesaId)
                            .ToListAsync();

            return colecaoMesas;
        }

        public async Task<MesaModel> ObterMesa(int mesaId)
        {
            ValidarMesa(mesaId);

            var mesa = await _context.Mesa
                        .Where(m => m.MesaId == mesaId)
                        .Select(m => new MesaModel()
                        {
                            MesaId = m.MesaId,
                            Capacidade = m.Capacidade,
                            Ocupada = m.Ocupada
                        })
                        .FirstOrDefaultAsync();

            _ = mesa ?? throw new Exception("Não foi possível obter a mesa solicitada");

            return mesa;
        }

    }
}
