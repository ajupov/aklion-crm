using System;
using System.Linq;
using System.Threading.Tasks;
using Crm.Attributes;
using Crm.Exceptions;
using Crm.Models;
using Crm.Models.User.OrderAttributeLink;
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
    [Route("OrderAttributeLinks")]
    public class OrderAttributeLinksController : BaseController
    {
        private readonly Storage _storage;

        public OrderAttributeLinksController(Storage storage)
        {
            _storage = storage;
        }

        [HttpGet]
        [Route("GetList")]
        public async Task<PagingModel<OrderAttributeLinkModel>> GetList(OrderAttributeLinkParameterModel model)
        {
            var query = GetQuery(model);
            var list = await GetOrder(model, query).Skip(model.SkipCount).Take(model.TakeCount).ToListAsync().ConfigureAwait(false);
            var count = await query.CountAsync().ConfigureAwait(false);

            var result = list.Select(x => new OrderAttributeLinkModel
            {
                Id = x.Id,
                OrderId = x.OrderId,
                AttributeId = x.AttributeId,
                AttributeName = x.Attribute.Name,
                Value = x.Value,
                CreateDate = x.CreateDate.ToDateTimeString()
            }).ToList();

            return new PagingModel<OrderAttributeLinkModel>(result, count, model.Page, model.Size);
        }

        [HttpPost]
        [Route("Create")]
        public async Task Create(OrderAttributeLinkModel model)
        {
            var attributeId = await GetAttribute(model).ConfigureAwait(false);

            var orderAttributeLink = new OrderAttributeLink
            {
                StoreId = UserContext.StoreId,
                OrderId = model.OrderId,
                AttributeId = attributeId,
                Value = model.Value,
                CreateDate = DateTime.Now,
                ModifyDate = null
            };

            await _storage.OrderAttributeLink.AddAsync(orderAttributeLink).ConfigureAwait(false);
            await _storage.SaveChangesAsync().ConfigureAwait(false);
        }

        [HttpPost]
        [Route("Update")]
        public async Task Update(OrderAttributeLinkModel model)
        {
            var orderAttributeLink = await _storage.OrderAttributeLink.FirstOrDefaultAsync(x => x.Id == model.Id).ConfigureAwait(false);
            if (orderAttributeLink.StoreId != UserContext.StoreId)
            {
                throw new NotAccessChangingException();
            }

            orderAttributeLink.AttributeId = model.AttributeId;
            orderAttributeLink.Value = model.Value;
            orderAttributeLink.ModifyDate = DateTime.Now;

            _storage.OrderAttributeLink.Update(orderAttributeLink);
            await _storage.SaveChangesAsync().ConfigureAwait(false);
        }

        [HttpPost]
        [Route("Delete")]
        public async Task Delete(int id)
        {
            var orderAttributeLink = await _storage.OrderAttributeLink.FirstOrDefaultAsync(x => x.Id == id).ConfigureAwait(false);
            if (orderAttributeLink.StoreId != UserContext.StoreId)
            {
                throw new NotAccessChangingException();
            }

            _storage.OrderAttributeLink.Remove(orderAttributeLink);
            await _storage.SaveChangesAsync().ConfigureAwait(false);
        }

        [NonAction]
        private IQueryable<OrderAttributeLink> GetQuery(OrderAttributeLinkParameterModel model)
        {
            model.AttributeName = !string.IsNullOrWhiteSpace(model.AttributeName) ? model.AttributeName.Trim().ToLower() : null;
            model.Value = !string.IsNullOrWhiteSpace(model.Value) ? model.Value.Trim().ToLower() : null;

            return _storage.OrderAttributeLink.Include(x => x.Attribute).Where(x =>
                x.StoreId == UserContext.StoreId
                && x.OrderId == model.OrderId
                && (!model.AttributeId.HasValue || x.AttributeId == model.AttributeId)
                && (string.IsNullOrEmpty(model.AttributeName) || x.Attribute.Name.Trim().ToLower().Contains(model.AttributeName))
                && (string.IsNullOrEmpty(model.Value) || x.Value.Trim().ToLower().Contains(model.Value))
                && (!model.MinCreateDate.ToNullableDate().HasValue || x.CreateDate > model.MinCreateDate.ToNullableDate())
                && (!model.MaxCreateDate.ToNullableDate().HasValue || x.CreateDate < model.MaxCreateDate.ToNullableDate()));
        }

        [NonAction]
        private static IQueryable<OrderAttributeLink> GetOrder(BaseParameterModel model, IQueryable<OrderAttributeLink> query)
        {
            switch (model.SortingColumn)
            {
                case "AttributeName":
                    return model.IsDescSortingOrder
                        ? query.OrderByDescending(x => x.Attribute.Name)
                        : query.OrderBy(x => x.Attribute.Name);
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
        private async Task<int> GetAttribute(OrderAttributeLinkModel model)
        {
            var attribute = (model.AttributeId > 0
                                ? await _storage.OrderAttribute
                                    .FirstOrDefaultAsync(x => x.StoreId == UserContext.StoreId && x.Id == model.AttributeId)
                                    .ConfigureAwait(false)
                                : await _storage.OrderAttribute
                                    .FirstOrDefaultAsync(x =>
                                        x.StoreId == UserContext.StoreId && x.Name.Trim().ToLower() == model.AttributeName.Trim().ToLower())
                                    .ConfigureAwait(false)) ?? new OrderAttribute
                                    {
                                        Key = model.AttributeName.Trim().Replace(" ", "_"),
                                        Name = model.AttributeName.Trim(),
                                        StoreId = UserContext.StoreId
                                    };

            if (attribute.Id > 0)
            {
                _storage.OrderAttribute.Update(attribute);
                await _storage.SaveChangesAsync().ConfigureAwait(false);
                return attribute.Id;
            }

            var savedOrderAttribute = await _storage.OrderAttribute.AddAsync(attribute).ConfigureAwait(false);
            await _storage.SaveChangesAsync().ConfigureAwait(false);
            return savedOrderAttribute.Entity.Id;
        }
    }
}