using Microsoft.AspNetCore.Mvc;

namespace Aklion.Crm.Controllers
{
    public class BaseController : Controller
    {
        public IActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }

            return RedirectToAction("Index", "Home");
        }

        public UserContext.UserContext UserContext { get; set; }

        public bool IsUserContextInitialized => UserContext != null && UserContext.UserId > 0;
    }
}