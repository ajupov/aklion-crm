using System.Threading.Tasks;
using Aklion.Crm.Attributes;
using Aklion.Crm.Dao.UserAttributeLink;
using Aklion.Crm.Mappers.Administration.UserAttributeLink;
using Aklion.Crm.Models;
using Aklion.Crm.Models.Administration.UserAttributeLink;

namespace Aklion.Crm.Controllers.Administration
{
    [AjaxErrorHandle]
    public class UserAttributeLinksController : BaseController
    {
        private readonly IUserAttributeLinkDao _dao;

        public UserAttributeLinksController(IUserAttributeLinkDao dao)
        {
            _dao = dao;
        }

        public async Task<PagingModel<UserAttributeLinkModel>> GetList(UserAttributeLinkParameterModel model)
        {
            var result = await _dao.GetPagedListAsync(model.MapNew()).ConfigureAwait(false);
            return result.MapNew(model.Page, model.Size);
        }

        public Task Create(UserAttributeLinkModel model)
        {
            return _dao.CreateAsync(model.MapNew());
        }

        public async Task Update(UserAttributeLinkModel model)
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