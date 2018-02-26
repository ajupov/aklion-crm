using System.Threading.Tasks;
using Aklion.Crm.Attributes;
using Aklion.Crm.Dao.UserAttributeLink;
using Aklion.Crm.Mappers.Administration.UserAttributeLink;
using Aklion.Crm.Models;
using Aklion.Crm.Models.Administration.UserAttributeLink;
using Microsoft.AspNetCore.Mvc;

namespace Aklion.Crm.Controllers.Administration.User
{
    [AjaxErrorHandle]
    [Route("Administration/UserAttributeLinks")]
    public class AdministrationUserAttributeLinkController : BaseController
    {
        private readonly IUserAttributeLinkDao _userAttributeLinkDao;

        public AdministrationUserAttributeLinkController(IUserAttributeLinkDao userAttributeLinkDao)
        {
            _userAttributeLinkDao = userAttributeLinkDao;
        }

        [HttpGet]
        public async Task<PagingModel<UserAttributeLinkModel>> GetList(UserAttributeLinkParameterModel model)
        {
            var result = await _userAttributeLinkDao.GetPagedListAsync(model.MapNew()).ConfigureAwait(false);
            return result.MapNew(model.Page, model.Size);
        }

        [HttpPost]
        public Task Create(UserAttributeLinkModel model)
        {
            return _userAttributeLinkDao.CreateAsync(model.MapNew());
        }

        [HttpPost]
        public async Task Update(UserAttributeLinkModel model)
        {
            var result = await _userAttributeLinkDao.GetAsync(model.Id).ConfigureAwait(false);
            await _userAttributeLinkDao.UpdateAsync(result.MapFrom(model)).ConfigureAwait(false);
        }

        [HttpPost]
        public Task Delete(int id)
        {
            return _userAttributeLinkDao.DeleteAsync(id);
        }
    }
}