using System.Threading.Tasks;
using Aklion.Crm.Controllers;
using Aklion.Crm.Dao.UserContext;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Aklion.Crm.Filters
{
    public class UserContextInitializeFilter : IActionFilter, IAsyncActionFilter
    {
        private readonly IUserContextDao _userContextDao;

        public UserContextInitializeFilter(IUserContextDao userContextDao)
        {
            _userContextDao = userContextDao;
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var isAuthenticated = context.HttpContext?.User?.Identity?.IsAuthenticated ?? false;
            var login = context.HttpContext?.User?.Identity?.Name;
            var selectedStoreId = 2;

            if (!isAuthenticated || string.IsNullOrWhiteSpace(login))
            {
                await next().ConfigureAwait(false);
                return;
            }

            if (context.Controller.GetType().BaseType != typeof(BaseController))
            {
                await next().ConfigureAwait(false);
                return;
            }

            if (context.Controller is BaseController baseController)
            {
                var userContextDomain = await _userContextDao.Get(login, selectedStoreId).ConfigureAwait(false);
                if (userContextDomain == null)
                {
                    await next().ConfigureAwait(false);
                    return;
                }

                baseController.InitializeUserContext(userContextDomain);
            }

            await next().ConfigureAwait(false);
        }
    }
}