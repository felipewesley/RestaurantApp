using System.Threading.Tasks;
using Restaurante.Repositorio.Services.Comanda.Models;

namespace Restaurante.Repositorio.Services.Comanda
{
    public interface IComandaService
    {
        Task Registrar(FormularioModel model);
        Task Encerrar(int comandaId, bool porcentagemGarcom = false);
        Task<ResumidaModel> ObterResumida(int comandaId);
        Task<CompletaModel> ObterCompleta(int comandaId);
    }
}
