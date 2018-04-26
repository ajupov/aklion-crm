using System;
using System.Linq;
using System.Threading.Tasks;
using Crm.Attributes;
using Crm.Exceptions;
using Crm.Models;
using Crm.Models.User.Order;
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
    [Route("Orders")]
    public class OrdersController : BaseController
    {
        private readonly Storage _storage;

        public OrdersController(Storage storage)
        {
            _storage = storage;
        }

        [HttpGet]
        [Route("")]
        [Route("Index")]
        public IActionResult Index()
        {
            return View("~/Views/Order/Index.cshtml");
        }

        [HttpGet]
        [Route("GetList")]
        public async Task<PagingModel<OrderModel>> GetList(OrderParameterModel model)
        {
            var query = GetQuery(model);
            var list = await GetOrder(model, query).Skip(model.SkipCount).Take(model.TakeCount).ToListAsync().ConfigureAwait(false);
            var count = await query.CountAsync().ConfigureAwait(false);

            var result = list.Select(x => new OrderModel
            {
                Id = x.Id,
                ClientId = x.ClientId,
                ClientName = x.Client.Name,
                SourceId = x.SourceId,
                SourceName = x.Source.Name,
                StatusId = x.StatusId,
                StatusName = x.Status?.Name,
                TotalSum = x.DiscountSum,
                DiscountSum = x.DiscountSum,
                IsDeleted = x.IsDeleted,
                CreateDate = x.CreateDate.ToDateTimeString()
            }).ToList();

            return new PagingModel<OrderModel>(result, count, model.Page, model.Size);
        }

        [HttpPost]
        [Route("Create")]
        public async Task Create(OrderModel model)
        {
            var now = DateTime.Now;

            var order = new Order
            {
                StoreId = UserContext.StoreId,
                ClientId = model.ClientId,
                SourceId = model.SourceId,
                StatusId = model.StatusId,
                DiscountSum = model.DiscountSum,
                CreateDate = now
            };

            var savedOrder = await _storage.Order.AddAsync(order).ConfigureAwait(false);
            await _storage.SaveChangesAsync().ConfigureAwait(false);

            var orderAttributeLins = model.AttributeLinks.Select(x => new OrderAttributeLink
            {
                StoreId = UserContext.StoreId,
                OrderId = savedOrder.Entity.Id,
                AttributeId = x.AttributeId,
                Value = x.Value,
                CreateDate = now
            });

            await _storage.OrderAttributeLink.AddRangeAsync(orderAttributeLins).ConfigureAwait(false);

            var orderItems = model.OrderItems.Select(x => new OrderItem
            {
                StoreId = UserContext.StoreId,
                OrderId = savedOrder.Entity.Id,
                ProductId = x.ProductId,
                Price = x.Price,
                Count = x.Count
            });

            await _storage.OrderItem.AddRangeAsync(orderItems).ConfigureAwait(false);
            await _storage.SaveChangesAsync().ConfigureAwait(false);
        }

        [HttpPost]
        [Route("Update")]
        public async Task Update(OrderModel model)
        {
            var now = DateTime.Now;

            var order = await _storage.Order.Include(x => x.AttributeLinks).Include(x => x.OrderItems).FirstOrDefaultAsync(x => x.Id == model.Id)
                .ConfigureAwait(false);
            if (order.StoreId != UserContext.StoreId)
            {
                throw new NotAccessChangingException();
            }

            if (order.IsDeleted)
            {
                throw new ObjectIsDeletedException();
            }

            order.ClientId = model.ClientId;
            order.SourceId = model.SourceId;
            order.StatusId = model.StatusId;
            order.DiscountSum = model.DiscountSum;
            order.ModifyDate = now;

            order.AttributeLinks.ForEach(x =>
            {
                var orderAttributeLink = model.AttributeLinks.FirstOrDefault(l => l.Id == x.Id);
                if (orderAttributeLink == null)
                {
                    return;
                }

                x.AttributeId = orderAttributeLink.AttributeId;
                x.Value = orderAttributeLink.Value;
                x.ModifyDate = now;
            });

            order.OrderItems.ForEach(x =>
            {
                var orderItem = model.OrderItems.FirstOrDefault(l => l.Id == x.Id);
                if (orderItem == null)
                {
                    return;
                }

                x.ProductId = orderItem.ProductId;
                x.Price = orderItem.Price;
                x.Count = orderItem.Count;
            });

            _storage.Order.Update(order);

            var orderAttributeLinks = model.AttributeLinks.Where(x => !order.AttributeLinks.Select(i => i.Id).Contains(x.Id)).Select(x =>
                new OrderAttributeLink
                {
                    StoreId = UserContext.StoreId,
                    OrderId = order.Id,
                    AttributeId = x.AttributeId,
                    Value = x.Value,
                    CreateDate = now
                }).ToList();

            await _storage.OrderAttributeLink.AddRangeAsync(orderAttributeLinks).ConfigureAwait(false);

            var orderItems = model.OrderItems.Where(x => !order.OrderItems.Select(i => i.Id).Contains(x.Id)).Select(x =>
                new OrderItem
                {
                    StoreId = UserContext.StoreId,
                    OrderId = order.Id,
                    ProductId = x.ProductId,
                    Price = x.Price,
                    Count = x.Count
                }).ToList();

            await _storage.OrderItem.AddRangeAsync(orderItems).ConfigureAwait(false);

            await _storage.SaveChangesAsync().ConfigureAwait(false);
        }

        [HttpPost]
        [Route("Delete")]
        public async Task Delete(int id)
        {
            var order = await _storage.Order.FirstOrDefaultAsync(x => x.Id == id).ConfigureAwait(false);
            if (order.StoreId != UserContext.StoreId)
            {
                throw new NotAccessChangingException();
            }

            order.IsDeleted = !order.IsDeleted;
            await _storage.SaveChangesAsync().ConfigureAwait(false);
        }

        [NonAction]
        private IQueryable<Order> GetQuery(OrderParameterModel model)
        {
            model.ClientName = !string.IsNullOrWhiteSpace(model.ClientName) ? model.ClientName.Trim().ToLower() : null;
            model.SourceName = !string.IsNullOrWhiteSpace(model.SourceName) ? model.SourceName.Trim().ToLower() : null;
            model.StatusName = !string.IsNullOrWhiteSpace(model.StatusName) ? model.StatusName.Trim().ToLower() : null;

            return _storage.Order
                .Include(x => x.Client)
                .Include(x => x.Source)
                .Include(x => x.Status)
                .Include(x => x.AttributeLinks)
                .Include(x => x.OrderItems)
                .Where(x => x.StoreId == UserContext.StoreId
                            && (!model.Id.HasValue || x.Id == model.Id)
                            && (!model.ClientId.HasValue || x.ClientId == model.ClientId)
                            && (string.IsNullOrEmpty(model.ClientName) || x.Client.Name.Trim().ToLower().Contains(model.ClientName))
                            && (!model.SourceId.HasValue || x.SourceId == model.SourceId)
                            && (string.IsNullOrEmpty(model.SourceName) || x.Source.Name.Trim().ToLower().Contains(model.SourceName))
                            && (!model.StatusId.HasValue || x.StatusId == model.StatusId)
                            && (string.IsNullOrEmpty(model.StatusName) || x.Status.Name.Trim().ToLower().Contains(model.StatusName))
                            && (!model.MinTotalSum.HasValue || x.OrderItems.Sum(i => i.Price) > model.MinTotalSum)
                            && (!model.MaxTotalSum.HasValue || x.OrderItems.Sum(i => i.Price) < model.MaxTotalSum)
                            && (!model.MinDiscountSum.HasValue || x.DiscountSum < model.MinDiscountSum)
                            && (!model.MaxDiscountSum.HasValue || x.DiscountSum < model.MaxDiscountSum)
                            && (!model.MinCreateDate.ToNullableDate().HasValue || x.CreateDate < model.MinCreateDate.ToNullableDate())
                            && (!model.MaxCreateDate.ToNullableDate().HasValue || x.CreateDate < model.MaxCreateDate.ToNullableDate())
                            && (!model.IsDeleted.HasValue || x.IsDeleted == model.IsDeleted)
                            && (x.AttributeLinks == null || model.Attributes == null || !model.Attributes.Any()
                                || model.Attributes.Where(a => !string.IsNullOrWhiteSpace(a.Value)).All(a =>
                                    x.AttributeLinks.Any(ca =>
                                        a.Key == ca.AttributeId && ca.Value.Trim().ToLower().Contains(a.Value.Trim().ToLower())))));
        }

        [NonAction]
        private static IQueryable<Order> GetOrder(BaseParameterModel model, IQueryable<Order> query)
        {
            switch (model.SortingColumn)
            {
                case "ClientName":
                    return model.IsDescSortingOrder
                        ? query.OrderBy(x => x.IsDeleted).ThenByDescending(x => x.Client.Name)
                        : query.OrderBy(x => x.IsDeleted).ThenBy(x => x.Client.Name);
                case "SourceName":
                    return model.IsDescSortingOrder
                        ? query.OrderBy(x => x.IsDeleted).ThenByDescending(x => x.Source.Name)
                        : query.OrderBy(x => x.IsDeleted).ThenBy(x => x.Source.Name);
                case "StatusName":
                    return model.IsDescSortingOrder
                        ? query.OrderBy(x => x.IsDeleted).ThenByDescending(x => x.Status.Name)
                        : query.OrderBy(x => x.IsDeleted).ThenBy(x => x.Status.Name);
                case "TotalSum":
                    return model.IsDescSortingOrder
                        ? query.OrderBy(x => x.IsDeleted).ThenByDescending(x => x.OrderItems.Sum(i => i.Price))
                        : query.OrderBy(x => x.IsDeleted).ThenBy(x => x.OrderItems.Sum(i => i.Price));
                case "DiscountSum":
                    return model.IsDescSortingOrder
                        ? query.OrderBy(x => x.IsDeleted).ThenByDescending(x => x.DiscountSum)
                        : query.OrderBy(x => x.IsDeleted).ThenBy(x => x.DiscountSum);
                default:
                    return model.IsDescSortingOrder
                        ? query.OrderBy(x => x.IsDeleted).ThenByDescending(x => x.CreateDate)
                        : query.OrderBy(x => x.IsDeleted).ThenBy(x => x.CreateDate);
            }
        }
    }
}