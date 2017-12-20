using Aklion.Crm.Business.ImageLoad;
using Aklion.Crm.Business.Mail;
using Aklion.Crm.Business.Mail.Models;
using Aklion.Crm.Business.Permission;
using Aklion.Crm.Business.Sms;
using Aklion.Crm.Business.Sms.Models;
using Aklion.Crm.Business.Store;
using Aklion.Crm.Business.UserToken;
using Aklion.Crm.Dao.AuditLog;
using Aklion.Crm.Dao.Client;
using Aklion.Crm.Dao.ClientAttribute;
using Aklion.Crm.Dao.ClientAttributeLink;
using Aklion.Crm.Dao.Order;
using Aklion.Crm.Dao.OrderAttribute;
using Aklion.Crm.Dao.OrderAttributeLink;
using Aklion.Crm.Dao.OrderItem;
using Aklion.Crm.Dao.OrderSource;
using Aklion.Crm.Dao.OrderStatus;
using Aklion.Crm.Dao.Product;
using Aklion.Crm.Dao.ProductAttribute;
using Aklion.Crm.Dao.ProductAttributeLink;
using Aklion.Crm.Dao.ProductStatus;
using Aklion.Crm.Dao.Store;
using Aklion.Crm.Dao.User;
using Aklion.Crm.Dao.UserAttribute;
using Aklion.Crm.Dao.UserAttributeLink;
using Aklion.Crm.Dao.UserContext;
using Aklion.Crm.Dao.UserPermission;
using Aklion.Crm.Dao.UserToken;
using Aklion.Crm.Filters;
using Aklion.Infrastructure.ApiClient;
using Aklion.Infrastructure.ConnectionFactory;
using Aklion.Infrastructure.Dao;
using Aklion.Infrastructure.DataBaseExecutor;
using Aklion.Infrastructure.Logger;
using Aklion.Infrastructure.Reader;
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
                .AddSingleton<IApiClient, ApiClient>()
                .AddSingleton<IConnectionFactory, ConnectionFactory>()
                .AddSingleton<IDataBaseExecutor, DataBaseExecutor>()
                .AddSingleton<IDao, Infrastructure.Dao.Dao>()
                .AddSingleton<IReader, Reader>()
                .AddSingleton<ILogger, Logger>()
                .AddSingleton<IReader, Reader>()
                .AddSingleton<IImageLoadService, ImageLoadService>()
                .AddSingleton<IMailService, MailService>()
                .AddSingleton<IPermissionService, PermissionService>()
                .AddSingleton<ISmsService, SmsService>()
                .AddSingleton<IStoreService, StoreService>()
                .AddSingleton<IUserTokenService, UserTokenService>()
                .AddSingleton<IAuditLogDao, AuditLogDao>()
                .AddSingleton<IClientDao, ClientDao>()
                .AddSingleton<IClientAttributeDao, ClientAttributeDao>()
                .AddSingleton<IClientAttributeLinkDao, ClientAttributeLinkDao>()
                .AddSingleton<IOrderDao, OrderDao>()
                .AddSingleton<IOrderAttributeDao, OrderAttributeDao>()
                .AddSingleton<IOrderAttributeLinkDao, OrderAttributeLinkDao>()
                .AddSingleton<IOrderItemDao, OrderItemDao>()
                .AddSingleton<IOrderSourceDao, OrderSourceDao>()
                .AddSingleton<IOrderStatusDao, OrderStatusDao>()
                .AddSingleton<IProductDao, ProductDao>()
                .AddSingleton<IProductAttributeDao, ProductAttributeDao>()
                .AddSingleton<IProductAttributeLinkDao, ProductAttributeLinkDao>()
                .AddSingleton<IProductStatusDao, ProductStatusDao>()
                .AddSingleton<IStoreDao, StoreDao>()
                .AddSingleton<IUserDao, UserDao>()
                .AddSingleton<IUserAttributeDao, UserAttributeDao>()
                .AddSingleton<IUserAttributeLinkDao, UserAttributeLinkDao>()
                .AddSingleton<IUserContextDao, UserContextDao>()
                .AddSingleton<IUserPermissionDao, UserPermissionDao>()
                .AddSingleton<IUserTokenDao, UserTokenDao>()
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