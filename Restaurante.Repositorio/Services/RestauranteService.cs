using Restaurante.Repositorio.Contexto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Restaurante.Repositorio.Services
{
    public class RestauranteService
    {
        public RestauranteContexto _context;

        public RestauranteService(RestauranteContexto context)
        {
            _context = context;
        }

        public async Task SaveChangesAsync(string errorMessage)
        {
            if (await _context.SaveChangesAsync() <= 0)
                throw new Exception(errorMessage);
        }
    }
}
