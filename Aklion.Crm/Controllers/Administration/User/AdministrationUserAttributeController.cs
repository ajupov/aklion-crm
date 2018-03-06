using System.Collections.Generic;
using System.Threading.Tasks;
using Aklion.Crm.Attributes;
using Aklion.Crm.Dao.UserAttribute;
using Aklion.Crm.Mappers.Administration.UserAttribute;
using Aklion.Crm.Models;
using Aklion.Crm.Models.Administration.UserAttribute;
using Microsoft.AspNetCore.Mvc;

namespace Aklion.Crm.Controllers.Administration.User
{
    [AjaxErrorHandle]
    [Route("Administration/UserAttributes")]
    public class AdministrationUserAttributeController : BaseController
    {
        private readonly IUserAttributeDao _dao;

        public AdministrationUserAttributeController(IUserAttributeDao dao)
        {
            _dao = dao;
        }

        [HttpGet("GetList")]
        public async Task<PagingModel<UserAttributeModel>> GetList(UserAttributeParameterModel model)
        {
            var result = await _dao.GetPagedListAsync(model.MapNew()).ConfigureAwait(false);
            return result.MapNew(model.Page, model.Size);
        }

        [HttpGet("GetAutocomplete")]
        public Task<Dictionary<string, int>> GetAutocomplete(string pattern, int storeId)
        {
            return _dao.GetAutocompleteAsync(pattern.MapNew(storeId));
        }

        [HttpPost("Create")]
        public Task Create(UserAttributeModel model)
        {
            return _dao.CreateAsync(model.MapNew());
        }

        [HttpPost("Update")]
        public async Task Update(UserAttributeModel model)
        {
            var result = await _dao.GetAsync(model.Id).ConfigureAwait(false);
            await _dao.UpdateAsync(result.MapFrom(model)).ConfigureAwait(false);
        }

        [HttpPost("Delete")]
        public Task Delete(int id)
        {
            return _dao.DeleteAsync(id);
        }
    }
}