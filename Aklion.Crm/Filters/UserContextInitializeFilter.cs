using System.Collections.Generic;
using System.Threading.Tasks;
using Aklion.Crm.Controllers;
using Aklion.Crm.Dao.User;
using Aklion.Crm.Enums;
using Aklion.Crm.Models;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Aklion.Crm.Filters
{
    public class UserContextInitializeFilter : IActionFilter, IAsyncActionFilter
    {
        private readonly IUserDao _userDao;

        public UserContextInitializeFilter(IUserDao userDao)
        {
            _userDao = userDao;
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
                var user = await _userDao.GetByLogin(login).ConfigureAwait(false);
                if (user == null)
                {
                    await next().ConfigureAwait(false);
                    return;
                }

                baseController.UserContext = new UserContext
                {
                    UserId = user.Id,
                    StoreId = 1,
                    Permissions = new List<Permission>
                    {
                        Permission.Admin
                    }
                };
            }

            await next().ConfigureAwait(false);
        }
    }
}