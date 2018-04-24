using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Crm.Attributes;
using Crm.Exceptions;
using Crm.Models;
using Crm.Models.User.OrderStatus;
using Crm.Storages;
using Crm.Storages.Models;
using Infrastructure.Dao.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Crm.Controllers
{
    [CheckStore]
    [AjaxErrorHandle]
    [Route("OrderStatuses")]
    public class OrderStatusesController : BaseController
    {
        private readonly Storage _storage;

        public OrderStatusesController(Storage storage)
        {
            _storage = storage;
        }

        [HttpGet]
        [Route("GetList")]
        public async Task<PagingModel<OrderStatusModel>> GetList(OrderStatusParameterModel model)
        {
            var query = GetQuery(model);
            var list = await GetOrder(model, query).Skip(model.SkipCount).Take(model.TakeCount).ToListAsync().ConfigureAwait(false);
            var count = await query.CountAsync().ConfigureAwait(false);

            var result = list.Select(x => new OrderStatusModel
            {
                Id = x.Id,
                Name = x.Name
            }).ToList();

            return new PagingModel<OrderStatusModel>(result, count, model.Page, model.Size);
        }

        [HttpGet]
        [Route("GetSelect")]
        public async Task<Dictionary<string, int>> GetSelect()
        {
            return await _storage.OrderStatus.Where(x => x.StoreId == UserContext.StoreId).ToDictionaryAsync(k => k.Name, v => v.Id)
                .ConfigureAwait(false);
        }

        [HttpPost]
        [Route("Create")]
        public async Task Create(OrderStatusModel model)
        {
            var orderStatus = new OrderStatus
            {
                StoreId = UserContext.StoreId,
                Name = model.Name.Trim()
            };

            await _storage.OrderStatus.AddAsync(orderStatus).ConfigureAwait(false);
            await _storage.SaveChangesAsync().ConfigureAwait(false);
        }

        [HttpPost]
        [Route("Update")]
        public async Task Update(OrderStatusModel model)
        {
            var orderStatus = await _storage.OrderStatus.FirstOrDefaultAsync(x => x.Id == model.Id).ConfigureAwait(false);
            if (orderStatus.StoreId != UserContext.StoreId)
            {
                throw new NotAccessChangingException();
            }

            orderStatus.Name = model.Name.Trim();

            _storage.OrderStatus.Update(orderStatus);
            await _storage.SaveChangesAsync().ConfigureAwait(false);
        }

        [HttpPost]
        [Route("Delete")]
        public async Task Delete(int id)
        {
            var orderStatus = await _storage.OrderStatus.FirstOrDefaultAsync(x => x.Id == id).ConfigureAwait(false);
            if (orderStatus.StoreId != UserContext.StoreId)
            {
                throw new NotAccessChangingException();
            }

            _storage.OrderStatus.Remove(orderStatus);
            await _storage.SaveChangesAsync().ConfigureAwait(false);
        }

        [NonAction]
        private IQueryable<OrderStatus> GetQuery(OrderStatusParameterModel model)
        {
            model.Name = !string.IsNullOrWhiteSpace(model.Name) ? model.Name.Trim().ToLower() : null;

            return _storage.OrderStatus.Where(x =>
                x.StoreId == UserContext.StoreId && (string.IsNullOrEmpty(model.Name) || x.Name.Trim().ToLower().Contains(model.Name)));
        }

        [NonAction]
        private static IQueryable<OrderStatus> GetOrder(BaseParameterModel model, IQueryable<OrderStatus> query)
        {
            return model.IsDescSortingOrder
                ? query.OrderByDescending(x => x.Name)
                : query.OrderBy(x => x.Name);
        }
    }
}