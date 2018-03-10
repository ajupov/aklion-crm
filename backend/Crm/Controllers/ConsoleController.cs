using System.Threading.Tasks;
using Crm.Attributes;
using Crm.Dao.Analytics;
using Crm.Mappers.Analytics;
using Crm.Models.Analytics;
using Microsoft.AspNetCore.Mvc;

namespace Crm.Controllers
{
    [AjaxErrorHandle]
    [Route("Console")]
    public class ConsoleController : BaseController
    {
        private readonly IAnalyticsDao _dao;

        public ConsoleController(IAnalyticsDao dao)
        {
            _dao = dao;
        }

        [HttpGet]
        [Route("")]
        [Route("Index")]
        public IActionResult Index()
        {
            return View("~/Views/Console/Index.cshtml");
        }

        [HttpGet]
        [Route("GetGeneralCounts")]
        public async Task<GeneralCountsModel> GetGeneralCounts()
        {
            var result = await _dao.GetGeneralCountsAsync(UserContext.UserId).ConfigureAwait(false);
            return result.MapNew();
        }
    }
}