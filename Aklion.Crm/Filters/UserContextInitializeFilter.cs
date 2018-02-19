using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Aklion.Crm.Controllers;
using Aklion.Crm.Dao.UserContext;
using Microsoft.AspNetCore.Mvc;
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

        public Task OnActionExecution(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            return ActionExecution(context, next);
        }

        public Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            return ActionExecution(context, next);
        }

        public async Task ActionExecution(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var controller = context.Controller as Controller;
            if (controller == null)
            {
                await next().ConfigureAwait(false);
                return;
            }

            controller.ViewBag.UserContext = null;
            controller.ViewBag.IsUserContextInitialized = false;

            var isAuthenticated = context.HttpContext?.User?.Identity?.IsAuthenticated ?? false;

            var userIdClaim = context.HttpContext?.User?.Claims.FirstOrDefault(c => c.Type == ClaimTypes.PrimarySid);
            var storeIdClaim = context.HttpContext?.User?.Claims.FirstOrDefault(c => c.Type == "StoreId");

            if (!isAuthenticated || userIdClaim == null)
            {
                await next().ConfigureAwait(false);
                return;
            }

            var userId = int.Parse(userIdClaim.Value);
            if (userId <= 0)
            {
                await next().ConfigureAwait(false);
                return;
            }

            int.TryParse(storeIdClaim?.Value, out var storeId);

            if (context.Controller.GetType().BaseType != typeof(BaseController))
            {
                await next().ConfigureAwait(false);
                return;
            }

            if (context.Controller is BaseController baseController)
            {
                var userContextDomain = await _userContextDao.GetAsync(userId, storeId).ConfigureAwait(false);
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