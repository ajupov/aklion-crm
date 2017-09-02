using System.Threading.Tasks;
using Aklion.Crm.Dao.Store;
using Aklion.Crm.Mappers;
using Microsoft.AspNetCore.Mvc;

namespace Aklion.Crm.Controllers
{
    public class StoresController : Controller
    {
        private readonly IStoreDao _storeDao;

        public StoresController(IStoreDao storeDao)
        {
            _storeDao = storeDao;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> GetList()
        {
            var result = await _storeDao.GetList(0, int.MaxValue).ConfigureAwait(false);
            return PartialView("Partials/Stores", result.Map());
        }
    }
}