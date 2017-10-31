using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Aklion.Crm.Attributes
{
    public class AjaxErrorHandleAttribute : ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            var data = new
            {
                IsSuccess = false,
                Error = "Произошла ошибка"
            };

            context.Result = new JsonResult(data);
            context.HttpContext.Response.StatusCode = 500;
            context.ExceptionHandled = true;
        }
    }
}