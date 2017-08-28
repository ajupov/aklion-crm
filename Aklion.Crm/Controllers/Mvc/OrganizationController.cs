using Microsoft.AspNetCore.Mvc;

namespace Aklion.Crm.Controllers.Mvc
{
    public class OrganizationController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}