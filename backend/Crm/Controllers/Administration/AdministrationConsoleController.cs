using System.Threading.Tasks;
using Crm.Attributes;
using Crm.Dao.Analytics;
using Crm.Mappers.Analytics;
using Crm.Models.Analytics;
using Microsoft.AspNetCore.Mvc;

namespace Crm.Controllers.Administration
{
    [AjaxErrorHandle]
    [Route("Administration/Console")]
    public class AdministrationConsoleController : BaseController
    {
        private readonly IAnalyticsDao _dao;

        public AdministrationConsoleController(IAnalyticsDao dao)
        {
            _dao = dao;
        }

        [HttpGet]
        [Route("")]
        [Route("Index")]
        public IActionResult Index()
        {
            return View("~/Views/Administration/Console/Index.cshtml");
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