using Restaurante.Repositorio.Services.Mesa.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Restaurante.Repositorio.Services.Mesa
{
    public interface IMesaService
    {
        Task AtualizarStatus(int mesaId, bool ocupada);
        Task<ICollection<MesaModel>> Listar();
        Task<MesaModel> Obter(int mesaId);
    }
}
