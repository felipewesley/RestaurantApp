using Microsoft.EntityFrameworkCore;
using Restaurante.Dominio;

namespace Restaurante.Repositorio.Contexto
{
    public class RestauranteContexto : DbContext
    {
        public DbSet<Comanda> Comanda { get; set; }
        public DbSet<Mesa> Mesa { get; set; }
        public DbSet<Pedido> Pedido { get; set; }
        public DbSet<Produto> Produto { get; set; }
        public DbSet<Status> Status { get; set; }
        public DbSet<TipoProduto> TipoProduto { get; set; }

        public RestauranteContexto() { }
        public RestauranteContexto(DbContextOptions<RestauranteContexto> options) : base(options) { }
    }
}
