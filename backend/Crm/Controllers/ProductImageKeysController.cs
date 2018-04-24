using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Crm.Attributes;
using Crm.Exceptions;
using Crm.Models;
using Crm.Models.User.ProductImageKey;
using Crm.Storages;
using Crm.Storages.Models;
using Infrastructure.Dao.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Crm.Controllers
{
    [CheckStore]
    [AjaxErrorHandle]
    [Route("ProductImageKeys")]
    public class ProductImageKeysController : BaseController
    {
        private readonly Storage _storage;

        public ProductImageKeysController(Storage storage)
        {
            _storage = storage;
        }

        [HttpGet]
        [Route("GetList")]
        public async Task<PagingModel<ProductImageKeyModel>> GetList(ProductImageKeyParameterModel model)
        {
            var query = GetQuery(model);
            var list = await GetOrder(model, query).Skip(model.SkipCount).Take(model.TakeCount).ToListAsync().ConfigureAwait(false);
            var count = await query.CountAsync().ConfigureAwait(false);

            var result = list.Select(x => new ProductImageKeyModel
            {
                Id = x.Id,
                Key = x.Key,
                Name = x.Name
            }).ToList();

            return new PagingModel<ProductImageKeyModel>(result, count, model.Page, model.Size);
        }

        [HttpGet]
        [Route("GetSelect")]
        public async Task<Dictionary<string, int>> GetSelect()
        {
            return await _storage.ProductImageKey.Where(x => x.StoreId == UserContext.StoreId).ToDictionaryAsync(k => k.Name, v => v.Id)
                .ConfigureAwait(false);
        }

        [HttpPost]
        [Route("Create")]
        public async Task Create(ProductImageKeyModel model)
        {
            var clientAttribute = new ProductImageKey
            {
                Key = model.Key.Trim(),
                Name = model.Name.Trim(),
                StoreId = UserContext.StoreId
            };

            await _storage.ProductImageKey.AddAsync(clientAttribute).ConfigureAwait(false);
            await _storage.SaveChangesAsync().ConfigureAwait(false);
        }

        [HttpPost]
        [Route("Update")]
        public async Task Update(ProductImageKeyModel model)
        {
            var productImageKey = await _storage.ProductImageKey.FirstOrDefaultAsync(x => x.Id == model.Id).ConfigureAwait(false);
            if (productImageKey.StoreId != UserContext.StoreId)
            {
                throw new NotAccessChangingException();
            }

            productImageKey.Key = model.Key.Trim();
            productImageKey.Name = model.Name.Trim();

            _storage.ProductImageKey.Update(productImageKey);
            await _storage.SaveChangesAsync().ConfigureAwait(false);
        }

        [HttpPost]
        [Route("Delete")]
        public async Task Delete(int id)
        {
            var productImageKey = await _storage.ProductImageKey.FirstOrDefaultAsync(x => x.Id == id).ConfigureAwait(false);
            if (productImageKey.StoreId != UserContext.StoreId)
            {
                throw new NotAccessChangingException();
            }

            _storage.ProductImageKey.Remove(productImageKey);
            await _storage.SaveChangesAsync().ConfigureAwait(false);
        }

        [NonAction]
        private IQueryable<ProductImageKey> GetQuery(ProductImageKeyParameterModel model)
        {
            model.Name = !string.IsNullOrWhiteSpace(model.Name) ? model.Name.Trim().ToLower() : null;
            model.Key = !string.IsNullOrWhiteSpace(model.Key) ? model.Key.Trim().ToLower() : null;

            return _storage.ProductImageKey.Where(x =>
                x.StoreId == UserContext.StoreId && (string.IsNullOrEmpty(model.Name) || x.Name.Trim().ToLower().Contains(model.Name)) &&
                (string.IsNullOrEmpty(model.Key) || x.Name.Trim().ToLower().Contains(model.Key)));
        }

        [NonAction]
        private static IQueryable<ProductImageKey> GetOrder(BaseParameterModel model, IQueryable<ProductImageKey> query)
        {
            switch (model.SortingColumn)
            {
                case "Key":
                    return model.IsDescSortingOrder
                        ? query.OrderByDescending(x => x.Key)
                        : query.OrderBy(x => x.Key);
                default:
                    return model.IsDescSortingOrder
                        ? query.OrderByDescending(x => x.Name)
                        : query.OrderBy(x => x.Name);
            }
        }
    }
}