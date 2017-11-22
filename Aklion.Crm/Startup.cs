using Aklion.Crm.Business.ImageLoad;
using Aklion.Crm.Business.Mail;
using Aklion.Crm.Business.Mail.Models;
using Aklion.Crm.Business.Permission;
using Aklion.Crm.Business.Sms;
using Aklion.Crm.Business.Sms.Models;
using Aklion.Crm.Business.UserToken;
using Aklion.Crm.Dao.Attribute;
using Aklion.Crm.Dao.Category;
using Aklion.Crm.Dao.CrmUserContext;
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
using Aklion.Crm.Filters;
using Aklion.Crm.Models;
using Aklion.Infrastructure.Storage.ConnectionFactory;
using Aklion.Infrastructure.Storage.DataBaseExecutor;
using Aklion.Infrastructure.Storage.DataBaseExecutor.Readers;
using Aklion.Infrastructure.Utils.Logger;
using Aklion.Infrastructure.Utils.UserContext;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
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
                .AddSingleton<IReader, Reader>()
                .AddSingleton<ILogger, Logger>()
                .AddSingleton<IPermissionService, PermissionService>()
                .AddSingleton<IUserDao, UserDao>()
                .AddSingleton<ICrmUserContextDao, CrmUserContextDao>()
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
                .AddSingleton<IMailService, MailService>()
                .AddSingleton<ISmsService, SmsService>()
                .AddSingleton<IImageLoadService, ImageLoadService>()
                .AddSingleton<IUserTokenService, UserTokenService>()
                .AddScoped<IUserContext, UserContext.UserContext>()

                .Configure<MailServiceConfiguration>(Configuration.GetSection("MailServiceConfiguration"))
                .Configure<SmsServiceConfiguration>(Configuration.GetSection("SmsServiceConfiguration"));

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(o => 
                {
                    o.LoginPath = new PathString("/Account/Login");
                });

            services.AddMvc(o =>
                {
                    o.Filters.Add(typeof(UserContextInitializeFilter));
                })
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
            app.UseAuthentication();

            app.UseMvc(r => r.MapRoute("default", "{controller=Home}/{action=Index}/{id?}"));
        }
    }
}