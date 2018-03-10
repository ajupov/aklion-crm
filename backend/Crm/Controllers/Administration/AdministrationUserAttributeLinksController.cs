using System.Threading.Tasks;
using Crm.Attributes;
using Crm.Dao.UserAttributeLink;
using Crm.Mappers.Administration.UserAttributeLink;
using Crm.Models;
using Crm.Models.Administration.UserAttributeLink;
using Microsoft.AspNetCore.Mvc;

namespace Crm.Controllers.Administration
{
    [AjaxErrorHandle]
    [Route("Administration/UserAttributeLinks")]
    public class AdministrationUserAttributeLinksController : BaseController
    {
        private readonly IUserAttributeLinkDao _dao;

        public AdministrationUserAttributeLinksController(IUserAttributeLinkDao dao)
        {
            _dao = dao;
        }

        [HttpGet]
        [Route("GetList")]
        public async Task<PagingModel<UserAttributeLinkModel>> GetList(UserAttributeLinkParameterModel model)
        {
            var result = await _dao.GetPagedListAsync(model.MapNew()).ConfigureAwait(false);
            return result.MapNew(model.Page, model.Size);
        }

        [HttpPost]
        [Route("Create")]
        public Task Create(UserAttributeLinkModel model)
        {
            return _dao.CreateAsync(model.MapNew());
        }

        [HttpPost]
        [Route("Update")]
        public async Task Update(UserAttributeLinkModel model)
        {
            var result = await _dao.GetAsync(model.Id).ConfigureAwait(false);
            await _dao.UpdateAsync(result.MapFrom(model)).ConfigureAwait(false);
        }

        [HttpPost]
        [Route("Delete")]
        public Task Delete(int id)
        {
            return _dao.DeleteAsync(id);
        }
    }
}