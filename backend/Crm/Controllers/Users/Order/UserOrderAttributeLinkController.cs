using System.Threading.Tasks;
using Crm.Attributes;
using Crm.Dao.OrderAttributeLink;
using Crm.Exceptions;
using Crm.Mappers.User.OrderAttributeLink;
using Crm.Models;
using Crm.Models.User.OrderAttributeLink;
using Microsoft.AspNetCore.Mvc;

namespace Crm.Controllers.Users.Order
{
    [AjaxErrorHandle]
    [Route("OrderAttributeLinks")]
    public class UserOrderAttributeLinkController : BaseController
    {
        private readonly IOrderAttributeLinkDao _dao;

        public UserOrderAttributeLinkController(IOrderAttributeLinkDao dao)
        {
            _dao = dao;
        }

        [HttpGet]
        public async Task<PagingModel<OrderAttributeLinkModel>> GetList(OrderAttributeLinkParameterModel model)
        {
            var result = await _dao.GetPagedListAsync(model.MapNew(UserContext.StoreId)).ConfigureAwait(false);
            return result.MapNew(model.Page, model.Size);
        }

        [HttpPost]
        public Task Create(OrderAttributeLinkModel model)
        {
            return _dao.CreateAsync(model.MapNew(UserContext.StoreId));
        }

        [HttpPost]
        public async Task Update(OrderAttributeLinkModel model)
        {
            var result = await _dao.GetAsync(model.Id).ConfigureAwait(false);
            if (result.StoreId != UserContext.StoreId)
            {
                throw new NotAccessChangingException();
            }

            await _dao.UpdateAsync(result.MapFrom(model)).ConfigureAwait(false);
        }

        [HttpPost]
        public async Task Delete(int id)
        {
            var result = await _dao.GetAsync(id).ConfigureAwait(false);
            if (result.StoreId != UserContext.StoreId)
            {
                throw new NotAccessChangingException();
            }

            result.IsDeleted = true;
            await _dao.UpdateAsync(result).ConfigureAwait(false);
        }
    }
}