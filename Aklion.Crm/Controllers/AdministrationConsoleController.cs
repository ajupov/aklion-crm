using Microsoft.AspNetCore.Mvc;

namespace Aklion.Crm.Controllers
{
    [Route("Administration/Console")]
    public class AdministrationConsoleController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}