using System;
using System.Linq;
using System.Threading.Tasks;
using Crm.Attributes;
using Crm.Exceptions;
using Crm.Models;
using Crm.Models.User.ClientAttributeLink;
using Crm.Storages;
using Crm.Storages.Models;
using Infrastructure.Dao.Models;
using Infrastructure.DateTime;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Crm.Controllers
{
    [AjaxErrorHandle]
    [Route("ClientAttributeLinks")]
    public class ClientAttributeLinksController : BaseController
    {
        private readonly Storage _storage;

        public ClientAttributeLinksController(Storage storage)
        {
            _storage = storage;
        }

        [HttpGet]
        [Route("GetList")]
        public async Task<PagingModel<ClientAttributeLinkModel>> GetList(ClientAttributeLinkParameterModel model)
        {
            var store = await _storage.Store.FirstOrDefaultAsync(x => x.Id == UserContext.StoreId).ConfigureAwait(false);
            if (store == null)
            {
                throw new StoreNotFoundException();
            }

            if (store.IsDeleted)
            {
                throw new StoreIsDeletedException();
            }

            var query = GetQuery(model);

            var list = await GetOrder(model, query).Skip(model.SkipCount).Take(model.TakeCount).ToListAsync().ConfigureAwait(false);
            var count = await query.CountAsync().ConfigureAwait(false);

            var result = list.Select(x => new ClientAttributeLinkModel
            {
                Id = x.Id,
                ClientId = x.ClientId,
                AttributeId = x.AttributeId,
                AttributeName = x.Attribute.Name,
                Value = x.Value,
                IsDeleted = x.IsDeleted,
                CreateDate = x.CreateDate.ToDateTimeString()
            }).ToList();

            return new PagingModel<ClientAttributeLinkModel>(result, count, model.Page, model.Size);
        }

        [HttpPost]
        [Route("Create")]
        public async Task Create(ClientAttributeLinkModel model)
        {
            var store = await _storage.Store.FirstOrDefaultAsync(x => x.Id == UserContext.StoreId).ConfigureAwait(false);
            if (store == null)
            {
                throw new StoreNotFoundException();
            }

            if (store.IsDeleted)
            {
                throw new StoreIsDeletedException();
            }

            var attributeId = await GetAttribute(model).ConfigureAwait(false);

            var clientAttributeLink = new ClientAttributeLink
            {
                StoreId = UserContext.StoreId,
                ClientId = model.ClientId,
                AttributeId = attributeId,
                Value = model.Value,
                Store = store,
                IsDeleted = false,
                CreateDate = DateTime.Now,
                ModifyDate = null
            };

            await _storage.ClientAttributeLink.AddAsync(clientAttributeLink).ConfigureAwait(false);
            await _storage.SaveChangesAsync().ConfigureAwait(false);
        }

        [HttpPost]
        [Route("Update")]
        public async Task Update(ClientAttributeLinkModel model)
        {
            var store = await _storage.Store.FirstOrDefaultAsync(x => x.Id == UserContext.StoreId).ConfigureAwait(false);
            if (store == null)
            {
                throw new StoreNotFoundException();
            }

            if (store.IsDeleted)
            {
                throw new StoreIsDeletedException();
            }

            var clientAttributeLink = await _storage.ClientAttributeLink.FirstOrDefaultAsync(x => x.Id == model.Id).ConfigureAwait(false);
            if (clientAttributeLink.StoreId != UserContext.StoreId)
            {
                throw new NotAccessChangingException();
            }

            if (clientAttributeLink.IsDeleted)
            {
                throw new ObjectIsDeletedException();
            }

            clientAttributeLink.AttributeId = model.AttributeId;
            clientAttributeLink.Value = model.Value;
            clientAttributeLink.ModifyDate = DateTime.Now;

            _storage.Update(clientAttributeLink);
            await _storage.SaveChangesAsync().ConfigureAwait(false);
        }

        [HttpPost]
        [Route("Delete")]
        public async Task Delete(int id)
        {
            var store = await _storage.Store.FirstOrDefaultAsync(x => x.Id == UserContext.StoreId).ConfigureAwait(false);
            if (store == null)
            {
                throw new StoreNotFoundException();
            }

            if (store.IsDeleted)
            {
                throw new StoreIsDeletedException();
            }

            var clientAttributeLink = await _storage.ClientAttributeLink.FirstOrDefaultAsync(x => x.Id == id).ConfigureAwait(false);
            if (clientAttributeLink.StoreId != UserContext.StoreId)
            {
                throw new NotAccessChangingException();
            }

            clientAttributeLink.IsDeleted = !clientAttributeLink.IsDeleted;
            await _storage.SaveChangesAsync().ConfigureAwait(false);
        }

        [NonAction]
        private IQueryable<ClientAttributeLink> GetQuery(ClientAttributeLinkParameterModel model)
        {
            return _storage.ClientAttributeLink.Include(x => x.Attribute).Where(x =>
                x.StoreId == UserContext.StoreId
                && x.ClientId == model.ClientId
                && (!model.Id.HasValue || x.Id == model.Id)
                && (!model.AttributeId.HasValue || x.AttributeId == model.AttributeId)
                && (string.IsNullOrEmpty(model.AttributeName) || x.Attribute.Name.Contains(model.AttributeName))
                && (string.IsNullOrEmpty(model.Value) || x.Value.Contains(model.Value))
                && (!model.IsDeleted.HasValue || x.IsDeleted == model.IsDeleted)
                && (!model.MinCreateDate.ToNullableDate().HasValue || x.CreateDate > model.MinCreateDate.ToNullableDate())
                && (!model.MaxCreateDate.ToNullableDate().HasValue || x.CreateDate < model.MaxCreateDate.ToNullableDate()));
        }

        [NonAction]
        private static IQueryable<ClientAttributeLink> GetOrder(BaseParameterModel model, IQueryable<ClientAttributeLink> query)
        {
            switch (model.SortingColumn)
            {
                case "AttributeName":
                    return model.IsDescSortingOrder ? query.OrderByDescending(x => x.Attribute.Name) : query.OrderBy(x => x.Attribute.Name);
                case "Value":
                    return model.IsDescSortingOrder ? query.OrderByDescending(x => x.Value) : query.OrderBy(x => x.Value);
                default:
                    return model.IsDescSortingOrder ? query.OrderByDescending(x => x.CreateDate) : query.OrderBy(x => x.CreateDate);
            }
        }

        [NonAction]
        private async Task<int> GetAttribute(ClientAttributeLinkModel model)
        {
            var attribute = (model.AttributeId > 0
                                ? await _storage.ClientAttribute
                                    .FirstOrDefaultAsync(x => x.StoreId == UserContext.StoreId && x.Id == model.AttributeId)
                                    .ConfigureAwait(false)
                                : await _storage.ClientAttribute
                                    .FirstOrDefaultAsync(x =>
                                        x.StoreId == UserContext.StoreId && x.Name.ToLower() == model.AttributeName.Trim().ToLower())
                                    .ConfigureAwait(false)) ?? new ClientAttribute
                            {
                                Key = model.AttributeName.Trim().Replace(" ", "_"),
                                Name = model.AttributeName.Trim(),
                                StoreId = UserContext.StoreId,
                                IsDeleted = false,
                                CreateDate = DateTime.Now,
                                ModifyDate = null
                            };

            attribute.IsDeleted = false;

            if (attribute.Id > 0)
            {
                _storage.ClientAttribute.Update(attribute);
                await _storage.SaveChangesAsync().ConfigureAwait(false);
                return attribute.Id;
            }

            var savedClientAttribute = await _storage.ClientAttribute.AddAsync(attribute).ConfigureAwait(false);
            await _storage.SaveChangesAsync().ConfigureAwait(false);
            return savedClientAttribute.Entity.Id;
        }
    }
}