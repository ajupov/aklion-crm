using System;
using System.Collections.Generic;
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
            var order = new Order
            {
                Name = model.Name,
                StoreId = UserContext.StoreId,
                IsDeleted = false,
                CreateDate = DateTime.Now,
                ModifyDate = null
            };

            await _storage.Order.AddAsync(order).ConfigureAwait(false);
            await _storage.SaveChangesAsync().ConfigureAwait(false);
        }

        [HttpPost]
        [Route("Update")]
        public async Task Update(OrderModel model)
        {
            var order = await _storage.Order.FirstOrDefaultAsync(x => x.Id == model.Id).ConfigureAwait(false);
            if (order.StoreId != UserContext.StoreId)
            {
                throw new NotAccessChangingException();
            }

            if (order.IsDeleted)
            {
                throw new ObjectIsDeletedException();
            }

            order.Name = model.Name;
            order.ModifyDate = DateTime.Now;

            _storage.Order.Update(order);
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
            model.Name = !string.IsNullOrWhiteSpace(model.Name) ? model.Name.Trim().ToLower() : null;
            model.VendorCode = !string.IsNullOrWhiteSpace(model.VendorCode) ? model.VendorCode.Trim().ToLower() : null;

            return _storage.Order
                .Include(x => x.ParentOrder)
                .Include(x => x.Status)
                .Include(x => x.AttributeLinks)
                .Where(x => x.StoreId == UserContext.StoreId
                            && (!model.Id.HasValue || x.Id == model.Id)
                            && (string.IsNullOrEmpty(model.Name) || x.Name.Trim().ToLower().Contains(model.Name))
                            && (!model.MinPrice.HasValue || x.Price > model.MinPrice)
                            && (!model.MaxPrice.HasValue || x.Price < model.MaxPrice)
                            && (!model.StatusId.HasValue || x.StatusId == model.StatusId)
                            && (!model.ParentOrderId.HasValue || x.ParentOrderId == model.ParentOrderId)
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
        private static IQueryable<Order> GetOrder(BaseParameterModel model, IQueryable<Order> query)
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