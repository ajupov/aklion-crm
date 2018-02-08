using Microsoft.AspNetCore.Mvc.Filters;

namespace Aklion.Crm.Filters
{
    public class LogFileFilter : IActionFilter
    {
        public void OnActionExecuting(ActionExecutingContext context)
        {
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
        }
    }
}