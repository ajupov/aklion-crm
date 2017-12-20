using System.Threading.Tasks;
using Aklion.Crm.Attributes;
using Aklion.Crm.Dao.OrderAttributeLink;
using Aklion.Crm.Mappers.Administration.OrderAttributeLink;
using Aklion.Crm.Models;
using Aklion.Crm.Models.Administration.OrderAttributeLink;
using Microsoft.AspNetCore.Mvc;

namespace Aklion.Crm.Controllers.Administration
{
    [Route("Administration/OrderAttributeLinks")]
    public class AdministrationOrderAttributeLinkController : BaseController
    {
        private readonly IOrderAttributeLinkDao _clientAttributeLinkDao;

        public AdministrationOrderAttributeLinkController(IOrderAttributeLinkDao clientAttributeLinkDao)
        {
            _clientAttributeLinkDao = clientAttributeLinkDao;
        }

        [HttpGet]
        [Route("GetList")]
        public async Task<PagingModel<OrderAttributeLinkModel>> GetList(OrderAttributeLinkParameterModel model)
        {
            var result = await _clientAttributeLinkDao.GetPagedListAsync(model.MapNew()).ConfigureAwait(false);

            return result.MapNew(model.Page, model.Size);
        }

        [HttpPost]
        [Route("Create")]
        [AjaxErrorHandle]
        public Task Create(OrderAttributeLinkModel model)
        {
            return _clientAttributeLinkDao.CreateAsync(model.MapNew());
        }

        [HttpPost]
        [Route("Update")]
        [AjaxErrorHandle]
        public async Task Update(OrderAttributeLinkModel model)
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