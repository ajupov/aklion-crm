using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Crm.Attributes;
using Crm.Exceptions;
using Crm.Models;
using Crm.Models.User.Product;
using Crm.Storages;
using Crm.Storages.Models;
using Infrastructure.Dao.Models;
using Infrastructure.DateTime;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Crm.Controllers
{
    [CheckStore]
    [AjaxErrorHandle]
    [Route("Products")]
    public class ProductsController : BaseController
    {
        private readonly Storage _storage;

        public ProductsController(Storage storage)
        {
            _storage = storage;
        }

        [HttpGet]
        [Route("")]
        [Route("Index")]
        public IActionResult Index()
        {
            return View("~/Views/Product/Index.cshtml");
        }

        [HttpGet]
        [Route("GetList")]
        public async Task<PagingModel<ProductModel>> GetList(ProductParameterModel model)
        {
            var query = GetQuery(model);
            var list = await GetOrder(model, query).Skip(model.SkipCount).Take(model.TakeCount).ToListAsync().ConfigureAwait(false);
            var count = await query.CountAsync().ConfigureAwait(false);

            var result = list.Select(x => new ProductModel
            {
                Id = x.Id,
                ParentProductId = x.ParentProductId,
                ParentProductName = x.ParentProduct?.Name,
                Name = x.Name,
                Price = x.Price,
                StatusId = x.StatusId,
                StatusName = x.Status?.Name,
                VendorCode = x.VendorCode,
                IsDeleted = x.IsDeleted,
                CreateDate = x.CreateDate.ToDateTimeString()
            }).ToList();

            return new PagingModel<ProductModel>(result, count, model.Page, model.Size);
        }

        [HttpGet]
        [Route("GetAutocomplete")]
        public async Task<Dictionary<string, int>> GetAutocomplete(string pattern)
        {
            pattern = pattern.ToLower();

            return await _storage.Product.Where(x => !x.IsDeleted && x.StoreId == UserContext.StoreId && x.Name.ToLower().Contains(pattern))
                .ToDictionaryAsync(k => k.Name, v => v.Id).ConfigureAwait(false);
        }

        [HttpPost]
        [Route("Create")]
        public async Task Create(ProductModel model)
        {
            var product = new Product
            {
                Name = model.Name,
                StoreId = UserContext.StoreId,
                IsDeleted = false,
                CreateDate = DateTime.Now,
                ModifyDate = null
            };

            await _storage.Product.AddAsync(product).ConfigureAwait(false);
            await _storage.SaveChangesAsync().ConfigureAwait(false);
        }

        [HttpPost]
        [Route("Update")]
        public async Task Update(ProductModel model)
        {
            var product = await _storage.Product.FirstOrDefaultAsync(x => x.Id == model.Id).ConfigureAwait(false);
            if (product.StoreId != UserContext.StoreId)
            {
                throw new NotAccessChangingException();
            }

            if (product.IsDeleted)
            {
                throw new ObjectIsDeletedException();
            }

            product.Name = model.Name;
            product.ModifyDate = DateTime.Now;

            _storage.Product.Update(product);
            await _storage.SaveChangesAsync().ConfigureAwait(false);
        }

        [HttpPost]
        [Route("Delete")]
        public async Task Delete(int id)
        {
            var product = await _storage.Product.FirstOrDefaultAsync(x => x.Id == id).ConfigureAwait(false);
            if (product.StoreId != UserContext.StoreId)
            {
                throw new NotAccessChangingException();
            }

            product.IsDeleted = !product.IsDeleted;
            await _storage.SaveChangesAsync().ConfigureAwait(false);
        }

        [NonAction]
        private IQueryable<Product> GetQuery(ProductParameterModel model)
        {
            model.Name = !string.IsNullOrWhiteSpace(model.Name) ? model.Name.Trim().ToLower() : null;
            model.VendorCode = !string.IsNullOrWhiteSpace(model.VendorCode) ? model.VendorCode.Trim().ToLower() : null;

            return _storage.Product
                .Include(x => x.ParentProduct)
                .Include(x => x.Status)
                .Include(x => x.AttributeLinks)
                .Where(x => x.StoreId == UserContext.StoreId
                            && (!model.Id.HasValue || x.Id == model.Id)
                            && (string.IsNullOrEmpty(model.Name) || x.Name.Trim().ToLower().Contains(model.Name))
                            && (!model.MinPrice.HasValue || x.Price > model.MinPrice)
                            && (!model.MaxPrice.HasValue || x.Price < model.MaxPrice)
                            && (!model.StatusId.HasValue || x.StatusId == model.StatusId)
                            && (!model.ParentProductId.HasValue || x.ParentProductId == model.ParentProductId)
                            && (string.IsNullOrEmpty(model.VendorCode) || x.VendorCode.Trim() == model.VendorCode)
                            && (!model.MinCreateDate.ToNullableDate().HasValue || x.CreateDate > model.MinCreateDate.ToNullableDate())
                            && (!model.MaxCreateDate.ToNullableDate().HasValue || x.CreateDate < model.MaxCreateDate.ToNullableDate())
                            && (!model.IsDeleted.HasValue || x.IsDeleted == model.IsDeleted)
                            && (x.AttributeLinks == null || model.Attributes == null || !model.Attributes.Any()
                                || model.Attributes.Where(a => !string.IsNullOrWhiteSpace(a.Value)).All(a =>
                                    x.AttributeLinks.Any(ca =>
                                        a.Key == ca.AttributeId && ca.Value.Trim().ToLower().Contains(a.Value.Trim().ToLower())))));
        }

        [NonAction]
        private static IQueryable<Product> GetOrder(BaseParameterModel model, IQueryable<Product> query)
        {
            switch (model.SortingColumn)
            {
                case "Name":
                    return model.IsDescSortingOrder
                        ? query.OrderBy(x => x.IsDeleted).ThenByDescending(x => x.Name)
                        : query.OrderBy(x => x.IsDeleted).ThenBy(x => x.Name);
                case "Price":
                    return model.IsDescSortingOrder
                        ? query.OrderBy(x => x.IsDeleted).ThenByDescending(x => x.Price)
                        : query.OrderBy(x => x.IsDeleted).ThenBy(x => x.Price);
                case "VendorCode":
                    return model.IsDescSortingOrder
                        ? query.OrderBy(x => x.IsDeleted).ThenByDescending(x => x.VendorCode)
                        : query.OrderBy(x => x.IsDeleted).ThenBy(x => x.VendorCode);
                default:
                    return model.IsDescSortingOrder
                        ? query.OrderBy(x => x.IsDeleted).ThenByDescending(x => x.CreateDate)
                        : query.OrderBy(x => x.IsDeleted).ThenBy(x => x.CreateDate);
            }
        }
    }
}