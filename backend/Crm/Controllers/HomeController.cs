using Crm.Enums;
using Microsoft.AspNetCore.Mvc;

namespace Crm.Controllers
{
    [Route("")]
    [Route("Home")]
    public class HomeController : BaseController
    {
        [HttpGet]
        [Route("")]
        [Route("Index")]
        public IActionResult Index()
        {
            if (!IsUserContextInitialized)
            {
                return View();
            }

            if (UserContext.Permissions.Contains(Permission.Admin))
            {
                return RedirectToAction("Index", "AdministrationConsole");
            }
            else
            {
                return RedirectToAction("Index", "Console");
            }
        }
    }
}