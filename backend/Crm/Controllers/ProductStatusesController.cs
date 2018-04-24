using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Crm.Attributes;
using Crm.Exceptions;
using Crm.Models;
using Crm.Models.User.ProductStatus;
using Crm.Storages;
using Crm.Storages.Models;
using Infrastructure.Dao.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Crm.Controllers
{
    [CheckStore]
    [AjaxErrorHandle]
    [Route("ProductStatuses")]
    public class ProductStatusesController : BaseController
    {
        private readonly Storage _storage;

        public ProductStatusesController(Storage storage)
        {
            _storage = storage;
        }

        [HttpGet]
        [Route("GetList")]
        public async Task<PagingModel<ProductStatusModel>> GetList(ProductStatusParameterModel model)
        {
            var query = GetQuery(model);
            var list = await GetOrder(model, query).Skip(model.SkipCount).Take(model.TakeCount).ToListAsync().ConfigureAwait(false);
            var count = await query.CountAsync().ConfigureAwait(false);

            var result = list.Select(x => new ProductStatusModel
            {
                Id = x.Id,
                Name = x.Name
            }).ToList();

            return new PagingModel<ProductStatusModel>(result, count, model.Page, model.Size);
        }

        [HttpGet]
        [Route("GetSelect")]
        public async Task<Dictionary<string, int>> GetSelect()
        {
            return await _storage.ProductStatus.Where(x => x.StoreId == UserContext.StoreId).ToDictionaryAsync(k => k.Name, v => v.Id)
                .ConfigureAwait(false);
        }

        [HttpPost]
        [Route("Create")]
        public async Task Create(ProductStatusModel model)
        {
            var productStatus = new ProductStatus
            {
                StoreId = UserContext.StoreId,
                Name = model.Name.Trim()
            };

            await _storage.ProductStatus.AddAsync(productStatus).ConfigureAwait(false);
            await _storage.SaveChangesAsync().ConfigureAwait(false);
        }

        [HttpPost]
        [Route("Update")]
        public async Task Update(ProductStatusModel model)
        {
            var productStatus = await _storage.ProductStatus.FirstOrDefaultAsync(x => x.Id == model.Id).ConfigureAwait(false);
            if (productStatus.StoreId != UserContext.StoreId)
            {
                throw new NotAccessChangingException();
            }

            productStatus.Name = model.Name.Trim();

            _storage.ProductStatus.Update(productStatus);
            await _storage.SaveChangesAsync().ConfigureAwait(false);
        }

        [HttpPost]
        [Route("Delete")]
        public async Task Delete(int id)
        {
            var productStatus = await _storage.ProductStatus.FirstOrDefaultAsync(x => x.Id == id).ConfigureAwait(false);
            if (productStatus.StoreId != UserContext.StoreId)
            {
                throw new NotAccessChangingException();
            }

            _storage.ProductStatus.Remove(productStatus);
            await _storage.SaveChangesAsync().ConfigureAwait(false);
        }

        [NonAction]
        private IQueryable<ProductStatus> GetQuery(ProductStatusParameterModel model)
        {
            model.Name = !string.IsNullOrWhiteSpace(model.Name) ? model.Name.Trim().ToLower() : null;

            return _storage.ProductStatus.Where(x =>
                x.StoreId == UserContext.StoreId && (string.IsNullOrEmpty(model.Name) || x.Name.Trim().ToLower().Contains(model.Name)));
        }

        [NonAction]
        private static IQueryable<ProductStatus> GetOrder(BaseParameterModel model, IQueryable<ProductStatus> query)
        {
            return model.IsDescSortingOrder
                ? query.OrderByDescending(x => x.Name)
                : query.OrderBy(x => x.Name);
        }
    }
}