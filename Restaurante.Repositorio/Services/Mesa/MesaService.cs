using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Restaurante.Repositorio.Contexto;
using Restaurante.Repositorio.Services.Mesa.Models;

namespace Restaurante.Repositorio.Services.Mesa
{
    public class MesaService : IMesaService
    {
        public double ValorRodizio { get; private set; }
        private readonly RestauranteContexto _context;

        public MesaService(RestauranteContexto context)
        {
            _context = context;

            // Definindo valor do rodizio na construcao da service
            ValorRodizio = 45.0;
        }

        public async Task AtualizarStatus(int mesaId, bool ocupada)
        {
            var mesa = await _context.Mesa
                        .Where(m => m.MesaId == mesaId && m.Ocupada != ocupada)
                        .FirstOrDefaultAsync();

            _ = mesa ?? throw new Exception("A mesa solicitada nao existe ou ja esta com o status desejado");

            mesa.Ocupada = ocupada;

            await _context.SaveChangesAsync();
        }

        public async Task<ICollection<MesaModel>> Listar()
        {
            var mesas = await _context.Mesa
                            .Where(m => m.Ocupada == false) // Apenas desocupadas
                            .Select(m => new MesaModel()
                            {
                                MesaId = m.MesaId,
                                Capacidade = m.Capacidade,
                                Ocupada = m.Ocupada
                            })
                            .OrderBy(m => m.MesaId)
                            .ToListAsync();

            return mesas;
        }

        public async Task<MesaModel> Obter(int mesaId)
        {
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