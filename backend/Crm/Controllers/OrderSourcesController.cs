using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Crm.Attributes;
using Crm.Exceptions;
using Crm.Models;
using Crm.Models.User.OrderSource;
using Crm.Storages;
using Crm.Storages.Models;
using Infrastructure.Dao.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Crm.Controllers
{
    [CheckStore]
    [AjaxErrorHandle]
    [Route("OrderSources")]
    public class OrderSourcesController : BaseController
    {
        private readonly Storage _storage;

        public OrderSourcesController(Storage storage)
        {
            _storage = storage;
        }

        [HttpGet]
        [Route("GetList")]
        public async Task<PagingModel<OrderSourceModel>> GetList(OrderSourceParameterModel model)
        {
            var query = GetQuery(model);
            var list = await GetOrder(model, query).Skip(model.SkipCount).Take(model.TakeCount).ToListAsync().ConfigureAwait(false);
            var count = await query.CountAsync().ConfigureAwait(false);

            var result = list.Select(x => new OrderSourceModel
            {
                Id = x.Id,
                Name = x.Name
            }).ToList();

            return new PagingModel<OrderSourceModel>(result, count, model.Page, model.Size);
        }

        [HttpGet]
        [Route("GetSelect")]
        public async Task<Dictionary<string, int>> GetSelect()
        {
            return await _storage.OrderSource.Where(x => x.StoreId == UserContext.StoreId).ToDictionaryAsync(k => k.Name, v => v.Id)
                .ConfigureAwait(false);
        }

        [HttpPost]
        [Route("Create")]
        public async Task Create(OrderSourceModel model)
        {
            var orderSource = new OrderSource
            {
                StoreId = UserContext.StoreId,
                Name = model.Name.Trim()
            };

            await _storage.OrderSource.AddAsync(orderSource).ConfigureAwait(false);
            await _storage.SaveChangesAsync().ConfigureAwait(false);
        }

        [HttpPost]
        [Route("Update")]
        public async Task Update(OrderSourceModel model)
        {
            var orderSource = await _storage.OrderSource.FirstOrDefaultAsync(x => x.Id == model.Id).ConfigureAwait(false);
            if (orderSource.StoreId != UserContext.StoreId)
            {
                throw new NotAccessChangingException();
            }

            orderSource.Name = model.Name.Trim();

            _storage.OrderSource.Update(orderSource);
            await _storage.SaveChangesAsync().ConfigureAwait(false);
        }

        [HttpPost]
        [Route("Delete")]
        public async Task Delete(int id)
        {
            var orderSource = await _storage.OrderSource.FirstOrDefaultAsync(x => x.Id == id).ConfigureAwait(false);
            if (orderSource.StoreId != UserContext.StoreId)
            {
                throw new NotAccessChangingException();
            }

            _storage.OrderSource.Remove(orderSource);
            await _storage.SaveChangesAsync().ConfigureAwait(false);
        }

        [NonAction]
        private IQueryable<OrderSource> GetQuery(OrderSourceParameterModel model)
        {
            model.Name = !string.IsNullOrWhiteSpace(model.Name) ? model.Name.Trim().ToLower() : null;

            return _storage.OrderSource.Where(x =>
                x.StoreId == UserContext.StoreId && (string.IsNullOrEmpty(model.Name) || x.Name.Trim().ToLower().Contains(model.Name)));
        }

        [NonAction]
        private static IQueryable<OrderSource> GetOrder(BaseParameterModel model, IQueryable<OrderSource> query)
        {
            return model.IsDescSortingOrder
                ? query.OrderByDescending(x => x.Name)
                : query.OrderBy(x => x.Name);
        }
    }
}