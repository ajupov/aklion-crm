using System.Threading.Tasks;
using Crm.Attributes;
using Crm.Dao.OrderAttributeLink;
using Crm.Mappers.Administration.OrderAttributeLink;
using Crm.Models;
using Crm.Models.Administration.OrderAttributeLink;
using Microsoft.AspNetCore.Mvc;

namespace Crm.Controllers.Administration
{
    [AjaxErrorHandle]
    [Route("Administration/OrderAttributeLinks")]
    public class AdministrationOrderAttributeLinksController : BaseController
    {
        private readonly IOrderAttributeLinkDao _dao;

        public AdministrationOrderAttributeLinksController(IOrderAttributeLinkDao dao)
        {
            _dao = dao;
        }

        [HttpGet]
        [Route("GetList")]
        public async Task<PagingModel<OrderAttributeLinkModel>> GetList(OrderAttributeLinkParameterModel model)
        {
            var result = await _dao.GetPagedListAsync(model.MapNew()).ConfigureAwait(false);
            return result.MapNew(model.Page, model.Size);
        }

        [HttpPost]
        [Route("Create")]
        public Task Create(OrderAttributeLinkModel model)
        {
            return _dao.CreateAsync(model.MapNew());
        }

        [HttpPost]
        [Route("Update")]
        public async Task Update(OrderAttributeLinkModel model)
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