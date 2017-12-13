using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Aklion.Crm.Filters
{
    public class LogFileFilter : IActionFilter, IAsyncActionFilter
    {
        public void OnActionExecuting(ActionExecutingContext context)
        {
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {

        }
    }
}