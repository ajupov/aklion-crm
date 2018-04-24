using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Crm.Attributes;
using Crm.Exceptions;
using Crm.Models;
using Crm.Models.User.ProductAttribute;
using Crm.Storages;
using Crm.Storages.Models;
using Infrastructure.Dao.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Crm.Controllers
{
    [CheckStore]
    [AjaxErrorHandle]
    [Route("ProductAttributes")]
    public class ProductAttributesController : BaseController
    {
        private readonly Storage _storage;

        public ProductAttributesController(Storage storage)
        {
            _storage = storage;
        }

        [HttpGet]
        [Route("GetList")]
        public async Task<PagingModel<ProductAttributeModel>> GetList(ProductAttributeParameterModel model)
        {
            var query = GetQuery(model);
            var list = await GetOrder(model, query).Skip(model.SkipCount).Take(model.TakeCount).ToListAsync().ConfigureAwait(false);
            var count = await query.CountAsync().ConfigureAwait(false);

            var result = list.Select(x => new ProductAttributeModel
            {
                Id = x.Id,
                Key = x.Key,
                Name = x.Name
            }).ToList();

            return new PagingModel<ProductAttributeModel>(result, count, model.Page, model.Size);
        }

        [HttpGet]
        [Route("GetAutocomplete")]
        public async Task<Dictionary<string, int>> GetAutocomplete(string pattern)
        {
            pattern = pattern.ToLower();

            return await _storage.ProductAttribute.Where(x => x.StoreId == UserContext.StoreId && x.Name.ToLower().StartsWith(pattern))
                .ToDictionaryAsync(k => k.Name, v => v.Id).ConfigureAwait(false);
        }

        [HttpGet]
        [Route("GetSelect")]
        public async Task<Dictionary<string, int>> GetSelect()
        {
            return await _storage.ProductAttribute.Where(x => x.StoreId == UserContext.StoreId).ToDictionaryAsync(k => k.Name, v => v.Id)
                .ConfigureAwait(false);
        }

        [HttpPost]
        [Route("Create")]
        public async Task Create(ProductAttributeModel model)
        {
            var productAttribute = new ProductAttribute
            {
                Key = model.Key.Trim(),
                Name = model.Name.Trim(),
                StoreId = UserContext.StoreId
            };

            await _storage.ProductAttribute.AddAsync(productAttribute).ConfigureAwait(false);
            await _storage.SaveChangesAsync().ConfigureAwait(false);
        }

        [HttpPost]
        [Route("Update")]
        public async Task Update(ProductAttributeModel model)
        {
            var productAttribute = await _storage.ProductAttribute.FirstOrDefaultAsync(x => x.Id == model.Id).ConfigureAwait(false);
            if (productAttribute.StoreId != UserContext.StoreId)
            {
                throw new NotAccessChangingException();
            }

            productAttribute.Key = model.Key.Trim();
            productAttribute.Name = model.Name.Trim();

            _storage.ProductAttribute.Update(productAttribute);
            await _storage.SaveChangesAsync().ConfigureAwait(false);
        }

        [HttpPost]
        [Route("Delete")]
        public async Task Delete(int id)
        {
            var productAttribute = await _storage.ProductAttribute.FirstOrDefaultAsync(x => x.Id == id).ConfigureAwait(false);
            if (productAttribute.StoreId != UserContext.StoreId)
            {
                throw new NotAccessChangingException();
            }

            _storage.ProductAttribute.Remove(productAttribute);
            await _storage.SaveChangesAsync().ConfigureAwait(false);
        }

        [NonAction]
        private IQueryable<ProductAttribute> GetQuery(ProductAttributeParameterModel model)
        {
            model.Key = !string.IsNullOrWhiteSpace(model.Key) ? model.Key.Trim().ToLower() : null;
            model.Name = !string.IsNullOrWhiteSpace(model.Name) ? model.Name.Trim().ToLower() : null;

            return _storage.ProductAttribute.Where(x =>
                x.StoreId == UserContext.StoreId
                && (string.IsNullOrEmpty(model.Key) || x.Key.Trim().ToLower().Contains(model.Key))
                && (string.IsNullOrEmpty(model.Name) || x.Name.Trim().ToLower().Contains(model.Name)));
        }

        [NonAction]
        private static IQueryable<ProductAttribute> GetOrder(BaseParameterModel model, IQueryable<ProductAttribute> query)
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