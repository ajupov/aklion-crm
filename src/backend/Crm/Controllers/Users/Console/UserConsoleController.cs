using Microsoft.AspNetCore.Mvc;

namespace Crm.Controllers.Users.Console
{
    [Route("Console")]
    public class UserConsoleController : BaseController
    {
        public IActionResult Index()
        {
            return View("~/Views/User/Console/Index.cshtml");
        }
    }
}