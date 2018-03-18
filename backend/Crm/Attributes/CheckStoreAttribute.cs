using System;
using System.Linq;
using System.Threading.Tasks;
using Crm.Controllers;
using Crm.Exceptions;
using Crm.Storages;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;

namespace Crm.Attributes
{
    [AttributeUsage(AttributeTargets.Class)]
    public class CheckStoreAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var _storage = context.HttpContext.RequestServices.GetService<Storage>();

            if (!(context.Controller is BaseController baseController))
            {
                return;
            }

            if (!baseController.IsUserContextInitialized)
            {
                return;
            }

            var store = _storage.Store.FirstOrDefault(x => x.Id == baseController.UserContext.StoreId);
            if (store == null)
            {
                throw new StoreNotFoundException();
            }

            if (store.IsDeleted)
            {
                throw new StoreIsDeletedException();
            }
        }

        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var _storage = context.HttpContext.RequestServices.GetService<Storage>();

            if (!(context.Controller is BaseController baseController))
            {
                await next().ConfigureAwait(false);
                return;
            }

            if (!baseController.IsUserContextInitialized)
            {
                await next().ConfigureAwait(false);
                return;
            }

            var store = _storage.Store.FirstOrDefault(x => x.Id == baseController.UserContext.StoreId);
            if (store == null)
            {
                throw new StoreNotFoundException();
            }

            if (store.IsDeleted)
            {
                throw new StoreIsDeletedException();
            }
        }
    }
}