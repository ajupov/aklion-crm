using System.Threading.Tasks;
using Aklion.Crm.Attributes;
using Aklion.Crm.Dao.ClientAttributeLink;
using Aklion.Crm.Mappers.Administration.ClientAttributeLink;
using Aklion.Crm.Models;
using Aklion.Crm.Models.Administration.ClientAttributeLink;
using Microsoft.AspNetCore.Mvc;

namespace Aklion.Crm.Controllers.Administration
{
    [Route("Administration/ClientAttributeLinks")]
    public class AdministrationClientAttributeLinkController : BaseController
    {
        private readonly IClientAttributeLinkDao _clientAttributeLinkDao;

        public AdministrationClientAttributeLinkController(IClientAttributeLinkDao clientAttributeLinkDao)
        {
            _clientAttributeLinkDao = clientAttributeLinkDao;
        }

        [HttpGet]
        [Route("GetList")]
        public async Task<PagingModel<ClientAttributeLinkModel>> GetList(ClientAttributeLinkParameterModel model)
        {
            var result = await _clientAttributeLinkDao.GetPagedListAsync(model.MapNew()).ConfigureAwait(false);

            return result.MapNew(model.Page, model.Size);
        }

        [HttpPost]
        [Route("Create")]
        [AjaxErrorHandle]
        public Task Create(ClientAttributeLinkModel model)
        {
            return _clientAttributeLinkDao.CreateAsync(model.MapNew());
        }

        [HttpPost]
        [Route("Update")]
        [AjaxErrorHandle]
        public async Task Update(ClientAttributeLinkModel model)
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