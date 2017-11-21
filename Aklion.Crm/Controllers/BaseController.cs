using Aklion.Infrastructure.Utils.UserContext;
using Microsoft.AspNetCore.Mvc;

namespace Aklion.Crm.Controllers
{
    public class BaseController : Controller
    {
        public BaseController(IUserContext userContext)
        {
            UserContext = userContext;
        }
        public IActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }

            return RedirectToAction("Index", "Home");
        }

        public IUserContext UserContext { get; set; }

        public bool IsUserContextInitialized => UserContext != null && UserContext.UserId > 0;
    }
}