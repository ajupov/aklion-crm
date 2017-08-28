using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Aklion.Crm.Controllers
{
    public class OrganizationsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> GetList()
        {
            using (var client = new HttpClient())
            {
                var str = await client.GetStringAsync("localhost:9001/api/v1/organizations").ConfigureAwait(false);
            }
            var organizations = 
        }
    }
}