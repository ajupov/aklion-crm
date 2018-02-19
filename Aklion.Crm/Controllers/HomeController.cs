using Aklion.Crm.Enums;
using Microsoft.AspNetCore.Mvc;

namespace Aklion.Crm.Controllers
{
    public class HomeController : BaseController
    {
        public IActionResult Index()
        {
            return IsUserContextInitialized
                ? (IActionResult) RedirectToAction("Index", UserContext.Permissions.Contains(Permission.Admin)
                    ? "AdministrationConsole"
                    : "UserConsole")
                : View();
        }
    }
}