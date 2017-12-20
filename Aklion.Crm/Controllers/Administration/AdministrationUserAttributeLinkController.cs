using System.Threading.Tasks;
using Aklion.Crm.Attributes;
using Aklion.Crm.Dao.UserAttributeLink;
using Aklion.Crm.Mappers.Administration.UserAttributeLink;
using Aklion.Crm.Models;
using Aklion.Crm.Models.Administration.UserAttributeLink;
using Microsoft.AspNetCore.Mvc;

namespace Aklion.Crm.Controllers.Administration
{
    [Route("Administration/UserAttributeLinks")]
    public class AdministrationUserAttributeLinkController : BaseController
    {
        private readonly IUserAttributeLinkDao _clientAttributeLinkDao;

        public AdministrationUserAttributeLinkController(IUserAttributeLinkDao clientAttributeLinkDao)
        {
            _clientAttributeLinkDao = clientAttributeLinkDao;
        }

        [HttpGet]
        [Route("GetList")]
        public async Task<PagingModel<UserAttributeLinkModel>> GetList(UserAttributeLinkParameterModel model)
        {
            var result = await _clientAttributeLinkDao.GetPagedListAsync(model.MapNew()).ConfigureAwait(false);

            return result.MapNew(model.Page, model.Size);
        }

        [HttpPost]
        [Route("Create")]
        [AjaxErrorHandle]
        public Task Create(UserAttributeLinkModel model)
        {
            return _clientAttributeLinkDao.CreateAsync(model.MapNew());
        }

        [HttpPost]
        [Route("Update")]
        [AjaxErrorHandle]
        public async Task Update(UserAttributeLinkModel model)
        {
            var result = await _clientAttributeLinkDao.GetAsync(model.Id).ConfigureAwait(false);

            await _clientAttributeLinkDao.UpdateAsync(result.MapFrom(model)).ConfigureAwait(false);
        }

        [HttpPost]
        [Route("Delete")]
        [AjaxErrorHandle]
        public Task Delete(int id)
        {
            return _clientAttributeLinkDao.DeleteAsync(id);
        }
    }
}