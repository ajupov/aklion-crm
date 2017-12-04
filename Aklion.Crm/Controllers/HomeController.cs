using Aklion.Crm.Enums;
using Microsoft.AspNetCore.Mvc;

namespace Aklion.Crm.Controllers
{
    public class HomeController : BaseController
    {
        public IActionResult Index()
        {
            if (IsUserContextInitialized)
            {
                if (UserContext.Permissions.Exists(p => p == Permission.Admin))
                {
                    return RedirectToAction("Index", "AdministrationConsole");
                }
                else
                {
                    return RedirectToAction("Index", "Account");
                }
            }

            return View();
        }
    }
}