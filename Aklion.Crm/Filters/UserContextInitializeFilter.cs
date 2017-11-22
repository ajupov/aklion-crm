using System.Threading.Tasks;
using Aklion.Crm.Controllers;
using Aklion.Crm.Dao.CrmUserContext;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Aklion.Crm.Filters
{
    public class UserContextInitializeFilter : IActionFilter, IAsyncActionFilter
    {
        private readonly ICrmUserContextDao _userContextDao;

        public UserContextInitializeFilter(ICrmUserContextDao userContextDao)
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
            const int selectedStoreId = 2;

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

                baseController.UserContext = new UserContext.UserContext
                {
                    UserId = userContextDomain.UserId,
                    UserLogin = userContextDomain.UserLogin,
                    IsEmailConfirmed = userContextDomain.IsEmailConfirmed,
                    IsPhoneConfirmed = userContextDomain.IsPhoneConfirmed,
                    IsLocked = userContextDomain.IsLocked,
                    IsDeleted = userContextDomain.IsDeleted,
                    AvatarUrl = userContextDomain.AvatarUrl,
                    StoreId = userContextDomain.StoreId,
                    StoreName = userContextDomain.StoreName,
                    StoreIsLocked = userContextDomain.StoreIsLocked,
                    StoreIsDeleted = userContextDomain.StoreIsDeleted,
                    Permissions = userContextDomain.Permissions,
                    AvialableStores = userContextDomain.AvialableStores
                };
            }

            await next().ConfigureAwait(false);
        }
    }
}