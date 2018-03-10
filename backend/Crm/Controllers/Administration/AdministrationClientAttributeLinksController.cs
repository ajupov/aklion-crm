using System.Threading.Tasks;
using Crm.Attributes;
using Crm.Dao.ClientAttributeLink;
using Crm.Mappers.Administration.ClientAttributeLink;
using Crm.Models;
using Crm.Models.Administration.ClientAttributeLink;
using Microsoft.AspNetCore.Mvc;

namespace Crm.Controllers.Administration
{
    [AjaxErrorHandle]
    [Route("Administration/ClientAttributeLinks")]
    public class AdministrationClientAttributeLinksController : BaseController
    {
        private readonly IClientAttributeLinkDao _dao;

        public AdministrationClientAttributeLinksController(IClientAttributeLinkDao dao)
        {
            _dao = dao;
        }

        [HttpGet]
        [Route("GetList")]
        public async Task<PagingModel<ClientAttributeLinkModel>> GetList(ClientAttributeLinkParameterModel model)
        {
            var result = await _dao.GetPagedListAsync(model.MapNew()).ConfigureAwait(false);
            return result.MapNew(model.Page, model.Size);
        }

        [HttpPost]
        [Route("Create")]
        public Task Create(ClientAttributeLinkModel model)
        {
            return _dao.CreateAsync(model.MapNew());
        }

        [HttpPost]
        [Route("Update")]
        public async Task Update(ClientAttributeLinkModel model)
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