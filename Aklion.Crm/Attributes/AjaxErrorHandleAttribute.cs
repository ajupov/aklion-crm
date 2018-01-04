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
                Error = $"Произошла ошибка: {context.Exception.Message}" +
                        $"Метод: {context.Exception.TargetSite.Name}." +
                        $"Трассировка стека: {context.Exception.StackTrace}"
            };

            context.Result = new JsonResult(data);
            context.HttpContext.Response.StatusCode = 500;
            context.ExceptionHandled = true;
        }
    }
}