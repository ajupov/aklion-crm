using System.Threading.Tasks;
using Aklion.Crm.Attributes;
using Aklion.Crm.Dao.OrderAttributeLink;
using Aklion.Crm.Mappers.Administration.OrderAttributeLink;
using Aklion.Crm.Models;
using Aklion.Crm.Models.Administration.OrderAttributeLink;
using Microsoft.AspNetCore.Mvc;

namespace Aklion.Crm.Controllers.Administration.Order
{
    [AjaxErrorHandle]
    [Route("Administration/OrderAttributeLinks")]
    public class AdministrationOrderAttributeLinkController : BaseController
    {
        private readonly IOrderAttributeLinkDao _dao;

        public AdministrationOrderAttributeLinkController(IOrderAttributeLinkDao dao)
        {
            _dao = dao;
        }

        [HttpGet]
        public async Task<PagingModel<OrderAttributeLinkModel>> GetList(OrderAttributeLinkParameterModel model)
        {
            var result = await _dao.GetPagedListAsync(model.MapNew()).ConfigureAwait(false);
            return result.MapNew(model.Page, model.Size);
        }

        [HttpPost]
        public Task Create(OrderAttributeLinkModel model)
        {
            return _dao.CreateAsync(model.MapNew());
        }

        [HttpPost]
        public async Task Update(OrderAttributeLinkModel model)
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