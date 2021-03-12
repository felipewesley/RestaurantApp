using Restaurante.Repositorio.Services.Mesa.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Restaurante.Repositorio.Services.Mesa
{
    public interface IMesaService
    {
        Task AtualizarStatusMesa(int mesaId, bool ocupada);
        Task<ICollection<MesaModel>> BuscarMesas();
        Task<MesaModel> ObterMesa(int mesaId);
    }
}
