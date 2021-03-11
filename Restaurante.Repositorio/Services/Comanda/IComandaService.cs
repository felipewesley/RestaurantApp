using Restaurante.Repositorio.Services.Comanda.Models;

namespace Restaurante.Repositorio.Services.Comanda
{
    public interface IComandaService
    {
        ComandaResumidaModel ObterComandaResumida(int comandaId);

        // ComandaCompletaModel ObterComandaCompleta(int comandaId);
    }
}
