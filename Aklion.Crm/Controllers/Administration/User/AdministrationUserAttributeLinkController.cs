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
        private readonly IUserAttributeLinkDao _dao;

        public AdministrationUserAttributeLinkController(IUserAttributeLinkDao dao)
        {
            _dao = dao;
        }

        [HttpGet("GetList")]
        public async Task<PagingModel<UserAttributeLinkModel>> GetList(UserAttributeLinkParameterModel model)
        {
            var result = await _dao.GetPagedListAsync(model.MapNew()).ConfigureAwait(false);
            return result.MapNew(model.Page, model.Size);
        }

        [HttpPost("Create")]
        public Task Create(UserAttributeLinkModel model)
        {
            return _dao.CreateAsync(model.MapNew());
        }

        [HttpPost("Update")]
        public async Task Update(UserAttributeLinkModel model)
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