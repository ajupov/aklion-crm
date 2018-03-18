using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Crm.Attributes;
using Crm.Exceptions;
using Crm.Models;
using Crm.Models.User.ClientAttribute;
using Crm.Storages;
using Crm.Storages.Models;
using Infrastructure.Dao.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Crm.Controllers
{
    [AjaxErrorHandle]
    [Route("ClientAttributes")]
    public class ClientAttributesController : BaseController
    {
        private readonly Storage _storage;

        public ClientAttributesController(Storage storage)
        {
            _storage = storage;
        }

        [HttpGet]
        [Route("GetList")]
        public async Task<PagingModel<ClientAttributeModel>> GetList(ClientAttributeParameterModel model)
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

            var result = list.Select(x => new ClientAttributeModel
            {
                Id = x.Id,
                Key = x.Key,
                Name = x.Name,
                IsDeleted = x.IsDeleted
            }).ToList();

            return new PagingModel<ClientAttributeModel>(result, count, model.Page, model.Size);
        }

        [HttpGet]
        [Route("GetAutocomplete")]
        public async Task<Dictionary<string, int>> GetAutocomplete(string pattern)
        {
            var store = _storage.Store.FirstOrDefault(x => x.Id == UserContext.StoreId);
            if (store == null)
            {
                throw new StoreNotFoundException();
            }

            if (store.IsDeleted)
            {
                throw new StoreIsDeletedException();
            }

            return await _storage.ClientAttribute.Where(x =>
                    !x.IsDeleted
                    && x.StoreId == UserContext.StoreId
                    && x.Name.StartsWith(pattern, true, CultureInfo.InvariantCulture))
                .ToDictionaryAsync(k => k.Name, v => v.Id)
                .ConfigureAwait(false);
        }

        [HttpGet]
        [Route("GetSelect")]
        public async Task<Dictionary<string, int>> GetSelect()
        {
            var store = _storage.Store.FirstOrDefault(x => x.Id == UserContext.StoreId);
            if (store == null)
            {
                throw new StoreNotFoundException();
            }

            if (store.IsDeleted)
            {
                throw new StoreIsDeletedException();
            }

            return await _storage.ClientAttribute.Where(x =>
                    !x.IsDeleted
                    && x.StoreId == UserContext.StoreId)
                .ToDictionaryAsync(k => k.Name, v => v.Id)
                .ConfigureAwait(false);
        }

        [HttpPost]
        [Route("Create")]
        public async Task Create(ClientAttributeModel model)
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

            var clientAttribute = new ClientAttribute
            {
                Key = model.Key.Trim(),
                Name = model.Name.Trim(),
                Store = store,
                IsDeleted = false,
                CreateDate = DateTime.Now,
                ModifyDate = null
            };

            await _storage.ClientAttribute.AddAsync(clientAttribute).ConfigureAwait(false);
            await _storage.SaveChangesAsync().ConfigureAwait(false);
        }

        [HttpPost]
        [Route("Update")]
        public async Task Update(ClientAttributeModel model)
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

            var clientAttribute = await _storage.ClientAttribute.FirstOrDefaultAsync(x => x.Id == model.Id).ConfigureAwait(false);
            if (clientAttribute.StoreId != UserContext.StoreId)
            {
                throw new NotAccessChangingException();
            }

            if (clientAttribute.IsDeleted)
            {
                throw new ObjectIsDeletedException();
            }

            clientAttribute.Key = model.Key.Trim();
            clientAttribute.Name = model.Name.Trim();
            clientAttribute.ModifyDate = DateTime.Now;

            _storage.Update(clientAttribute);
            await _storage.SaveChangesAsync().ConfigureAwait(false);
        }

        [HttpPost]
        [Route("Delete")]
        public async Task<bool> Delete(int id)
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

            var clientAttribute = await _storage.ClientAttribute.FirstOrDefaultAsync(x => x.Id == id).ConfigureAwait(false);
            if (clientAttribute.StoreId != UserContext.StoreId)
            {
                throw new NotAccessChangingException();
            }

            clientAttribute.IsDeleted = !clientAttribute.IsDeleted;
            await _storage.SaveChangesAsync().ConfigureAwait(false);

            return clientAttribute.IsDeleted;
        }

        [NonAction]
        private IQueryable<ClientAttribute> GetQuery(ClientAttributeParameterModel model)
        {
            return _storage.ClientAttribute.Where(x =>
                x.StoreId == UserContext.StoreId
                && (string.IsNullOrEmpty(model.Key) || x.Key.StartsWith(model.Key, true, CultureInfo.InvariantCulture))
                && (string.IsNullOrEmpty(model.Name) || x.Name.StartsWith(model.Name, true, CultureInfo.InvariantCulture)));
        }

        [NonAction]
        private static IQueryable<ClientAttribute> GetOrder(BaseParameterModel model, IQueryable<ClientAttribute> query)
        {
            switch (model.SortingColumn)
            {
                case "Key":
                    return model.IsDescSortingOrder ? query.OrderBy(x => x.IsDeleted).ThenByDescending(x => x.Key) : query.OrderBy(x => x.IsDeleted).ThenBy(x => x.Key);
                default:
                    return model.IsDescSortingOrder ? query.OrderBy(x => x.IsDeleted).ThenByDescending(x => x.Name) : query.OrderBy(x => x.IsDeleted).ThenBy(x => x.Name);
            }
        }
    }
}