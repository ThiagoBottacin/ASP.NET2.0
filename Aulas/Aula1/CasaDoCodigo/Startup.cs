using System;
using CasaDoCodigo.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CasaDoCodigo
{
    public partial class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Recupera a string de conexão ao banco de dados
            string connectionString = Configuration.GetConnectionString("Default");

            services.AddMvc();
            services.AddDistributedMemoryCache();
            services.AddSession();

            // Atribui a classe "ApplicationContext" para o DbContext passando a connection string
            services.AddDbContext<ApplicationContext>(options =>
                options.UseSqlServer(connectionString)
            );

            #region Injeçôes de Dependência

            services.AddTransient<IDataService, DataService>();
            services.AddTransient(typeof(IRepositoryBase<>), typeof(RepositoryBase<>));

            services.AddTransient<IProdutoRepository, ProdutoRepository>();
            services.AddTransient<ICadastroRepository, CadastroRepository>();
            services.AddTransient<IPedidoRepository, PedidoRepository>();
            services.AddTransient<IItemPedidoRepository, ItemPedidoRepository>();
            #endregion
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, 
            IServiceProvider serviceProvider)
        {
            if (env.IsDevelopment())
            {
                app.UseBrowserLink();
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();
            app.UseSession();
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Pedido}/{action=Carrossel}/{codigo?}");
            });

            serviceProvider.GetService<IDataService>().InicializaDB();
        }
    }
}
