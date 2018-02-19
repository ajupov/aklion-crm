using System.Threading.Tasks;
using Aklion.Crm.Attributes;
using Aklion.Crm.Dao.ClientAttributeLink;
using Aklion.Crm.Mappers.Administration.ClientAttributeLink;
using Aklion.Crm.Models;
using Aklion.Crm.Models.Administration.ClientAttributeLink;
using Microsoft.AspNetCore.Mvc;

namespace Aklion.Crm.Controllers.Administration.Clients
{
    [AjaxErrorHandle]
    [Route("Administration/ClientAttributeLinks")]
    public class AdministrationClientAttributeLinkController : BaseController
    {
        private readonly IClientAttributeLinkDao _dao;

        public AdministrationClientAttributeLinkController(IClientAttributeLinkDao dao)
        {
            _dao = dao;
        }

        [HttpGet]
        public async Task<PagingModel<ClientAttributeLinkModel>> GetList(ClientAttributeLinkParameterModel model)
        {
            var result = await _dao.GetPagedListAsync(model.MapNew()).ConfigureAwait(false);
            return result.MapNew(model.Page, model.Size);
        }

        [HttpPost]
        public Task Create(ClientAttributeLinkModel model)
        {
            return _dao.CreateAsync(model.MapNew());
        }

        [HttpPost]
        public async Task Update(ClientAttributeLinkModel model)
        {
            var result = await _dao.GetAsync(model.Id).ConfigureAwait(false);
            await _dao.UpdateAsync(result.MapFrom(model)).ConfigureAwait(false);
        }

        [HttpPost]
        public Task Delete(int id)
        {
            return _dao.DeleteAsync(id);
        }
    }
}