using Microsoft.AspNetCore.Mvc;

namespace Aklion.Crm.Controllers.Administration
{
    [Route("Console")]
    public class ConsoleController : BaseController
    {
        public IActionResult Index()
        {
            return View("~/Views/Administration/Console/Index.cshtml");
        }
    }
}