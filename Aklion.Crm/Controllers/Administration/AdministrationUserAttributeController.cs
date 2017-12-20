using System.Collections.Generic;
using System.Threading.Tasks;
using Aklion.Crm.Attributes;
using Aklion.Crm.Dao.UserAttribute;
using Aklion.Crm.Mappers.Administration.UserAttribute;
using Aklion.Crm.Models;
using Aklion.Crm.Models.Administration.UserAttribute;
using Microsoft.AspNetCore.Mvc;

namespace Aklion.Crm.Controllers.Administration
{
    [Route("Administration/UserAttributes")]
    public class AdministrationUserAttributeController : BaseController
    {
        private readonly IUserAttributeDao _clientAttributeDao;

        public AdministrationUserAttributeController(IUserAttributeDao clientAttributeDao)
        {
            _clientAttributeDao = clientAttributeDao;
        }

        [HttpGet]
        [Route("GetList")]
        public async Task<PagingModel<UserAttributeModel>> GetList(UserAttributeParameterModel model)
        {
            var result = await _clientAttributeDao.GetPagedListAsync(model.MapNew()).ConfigureAwait(false);

            return result.MapNew(model.Page, model.Size);
        }

        [HttpGet]
        [Route("GetForAutocompleteByNamePattern")]
        public Task<Dictionary<string, int>> GetForAutocompleteByNamePattern(string pattern, int storeId = 0)
        {
            return _clientAttributeDao.GetForAutocompleteAsync(pattern.MapNew(storeId));
        }

        [HttpPost]
        [Route("Create")]
        [AjaxErrorHandle]
        public Task Create(UserAttributeModel model)
        {
            return _clientAttributeDao.CreateAsync(model.MapNew());
        }

        [HttpPost]
        [Route("Update")]
        [AjaxErrorHandle]
        public async Task Update(UserAttributeModel model)
        {
            var result = await _clientAttributeDao.GetAsync(model.Id).ConfigureAwait(false);

            await _clientAttributeDao.UpdateAsync(result.MapFrom(model)).ConfigureAwait(false);
        }

        [HttpPost]
        [Route("Delete")]
        [AjaxErrorHandle]
        public Task Delete(int id)
        {
            return _clientAttributeDao.DeleteAsync(id);
        }
    }
}