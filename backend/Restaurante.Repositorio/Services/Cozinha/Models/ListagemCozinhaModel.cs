using System;
using Restaurante.Dominio.Enum;
using Restaurante.Repositorio.Services.Produto.Models;

namespace Restaurante.Repositorio.Services.Cozinha.Models
{
    public class ListagemCozinhaModel
    {
        public int ComandaId { get; set; }
        public int PedidoId { get; set; }
        public int MesaId { get; set; }
        public ProdutoModel Produto { get; set; }
        public int Quantidade { get; set; }
        public DateTime DataHoraRealizacao { get; set; }
        public DateTime? DataHoraEntrega { get; set; }
        public StatusEnum StatusEnum { get; set; }
    }
}
