using System.Threading.Tasks;
using Aklion.Crm.Dao.Analytics;
using Aklion.Crm.Mappers.Analytics;
using Aklion.Crm.Models.Analytics;
using Microsoft.AspNetCore.Mvc;

namespace Aklion.Crm.Controllers.Administration
{
    public class ConsoleController : BaseController
    {
        private readonly IAnalyticsDao _dao;

        public ConsoleController(IAnalyticsDao dao)
        {
            _dao = dao;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View("~/Views/Administration/Console/Index.cshtml");
        }

        [HttpGet]
        public async Task<GeneralCountsModel> GetGeneralCounts()
        {
            var result = await _dao.GetGeneralCountsAsync(UserContext.UserId).ConfigureAwait(false);
            return result.MapNew();
        }
    }
}