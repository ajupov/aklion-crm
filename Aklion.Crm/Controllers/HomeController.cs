using Microsoft.AspNetCore.Mvc;

namespace Aklion.Crm.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            ViewBag.Title = "Главная";
            return View();
        }

        public IActionResult Error()
        {
            ViewBag.Title = "Ошибка";
            return View();
        }
    }
}