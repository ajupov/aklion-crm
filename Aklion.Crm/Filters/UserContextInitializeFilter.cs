using System.Linq;
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

                baseController.UserContext = new UserContext.UserContext
                {
                    UserId = userContextDomain.CurrentUser.Id,
                    UserLogin = userContextDomain.CurrentUser.Login,
                    IsEmailConfirmed = userContextDomain.CurrentUser.IsEmailConfirmed,
                    IsPhoneConfirmed = userContextDomain.CurrentUser.IsPhoneConfirmed,
                    IsLocked = userContextDomain.CurrentUser.IsLocked,
                    IsDeleted = userContextDomain.CurrentUser.IsDeleted,
                    AvatarUrl = userContextDomain.CurrentUser.AvatarUrl,
                    StoreId = userContextDomain.CurrentStore.Id,
                    StoreName = userContextDomain.CurrentStore.Name,
                    StoreIsLocked = userContextDomain.CurrentStore.IsLocked,
                    StoreIsDeleted = userContextDomain.CurrentStore.IsDeleted,
                    Permissions = userContextDomain.CurrentStorePermissions.Select(s => s.Permission).ToList(),
                    AvialableStores = userContextDomain.Stores.ToDictionary(k => k.Id, v => v.Name)
                };

                baseController.ViewBag.UserContext = baseController.UserContext;
                baseController.ViewBag.IsUserContextInitialized = baseController.IsUserContextInitialized;
            }

            await next().ConfigureAwait(false);
        }
    }
}