using Microsoft.AspNetCore.Mvc;

namespace Aklion.Crm.Controllers.Mvc
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return RedirectToAction("Index", "Organization");
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}