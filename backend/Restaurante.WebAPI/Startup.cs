using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Restaurante.Repositorio.Contexto;
using Restaurante.Repositorio.Services.Mesa;
using Restaurante.Repositorio.Services.Pedido;
using Restaurante.Repositorio.Services.Comanda;
using Restaurante.Repositorio.Services.Produto;
using Restaurante.Repositorio.Services.TipoProduto;

namespace Restaurante.WebAPI
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Disponibilizando a classe de contexto para a aplicação
            services.AddDbContext<RestauranteContexto>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
            });

            // Adicionando referência para classe de repositório
            services.AddScoped<ComandaService>();
            services.AddScoped<MesaService>();
            services.AddScoped<PedidoService>();
            services.AddScoped<ProdutoService>();
            services.AddScoped<TipoProdutoService>();

            // Instancia do contexto para referenciar nas services necessarias
            RestauranteContexto contexto = new RestauranteContexto();

            // Referenciando MesaService dentro de ComandaService
            MesaService mesaService = new MesaService(contexto);
            ComandaService comandaService = new ComandaService(contexto, mesaService);

            services.AddControllers();

            services.AddCors(o => o.AddPolicy("MyPolicy", builder =>
            {
                builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
            }));

            services.AddMvc(option => option.EnableEndpointRouting = false);

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors("MyPolicy");

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseMvc();
        }
    }
}
