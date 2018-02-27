using System.Collections.Generic;
using System.Threading.Tasks;
using Aklion.Crm.Attributes;
using Aklion.Crm.Business.Store;
using Aklion.Crm.Dao.Store;
using Aklion.Crm.Mappers.User.Store;
using Aklion.Crm.Models;
using Aklion.Crm.Models.User.Store;
using Microsoft.AspNetCore.Mvc;

namespace Aklion.Crm.Controllers.Users.Store
{
    [AjaxErrorHandle]
    [Route("Stores")]
    public class UserStoreController : BaseController
    {
        private readonly IStoreService _service;
        private readonly IStoreDao _dao;

        public UserStoreController(IStoreService service, IStoreDao dao)
        {
            _service = service;
            _dao = dao;
        }

        [HttpGet]
        [Route("")]
        public IActionResult Index()
        {
            return View("~/Views/User/Store/Index.cshtml");
        }

        [HttpGet]
        public async Task<PagingModel<StoreModel>> GetList(StoreParameterModel model)
        {
            var result = await _dao.GetPagedListAsync(model.MapNew(UserContext.UserId)).ConfigureAwait(false);
            return result.MapNew(model.Page, model.Size);
        }

        [HttpGet]
        public Task<Dictionary<string, int>> GetAutocomplete(string pattern)
        {
            return _dao.GetAutocompleteAsync(pattern.MapNew(UserContext.UserId));
        }

        [HttpPost]
        public Task Create(StoreModel model)
        {
            return _dao.CreateAsync(model.MapNew());
        }

        [HttpPost]
        public async Task Update(StoreModel model)
        {
            var result = await _dao.GetAsync(model.Id).ConfigureAwait(false);
            await _dao.UpdateAsync(result.MapFrom(model)).ConfigureAwait(false);
        }

        [HttpPost]
        public async Task Delete(int id)
        {
            var result = await _dao.GetAsync(id).ConfigureAwait(false);
            result.IsDeleted = true;
            await _dao.UpdateAsync(result).ConfigureAwait(false);
        }

        [HttpPost]
        public Task<string> GenerateApiSecret(int id)
        {
            return _service.GenerateApiSecretAsync(UserContext.UserId, UserContext.StoreId, id);
        }
    }
}