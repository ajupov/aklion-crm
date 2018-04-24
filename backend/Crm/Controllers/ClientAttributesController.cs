using System.Collections.Generic;
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
    [CheckStore]
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
            var query = GetQuery(model);
            var list = await GetOrder(model, query).Skip(model.SkipCount).Take(model.TakeCount).ToListAsync().ConfigureAwait(false);
            var count = await query.CountAsync().ConfigureAwait(false);

            var result = list.Select(x => new ClientAttributeModel
            {
                Id = x.Id,
                Key = x.Key,
                Name = x.Name
            }).ToList();

            return new PagingModel<ClientAttributeModel>(result, count, model.Page, model.Size);
        }

        [HttpGet]
        [Route("GetAutocomplete")]
        public async Task<Dictionary<string, int>> GetAutocomplete(string pattern)
        {
            pattern = pattern.ToLower();

            return await _storage.ClientAttribute.Where(x => x.StoreId == UserContext.StoreId && x.Name.ToLower().StartsWith(pattern))
                .ToDictionaryAsync(k => k.Name, v => v.Id).ConfigureAwait(false);
        }

        [HttpGet]
        [Route("GetSelect")]
        public async Task<Dictionary<string, int>> GetSelect()
        {
            return await _storage.ClientAttribute.Where(x => x.StoreId == UserContext.StoreId).ToDictionaryAsync(k => k.Name, v => v.Id)
                .ConfigureAwait(false);
        }

        [HttpPost]
        [Route("Create")]
        public async Task Create(ClientAttributeModel model)
        {
            var clientAttribute = new ClientAttribute
            {
                Key = model.Key.Trim(),
                Name = model.Name.Trim(),
                StoreId = UserContext.StoreId
            };

            await _storage.ClientAttribute.AddAsync(clientAttribute).ConfigureAwait(false);
            await _storage.SaveChangesAsync().ConfigureAwait(false);
        }

        [HttpPost]
        [Route("Update")]
        public async Task Update(ClientAttributeModel model)
        {
            var clientAttribute = await _storage.ClientAttribute.FirstOrDefaultAsync(x => x.Id == model.Id).ConfigureAwait(false);
            if (clientAttribute.StoreId != UserContext.StoreId)
            {
                throw new NotAccessChangingException();
            }

            clientAttribute.Key = model.Key.Trim();
            clientAttribute.Name = model.Name.Trim();

            _storage.ClientAttribute.Update(clientAttribute);
            await _storage.SaveChangesAsync().ConfigureAwait(false);
        }

        [HttpPost]
        [Route("Delete")]
        public async Task Delete(int id)
        {
            var clientAttribute = await _storage.ClientAttribute.FirstOrDefaultAsync(x => x.Id == id).ConfigureAwait(false);
            if (clientAttribute.StoreId != UserContext.StoreId)
            {
                throw new NotAccessChangingException();
            }

            _storage.ClientAttribute.Remove(clientAttribute);
            await _storage.SaveChangesAsync().ConfigureAwait(false);
        }

        [NonAction]
        private IQueryable<ClientAttribute> GetQuery(ClientAttributeParameterModel model)
        {
            model.Key = !string.IsNullOrWhiteSpace(model.Key) ? model.Key.Trim().ToLower() : null;
            model.Name = !string.IsNullOrWhiteSpace(model.Name) ? model.Name.Trim().ToLower() : null;

            return _storage.ClientAttribute.Where(x =>
                x.StoreId == UserContext.StoreId
                && (string.IsNullOrEmpty(model.Key) || x.Key.Trim().ToLower().Contains(model.Key))
                && (string.IsNullOrEmpty(model.Name) || x.Name.Trim().ToLower().Contains(model.Name)));
        }

        [NonAction]
        private static IQueryable<ClientAttribute> GetOrder(BaseParameterModel model, IQueryable<ClientAttribute> query)
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