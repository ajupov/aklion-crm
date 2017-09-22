using System;
using System.Linq;
using System.Threading.Tasks;
using Aklion.Crm.Dao.Store.Models;
using Aklion.Crm.Models;
using Aklion.Crm.Models.Store;
using Aklion.Infrastructure.Storage.Repository;
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

            var list = result.Item2.Select(x => new StoreGetResponseModel
            {
                Id = x.Id,
                CreateUserId = x.CreateUserId,
                Name = x.Name,
                ApiKey = x.ApiKey,
                ApiSecret = x.ApiSecret,
                IsLocked = x.IsLocked,
                IsDeleted = x.IsDeleted,
                CreateDate = x.CreateDate,
                ModifyDate = x.ModifyDate
            }).ToList();

            return new BaseGetListResponseModel<StoreGetResponseModel>(list, result.Item1, model.Page, model.Size);
        }

        [HttpGet]
        public async Task<StoreGetResponseModel> Get(int id)
        {
            var result = await _repository.Get<Store>(id).ConfigureAwait(false);

            return new StoreGetResponseModel
            {
                Id = result.Id,
                CreateUserId = result.CreateUserId,
                Name = result.Name,
                ApiKey = result.ApiKey,
                ApiSecret = result.ApiSecret,
                IsLocked = result.IsLocked,
                IsDeleted = result.IsDeleted,
                CreateDate = result.CreateDate,
                ModifyDate = result.ModifyDate
            };
        }

        [HttpPost]
        public async Task Create(StoreCreateRequestRequestModel model)
        {
            var store = new Store
            {
                CreateUserId = model.CreateUserId,
                Name = model.Name,
                ApiKey = model.ApiKey,
                ApiSecret = model.ApiSecret,
                IsLocked = model.IsLocked,
                IsDeleted = model.IsDeleted,
                CreateDate = DateTime.Now
            };

            await _repository.Create(store).ConfigureAwait(false);
        }

        [HttpPost]
        public async Task Update(StoreUpdateRequestRequestModel model)
        {
            var store = await _repository.Get<Store>(model.Id).ConfigureAwait(false);

            store.CreateUserId = model.CreateUserId;
            store.Name = model.Name;
            store.ApiKey = model.ApiKey;
            store.ApiSecret = model.ApiSecret;
            store.IsLocked = model.IsLocked;
            store.IsDeleted = model.IsDeleted;
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