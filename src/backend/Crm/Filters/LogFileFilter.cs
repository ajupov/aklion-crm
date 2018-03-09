using Crm.Controllers;
using Infrastructure.Logger;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Crm.Filters
{
    public class LogFileFilter : IActionFilter
    {
        private readonly ILogger _logger;

        public LogFileFilter(ILogger logger)
        {
            _logger = logger;
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            var tag = context.Controller.GetType().Name;
            var queryString = context.HttpContext.Request.Path.Value;

            var userId = 0;
            var storeId = 0;
            string userLogin = null;

            if (context.Controller is BaseController baseController)
            {
                if (baseController.IsUserContextInitialized)
                {
                    userId = baseController.UserContext.UserId;
                    userLogin = baseController.UserContext.UserLogin;
                    storeId = baseController.UserContext.StoreId;
                }
            }

            var data = new
            {
                QueryString = queryString,
                UserId = userId,
                UserLogin = userLogin,
                StoreId = storeId
            };

            if (context.Exception == null)
            {
                _logger.Info(tag, "Запрос к контроллеру", data);
            }
            else
            {
                _logger.Error(tag, "Запрос к контроллеру", data, context.Exception);
            }
        }
    }
}