using System.Collections.Generic;
using System.Threading.Tasks;
using Aklion.Crm.Attributes;
using Aklion.Crm.Dao.UserAttribute;
using Aklion.Crm.Mappers.Administration.UserAttribute;
using Aklion.Crm.Models;
using Aklion.Crm.Models.Administration.UserAttribute;

namespace Aklion.Crm.Controllers.Administration
{
    [AjaxErrorHandle]
    public class UserAttributesController : BaseController
    {
        private readonly IUserAttributeDao _dao;

        public UserAttributesController(IUserAttributeDao dao)
        {
            _dao = dao;
        }

        public async Task<PagingModel<UserAttributeModel>> GetList(UserAttributeParameterModel model)
        {
            var result = await _dao.GetPagedListAsync(model.MapNew()).ConfigureAwait(false);
            return result.MapNew(model.Page, model.Size);
        }

        public Task<Dictionary<string, int>> GetAutocomplete(string pattern, int storeId)
        {
            return _dao.GetAutocompleteAsync(pattern.MapNew(storeId));
        }

        public Task Create(UserAttributeModel model)
        {
            return _dao.CreateAsync(model.MapNew());
        }

        public async Task Update(UserAttributeModel model)
        {
            var result = await _dao.GetAsync(model.Id).ConfigureAwait(false);
            await _dao.UpdateAsync(result.MapFrom(model)).ConfigureAwait(false);
        }

        public Task Delete(int id)
        {
            return _dao.DeleteAsync(id);
        }
    }
}