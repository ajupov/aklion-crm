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
            var queryString = context.HttpContext.Request.QueryString;

            var data = new
            {
                QueryString = queryString
            };

            if (context.Exception != null)
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