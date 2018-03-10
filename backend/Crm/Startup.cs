using Crm.Business.AuditLog;
using Crm.Business.ImageLoad;
using Crm.Business.Mail;
using Crm.Business.Mail.Models;
using Crm.Business.Permission;
using Crm.Business.Sms;
using Crm.Business.Sms.Models;
using Crm.Business.Store;
using Crm.Business.UserPermission;
using Crm.Business.UserToken;
using Crm.Dao.Analytics;
using Crm.Dao.Client;
using Crm.Dao.ClientAttribute;
using Crm.Dao.ClientAttributeLink;
using Crm.Dao.Order;
using Crm.Dao.OrderAttribute;
using Crm.Dao.OrderAttributeLink;
using Crm.Dao.OrderItem;
using Crm.Dao.OrderSource;
using Crm.Dao.OrderStatus;
using Crm.Dao.Product;
using Crm.Dao.ProductAttribute;
using Crm.Dao.ProductAttributeLink;
using Crm.Dao.ProductImageKey;
using Crm.Dao.ProductImageKeyLink;
using Crm.Dao.ProductStatus;
using Crm.Dao.Store;
using Crm.Dao.User;
using Crm.Dao.UserAttribute;
using Crm.Dao.UserAttributeLink;
using Crm.Dao.UserContext;
using Crm.Dao.UserPermission;
using Crm.Dao.UserToken;
using Crm.Filters;
using Infrastructure.ApiClient;
using Infrastructure.AuditLogger;
using Infrastructure.ConnectionFactory;
using Infrastructure.Dao;
using Infrastructure.DataBaseExecutor;
using Infrastructure.DataBaseExecutor.Reader;
using Infrastructure.Logger;
using Infrastructure.Logger.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json.Serialization;

namespace Crm
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
                 .AddSingleton<IHttpContextAccessor, HttpContextAccessor>()
                 .AddSingleton<IApiClient, ApiClient>()
                 .AddSingleton<IAuditLogger, AuditLogger>()
                 .AddSingleton<IConnectionFactory, ConnectionFactory>()
                 .AddSingleton<IDataBaseExecutor, DataBaseExecutor>()
                 .AddSingleton<IDao, Infrastructure.Dao.Dao>()
                 .AddSingleton<IReader, Reader>()
                 .AddSingleton<ILogger, Logger>()
                 .AddSingleton<IReader, Reader>()
                 .AddSingleton<IAuditLogService, AuditLogService>()
                 .AddSingleton<IImageLoadService, ImageLoadService>()
                 .AddSingleton<IMailService, MailService>()
                 .AddSingleton<IPermissionService, PermissionService>()
                 .AddSingleton<ISmsService, SmsService>()
                 .AddSingleton<IStoreService, StoreService>()
                 .AddSingleton<IUserPermissionService, UserPermissionService>()
                 .AddSingleton<IUserTokenService, UserTokenService>()
                 .AddSingleton<IAnalyticsDao, AnalyticsDao>()
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
                 .AddSingleton<IProductImageKeyDao, ProductImageKeyDao>()
                 .AddSingleton<IProductImageKeyLinkDao, ProductImageKeyLinkDao>()
                 .AddSingleton<IProductStatusDao, ProductStatusDao>()
                 .AddSingleton<IStoreDao, StoreDao>()
                 .AddSingleton<IUserDao, UserDao>()
                 .AddSingleton<IUserAttributeDao, UserAttributeDao>()
                 .AddSingleton<IUserAttributeLinkDao, UserAttributeLinkDao>()
                 .AddSingleton<IUserContextDao, UserContextDao>()
                 .AddSingleton<IUserPermissionDao, UserPermissionDao>()
                 .AddSingleton<IUserTokenDao, UserTokenDao>()
                 .Configure<LoggerConfiguration>(Configuration.GetSection("LoggerConfiguration"))
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
                     o.Filters.Add(typeof(LogFileFilter));
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
            app.UseMvc(r =>
            {
                r.MapRoute("default", "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}