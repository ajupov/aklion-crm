using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Crm.Attributes;
using Crm.Exceptions;
using Crm.Models;
using Crm.Models.User.ProductImageKeyLink;
using Crm.Storages;
using Crm.Storages.Models;
using Infrastructure.Dao.Models;
using Infrastructure.DateTime;
using Infrastructure.FileFormat;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Crm.Controllers
{
    [CheckStore]
    [AjaxErrorHandle]
    [Route("ProductImageKeyLinks")]
    public class ProductImageKeyLinksController : BaseController
    {
        private readonly Storage _storage;

        public ProductImageKeyLinksController(Storage storage)
        {
            _storage = storage;
        }

        [HttpGet]
        [Route("GetList")]
        public async Task<PagingModel<ProductImageKeyLinkModel>> GetList(ProductImageKeyLinkParameterModel model)
        {
            var query = GetQuery(model);
            var list = await GetOrder(model, query).Skip(model.SkipCount).Take(model.TakeCount).ToListAsync().ConfigureAwait(false);
            var count = await query.CountAsync().ConfigureAwait(false);

            var result = list.Select(x => new ProductImageKeyLinkModel
            {
                Id = x.Id,
                ProductId = x.ProductId,
                KeyId = x.KeyId,
                KeyName = x.Key.Name,
                Base64Value = x.Value,
                CreateDate = x.CreateDate.ToDateTimeString()
            }).ToList();

            return new PagingModel<ProductImageKeyLinkModel>(result, count, model.Page, model.Size);
        }

        [HttpPost]
        [Route("Create")]
        public async Task Create(ProductImageKeyLinkModel model)
        {
            var productImageKey = await GetProductImageKey(model).ConfigureAwait(false);

            var productImageKeyLink = new ProductImageKeyLink
            {
                StoreId = UserContext.StoreId,
                ProductId = model.ProductId,
                KeyId = productImageKey,
                CreateDate = DateTime.Now,
                ModifyDate = null
            };

            await _storage.ProductImageKeyLink.AddAsync(productImageKeyLink).ConfigureAwait(false);
            await _storage.SaveChangesAsync().ConfigureAwait(false);
        }

        [HttpPost]
        [Route("Update")]
        public async Task Update(ProductImageKeyLinkModel model)
        {
            var productImageKeyLink = await _storage.ProductImageKeyLink.FirstOrDefaultAsync(x => x.Id == model.Id).ConfigureAwait(false);
            if (productImageKeyLink.StoreId != UserContext.StoreId)
            {
                throw new NotAccessChangingException();
            }

            productImageKeyLink.KeyId = model.KeyId;
            productImageKeyLink.ModifyDate = DateTime.Now;

            _storage.ProductImageKeyLink.Update(productImageKeyLink);
            await _storage.SaveChangesAsync().ConfigureAwait(false);
        }

        [HttpPost]
        [Route("LoadImage")]
        public async Task LoadImage(ProductImageKeyLinkLoadImageModel model)
        {
            if (!model.ImageFile.FileName.IsImage())
            {
                return;
            }

            var productImageKeyLink = await _storage.ProductImageKeyLink.FirstOrDefaultAsync(x => x.Id == model.Id).ConfigureAwait(false);
            if (productImageKeyLink.StoreId != UserContext.StoreId)
            {
                throw new NotAccessChangingException();
            }

            var stream = new MemoryStream();
            model.ImageFile.CopyTo(stream);

            productImageKeyLink.Value = Convert.ToBase64String(stream.ToArray());
            productImageKeyLink.ModifyDate = DateTime.Now;

            _storage.ProductImageKeyLink.Update(productImageKeyLink);
            await _storage.SaveChangesAsync().ConfigureAwait(false);
        }

        [HttpPost]
        [Route("Delete")]
        public async Task Delete(int id)
        {
            var productImageKeyLink = await _storage.ProductImageKeyLink.FirstOrDefaultAsync(x => x.Id == id).ConfigureAwait(false);
            if (productImageKeyLink.StoreId != UserContext.StoreId)
            {
                throw new NotAccessChangingException();
            }

            _storage.ProductImageKeyLink.Remove(productImageKeyLink);
            await _storage.SaveChangesAsync().ConfigureAwait(false);
        }

        [NonAction]
        private IQueryable<ProductImageKeyLink> GetQuery(ProductImageKeyLinkParameterModel model)
        {
            model.KeyKey = !string.IsNullOrWhiteSpace(model.KeyKey) ? model.KeyKey.Trim().ToLower() : null;
            model.KeyName = !string.IsNullOrWhiteSpace(model.KeyName) ? model.KeyName.Trim().ToLower() : null;

            return _storage.ProductImageKeyLink.Include(x => x.Key).Where(x =>
                x.StoreId == UserContext.StoreId
                && x.ProductId == model.ProductId
                && (!model.KeyId.HasValue || x.KeyId == model.KeyId)
                && (string.IsNullOrEmpty(model.KeyKey) || x.Key.Name.Trim().ToLower().Contains(model.KeyKey))
                && (string.IsNullOrEmpty(model.KeyName) || x.Value.Trim().ToLower().Contains(model.KeyName))
                && (!model.MinCreateDate.ToNullableDate().HasValue || x.CreateDate > model.MinCreateDate.ToNullableDate())
                && (!model.MaxCreateDate.ToNullableDate().HasValue || x.CreateDate < model.MaxCreateDate.ToNullableDate()));
        }

        [NonAction]
        private static IQueryable<ProductImageKeyLink> GetOrder(BaseParameterModel model, IQueryable<ProductImageKeyLink> query)
        {
            switch (model.SortingColumn)
            {
                case "Key":
                    return model.IsDescSortingOrder
                        ? query.OrderByDescending(x => x.Key.Name)
                        : query.OrderBy(x => x.Key.Name);
                case "Value":
                    return model.IsDescSortingOrder
                        ? query.OrderByDescending(x => x.Value)
                        : query.OrderBy(x => x.Value);
                default:
                    return model.IsDescSortingOrder
                        ? query.OrderByDescending(x => x.CreateDate)
                        : query.OrderBy(x => x.CreateDate);
            }
        }

        [NonAction]
        private async Task<int> GetProductImageKey(ProductImageKeyLinkModel model)
        {
            var productImageKey = (model.KeyId > 0
                                      ? await _storage.ProductImageKey
                                          .FirstOrDefaultAsync(x => x.StoreId == UserContext.StoreId && x.Id == model.KeyId)
                                          .ConfigureAwait(false)
                                      : await _storage.ProductImageKey
                                          .FirstOrDefaultAsync(x =>
                                              x.StoreId == UserContext.StoreId && x.Name.Trim().ToLower() == model.KeyName.Trim().ToLower())
                                          .ConfigureAwait(false)) ?? new ProductImageKey
                                  {
                                      Key = model.KeyName.Trim().Replace(" ", "_"),
                                      Name = model.KeyName.Trim(),
                                      StoreId = UserContext.StoreId
                                  };

            if (productImageKey.Id > 0)
            {
                _storage.ProductImageKey.Update(productImageKey);
                await _storage.SaveChangesAsync().ConfigureAwait(false);
                return productImageKey.Id;
            }

            var savedProductImageKey = await _storage.ProductImageKey.AddAsync(productImageKey).ConfigureAwait(false);
            await _storage.SaveChangesAsync().ConfigureAwait(false);
            return savedProductImageKey.Entity.Id;
        }
    }
}