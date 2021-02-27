using System;
using System.Collections.Generic;
using RestauranteApp.DatabaseControl;
using RestauranteApp.Services.Mesa.Models;

namespace RestauranteApp.Services.Mesa
{
    class MesaService
    {
        public static float ValorRodizio = 45.0F;

        private static Entidades.Mesa ObterMesaEntidade(int mesaId)
        {
            string mesaCsv = Database.Select(Entidade.Mesa, mesaId);

            //  if (mesaCsv == null || mesaCsv == string.Empty) throw new Exception("A mesa selecionada nao existe!");
            if (mesaCsv == null || mesaCsv == string.Empty) return null;

            return new Entidades.Mesa().ConverterEmEntidade(mesaCsv);
        }

        public static bool ValidarMesa(int mesaId)
        {
            return !(mesaId <= 0 || mesaId > 16);
        }

        public static List<MesaListagemModel> ObterMesas(bool apenasDisponiveis = false)
        {
            string[] mesasCsv = Database.Select(Entidade.Mesa);
            List<MesaListagemModel> listaMesas = new List<MesaListagemModel>();

            foreach (string mesaCsv in mesasCsv)
            {
                var mesa = new Entidades.Mesa().ConverterEmEntidade(mesaCsv);

                if (apenasDisponiveis)
                {
                    if (mesa.Ocupada) continue;
                }
                listaMesas.Add(new MesaListagemModel()
                {
                    MesaId = mesa.MesaId,
                    Ocupada = mesa.Ocupada
                });
            }
            return listaMesas;
        }

        public static bool MesaOcupada(int mesaId)
        {
            var mesa = ObterMesaEntidade(mesaId);

            if (!ValidarMesa(mesaId)) throw new Exception("A mesa selecionada não existe!");

            return mesa.Ocupada;
        }

        public static int ObterQuantidadeClientes(int mesaId)
        {
            var mesa = ObterMesaEntidade(mesaId);

            return mesa.Capacidade;
        }

        public static bool QuantidadeClientesValida(int mesaId, int quantidadeClientes)
        {
            var mesa = ObterMesaEntidade(mesaId);

            return !(quantidadeClientes > mesa.Capacidade || quantidadeClientes <= 0);
        }
    }
}
