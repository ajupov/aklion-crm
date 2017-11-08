using System.Threading.Tasks;
using Aklion.Crm.Attributes;
using Aklion.Crm.Dao.Store;
using Aklion.Crm.Mappers.Store;
using Aklion.Crm.Models;
using Aklion.Crm.Models.Administration.Store;
using Microsoft.AspNetCore.Mvc;

namespace Aklion.Crm.Controllers.Administration
{
    [Route("Administration/Stores")]
    public class AdministrationStoreController : Controller
    {
        private readonly IStoreDao _storeDao;

        public AdministrationStoreController(IStoreDao storeDao)
        {
            _storeDao = storeDao;
        }

        [HttpGet]
        [Route("")]
        [Route("Index")]
        public IActionResult Index()
        {
            return View("~/Views/Administration/Store/Index.cshtml");
        }

        [HttpGet]
        [Route("GetList")]
        public async Task<PagingModel<StoreModel>> GetList(StoreParameterModel model)
        {
            var result = await _storeDao.GetPagedList(model.Map()).ConfigureAwait(false);

            return result.Map(model.Page, model.Size);
        }

        [HttpPost]
        [Route("Create")]
        [AjaxErrorHandle]
        public async Task Create(StoreModel model)
        {
            var store = model.Map();

            await _storeDao.Create(store).ConfigureAwait(false);
        }

        [HttpPost]
        [Route("Update")]
        [AjaxErrorHandle]
        public async Task Update(StoreModel model)
        {
            var store = await _storeDao.Get(model.Id).ConfigureAwait(false);
            if (store == null)
            {
                return;
            }

            model.Map(store);

            await _storeDao.Update(store).ConfigureAwait(false);
        }

        [HttpPost]
        [Route("Delete")]
        [AjaxErrorHandle]
        public async Task Delete(int id)
        {
            await _storeDao.Delete(id).ConfigureAwait(false);
        }
    }
}