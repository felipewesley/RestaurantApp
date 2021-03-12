using System.Threading.Tasks;
using Restaurante.Repositorio.Services.Comanda.Models;

namespace Restaurante.Repositorio.Services.Comanda
{
    public interface IComandaService
    {
        Task RegistrarComanda(ComandaFormularioModel comandaModel);
        Task EncerrarComanda(int comandaId, bool porcentagemGarcom = false);
        Task<ComandaResumidaModel> ObterComandaResumida(int comandaId);
        Task<ComandaCompletaModel> ObterComandaCompleta(int comandaId);
    }
}
