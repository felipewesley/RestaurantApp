using Restaurante.Repositorio.Services.Comanda.Models;
using System.Threading.Tasks;

namespace Restaurante.Repositorio.Services.Comanda
{
    public interface IComandaService
    {
        void RegistrarComanda(ComandaFormularioModel comandaModel);

        void EncerrarComanda(int comandaId, bool porcentagemGarcom = false);

        ComandaResumidaModel ObterComandaResumida(int comandaId);

        Task<ComandaCompletaModel> ObterComandaCompleta(int comandaId);
    }
}
