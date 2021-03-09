using System;
using System.Collections.Generic;
using RestauranteApp.Contexto;
using RestauranteApp.DatabaseControl;
using RestauranteApp.Services.Mesa.Models;
using System.Linq;

namespace RestauranteApp.Services.Mesa
{
    class MesaService
    {
        private static float ValorRodizio = 45.0F;

        public static float ObterValorRodizio()
        {
            return ValorRodizio;
        }

        public static void AtualizarStatusMesa(int mesaId, bool ocupada)
        {
            var context = new RestauranteContext();

            var mesa = context.Mesa
                        .Where(m => m.MesaId == mesaId)
                        .FirstOrDefault();

            mesa.Ocupada = ocupada;

            if (context.SaveChanges() <= 0)
                throw new Exception("Não foi possível atualizar o status da mesa para 'Ocupada'!");
        }

        public static bool ValidarMesa(int mesaId)
        {
            var context = new RestauranteContext();

            return context.Mesa
                    .ToList()
                    .Exists(m => m.MesaId == mesaId);
        }

        public static List<MesaListagemModel> ObterMesas(bool apenasDisponiveis = false)
        {
            var context = new RestauranteContext();

            var listaMesas = context.Mesa
                            .Where(m => m.Ocupada == false)
                            .Select(m => new MesaListagemModel {
                                MesaId = m.MesaId,
                                Ocupada = m.Ocupada
                            })
                            .OrderBy(m => m.MesaId)
                            .ToList();

            return listaMesas;
        }

        public static bool MesaOcupada(int mesaId)
        {
            var context = new RestauranteContext();

            var mesa = context.Mesa
                        .Where(m => m.MesaId == mesaId)
                        .FirstOrDefault();

            if (!ValidarMesa(mesaId)) throw new Exception("A mesa selecionada não existe!");

            return mesa.Ocupada;
        }

        public static int ObterQuantidadeClientes(int mesaId)
        {
            var context = new RestauranteContext();

            var mesa = context.Mesa
                        .Where(m => m.MesaId == mesaId)
                        .FirstOrDefault();

            return mesa.Capacidade;
        }

        public static bool QuantidadeClientesValida(int mesaId, int quantidadeClientes)
        {
            var context = new RestauranteContext();

            var mesa = context.Mesa
                        .Where(m => m.MesaId == mesaId)
                        .FirstOrDefault();

            return !(quantidadeClientes > mesa.Capacidade || quantidadeClientes <= 0);
        }
    }
}
