using System;
using RestauranteApp.Services.Status.Models;
using RestauranteApp.DatabaseControl;

namespace RestauranteApp.Services.Status
{
    class StatusService
    {
        public static StatusModel ObterStatus(int statusId)
        {
            string statusCsv = Database.Select(Entidade.Status, statusId);

            var status = new Entidades.Status().ConverterEmEntidade(statusCsv);

            return new StatusModel()
            {
                Descricao = status.Descricao
            };
        }
    }
}
