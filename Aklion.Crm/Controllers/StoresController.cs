using System;
using System.Linq;
using System.Threading.Tasks;
using Aklion.Crm.Dao.Store.Models;
using Aklion.Crm.Models;
using Aklion.Crm.Models.Store;
using Aklion.Infrastructure.Storage.Repository;
using Aklion.Infrastructure.Utils.Mapper;
using Microsoft.AspNetCore.Mvc;

namespace Aklion.Crm.Controllers
{
    public class StoresController : Controller
    {
        private readonly IRepository _repository;

        public StoresController(IRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<BaseGetListResponseModel<StoreGetResponseModel>> GetList(StoreGetRequestRequestModel model)
        {
            var result = await _repository.Get<Store>(model).ConfigureAwait(false);

            //TODO Написать маппер для списка моделей
            var list = result.Item2.Select(x => x.MapNew<Store, StoreGetResponseModel>()).ToList();

            return new BaseGetListResponseModel<StoreGetResponseModel>(list, result.Item1, model.Page, model.Size);
        }

        [HttpGet]
        public async Task<StoreGetResponseModel> Get(int id)
        {
            var model = await _repository.Get<Store>(id).ConfigureAwait(false);

            return model.MapNew<Store, StoreGetResponseModel>();
        }

        [HttpPost]
        public async Task Create(StoreCreateRequestRequestModel model)
        {
            var newModel = model.MapNew<StoreCreateRequestRequestModel, Store>();
            newModel.CreateDate = DateTime.Now;

            await _repository.Create(newModel).ConfigureAwait(false);
        }

        [HttpPost]
        public async Task Update(StoreUpdateRequestRequestModel model)
        {
            var store = await _repository.Get<Store>(model.Id).ConfigureAwait(false);

            model.Map(store);
            store.ModifyDate = DateTime.Now;

            await _repository.Update(store).ConfigureAwait(false);
        }

        [HttpPost]
        public async Task Delete(int id)
        {
            await _repository.Delete<Store>(id).ConfigureAwait(false);
        }
    }
}