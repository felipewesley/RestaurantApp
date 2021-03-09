using System;
using Microsoft.EntityFrameworkCore;
using RestauranteApp.Entidades;

namespace RestauranteApp.Contexto
{
    class RestauranteContext : DbContext
    {
        public DbSet<Comanda> Comanda { get; set; }
        public DbSet<Mesa> Mesa { get; set; }
        public DbSet<Pedido> Pedido { get; set; }
        public DbSet<Produto> Produto { get; set; }
        public DbSet<Status> Status { get; set; }
        public DbSet<TipoProduto> TipoProduto { get; set; }

        public RestauranteContext() { }
        public RestauranteContext(DbContextOptions<RestauranteContext> options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Integrated Security=SSPI;Persist Security Info=False;User ID=felipe.basso;Initial Catalog=felipe.basso;Data Source=SERVER");
        }
    }
}
