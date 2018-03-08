using Microsoft.AspNetCore.Mvc;

namespace Aklion.Crm.Controllers.Administration
{
    public class ConsoleController : BaseController
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View("~/Views/Administration/Console/Index.cshtml");
        }
    }
}