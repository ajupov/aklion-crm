using System.Threading.Tasks;
using Aklion.Crm.Dao.Store.Models;
using Aklion.Crm.Helpers;
using Aklion.Crm.Models.JqGrid;
using Aklion.Crm.Models.Store;
using Microsoft.AspNetCore.Mvc;

namespace Aklion.Crm.Controllers
{
    public class StoresController : Controller
    {
        private readonly IJqGridHelper _jqGridHelper;

        public StoresController(IJqGridHelper jqGridHelper)
        {
            _jqGridHelper = jqGridHelper;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<JqGridDataModel> Get(StoreGetModel model)
        {
            return await _jqGridHelper.GetData<Store>(model).ConfigureAwait(false);
        }

        [HttpPost]
        public async Task Edit(StoreEditModel model)
        {
            await _jqGridHelper.Edit<Store>(model).ConfigureAwait(false);
        }
    }
}