using Aklion.Crm.Business.Permission;
using Aklion.Crm.Dao.Attribute;
using Aklion.Crm.Dao.Category;
using Aklion.Crm.Dao.Post;
using Aklion.Crm.Dao.Product;
using Aklion.Crm.Dao.ProductAttribute;
using Aklion.Crm.Dao.ProductCategory;
using Aklion.Crm.Dao.ProductTag;
using Aklion.Crm.Dao.Store;
using Aklion.Crm.Dao.Tag;
using Aklion.Crm.Dao.User;
using Aklion.Crm.Dao.UserPermission;
using Aklion.Crm.Dao.UserPost;
using Aklion.Crm.Dao.UserToken;
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
                .AddSingleton<IPermissionService, PermissionService>()
                .AddSingleton<IUserDao, UserDao>()
                .AddSingleton<IStoreDao, StoreDao>()
                .AddSingleton<IPostDao, PostDao>()
                .AddSingleton<IUserPostDao, UserPostDao>()
                .AddSingleton<IUserPermissionDao, UserPermissionDao>()
                .AddSingleton<IUserTokenDao, UserTokenDao>()
                .AddSingleton<IProductDao, ProductDao>()
                .AddSingleton<ICategoryDao, CategoryDao>()
                .AddSingleton<IAttributeDao, AttributeDao>()
                .AddSingleton<ITagDao, TagDao>()
                .AddSingleton<IProductCategoryDao, ProductCategoryDao>()
                .AddSingleton<IProductAttributeDao, ProductAttributeDao>()
                .AddSingleton<IProductTagDao, ProductTagDao>()
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