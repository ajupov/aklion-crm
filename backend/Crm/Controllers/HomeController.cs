using System.Linq;
using System.Threading.Tasks;
using Crm.Enums;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Crm.Controllers
{
    [Route("")]
    [Route("Home")]
    public class HomeController : BaseController
    {
        private Storages.Storage storage;

        public HomeController(Storages.Storage storage)
        {
            this.storage = storage;
        }

        [HttpGet]
        [Route("")]
        [Route("Index")]
        public async Task<IActionResult> Index()
        {
            var clientAttributeLinks = await storage
                .ClientAttributeLink
  
                .Where(c => c.Attribute.Key == "")
                .ToListAsync()
                .ConfigureAwait(false);

            return View();

            if (!IsUserContextInitialized)
            {
                return View();
            }

            if (UserContext.Permissions.Contains(Permission.Admin))
            {
                return RedirectToAction("Index", "AdministrationConsole");
            }
            else
            {
                return RedirectToAction("Index", "Console");
            }
        }
    }
}