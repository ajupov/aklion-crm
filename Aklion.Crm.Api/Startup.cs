using Aklion.Crm.DateAccessLayer.Organization;
using Aklion.InfrastructureV1.ConnectionFactory;
using Aklion.InfrastructureV1.DataBaseExecutor;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Aklion.Crm.ApiV1
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton(Configuration)
                .AddSingleton<IConnectionFactory, MsSqlServerConnectionFactory>()
                .AddSingleton<IDataBaseExecutor, MsSqlServerDataBaseExecutor>()
                .AddSingleton<IOrganizationDao, OrganizationDao>()
                .AddMvc();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            
            app.UseMvc(r => r.MapRoute("default", "api/v1/{controller}/{action}/{id?}"));
        }
    }
}
