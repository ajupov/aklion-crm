using Microsoft.AspNetCore.Mvc;

namespace Aklion.Crm.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return RedirectToAction("Index", "Organizations");
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}