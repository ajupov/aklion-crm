using Microsoft.AspNetCore.Mvc;

namespace Aklion.Crm.Controllers.User
{
    [Route("Console")]
    public class ConsoleController : Controller
    {
        public IActionResult Index()
        {
            return View("~/Views/User/Console/Index.cshtml");
        }
    }
}