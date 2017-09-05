using System.Collections.Generic;
using System.Threading.Tasks;
using Aklion.Crm.Dao.Store;
using Aklion.Crm.Mappers;
using Aklion.Crm.Models.Stores;
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

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<List<Store>> GetList()
        {
            var result = await _storeDao.GetList(0, int.MaxValue).ConfigureAwait(false);
            return result.Map();
            //return PartialView("Partials/Stores", result.Map());
        }

        [HttpGet]
        public async Task<IActionResult> Get(int id)
        {
            var result = await _storeDao.Get(id).ConfigureAwait(false);
            return PartialView("Partials/EditStore", result.MapToEdit());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<int> Add(AddStore model)
        {
            if (!ModelState.IsValid)
            {
                return 0;
            }

            var result = await _storeDao.Create(model.Map()).ConfigureAwait(false);
            return result;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async void Update(EditStore model)
        {
            if (!ModelState.IsValid)
            {
                return;
            }

            var result = await _storeDao.Get(model.Id).ConfigureAwait(false);

            result.Name = model.Name;

            await _storeDao.Update(result).ConfigureAwait(false);
        }
    }
}