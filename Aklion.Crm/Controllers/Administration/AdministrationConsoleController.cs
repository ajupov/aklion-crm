using Microsoft.AspNetCore.Mvc;

namespace Aklion.Crm.Controllers.Administration
{
    [Route("Administration/Console")]
    public class AdministrationConsoleController : Controller
    {
        public IActionResult Index()
        {
            return View("~/Views/Administration/Console/Index.cshtml");
        }
    }
}