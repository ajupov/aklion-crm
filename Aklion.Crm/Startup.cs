using Aklion.Crm.Dao.Post;
using Aklion.Crm.Dao.Store;
using Aklion.Crm.Dao.User;
using Aklion.Crm.Dao.UserPost;
using Aklion.Infrastructure.Storage.ConnectionFactory;
using Aklion.Infrastructure.Storage.DataBaseExecutor;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json.Serialization;

namespace Aklion.Crm
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", false, true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", true)
                .AddEnvironmentVariables();

            Configuration = builder.Build();
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton(Configuration)
                .AddSingleton<IConnectionFactory, ConnectionFactory>()
                .AddSingleton<IDataBaseExecutor, DataBaseExecutor>()
                .AddSingleton<IUserDao, UserDao>()
                .AddSingleton<IStoreDao, StoreDao>()
                .AddSingleton<IPostDao, PostDao>()
                .AddSingleton<IUserPostDao, UserPostDao>()
                .AddMvc()
                .AddJsonOptions(o =>
                {
                    o.SerializerSettings.ContractResolver = new DefaultContractResolver
                    {
                        IgnoreSerializableAttribute = false
                    };
                });
        }
        
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseMvc(r => r.MapRoute("default", "{controller=Home}/{action=Index}/{id?}"));
        }
    }
}