using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Restaurante.Repositorio.Contexto;
using Restaurante.Repositorio.Services;
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

            // Adicionando referência para classe de repositório onde haja implementação da interface I(NomeDaInterface)
            services.AddScoped<ComandaService>();
            services.AddScoped<MesaService>();
            services.AddScoped<PedidoService>();
            services.AddScoped<ProdutoService>();
            services.AddScoped<TipoProdutoService>();

            RestauranteContexto contexto = new RestauranteContexto();

            MesaService mesaService = new MesaService(contexto);
            ComandaService comandaService = new ComandaService(contexto, mesaService);

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Restaurante.WebAPI", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Restaurante.WebAPI v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
