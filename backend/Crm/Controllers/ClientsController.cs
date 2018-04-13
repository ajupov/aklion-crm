using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Crm.Attributes;
using Crm.Exceptions;
using Crm.Models;
using Crm.Models.User.Client;
using Crm.Storages;
using Crm.Storages.Models;
using Infrastructure.Dao.Models;
using Infrastructure.DateTime;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Crm.Controllers
{
    [AjaxErrorHandle]
    [Route("Clients")]
    public class ClientsController : BaseController
    {
        private readonly Storage _storage;

        public ClientsController(Storage storage)
        {
            _storage = storage;
        }

        [HttpGet]
        [Route("")]
        [Route("Index")]
        public IActionResult Index()
        {
            return View("~/Views/Client/Index.cshtml");
        }

        [HttpGet]
        [Route("GetList")]
        public async Task<PagingModel<ClientModel>> GetList(ClientParameterModel model)
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

            var result = list.Select(x => new ClientModel
            {
                Id = x.Id,
                Name = x.Name,
                IsDeleted = x.IsDeleted,
                CreateDate = x.CreateDate.ToDateTimeString()
            }).ToList();

            return new PagingModel<ClientModel>(result, count, model.Page, model.Size);
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

            return await _storage.Client.Where(x =>
                    !x.IsDeleted
                    && x.StoreId == UserContext.StoreId
                    && x.Name.StartsWith(pattern, true, CultureInfo.InvariantCulture))
                .ToDictionaryAsync(k => k.Name, v => v.Id)
                .ConfigureAwait(false);
        }

        [HttpPost]
        [Route("Create")]
        public async Task Create(ClientModel model)
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

            var client = new Client
            {
                Name = model.Name,
                Store = store,
                IsDeleted = false,
                CreateDate = DateTime.Now,
                ModifyDate = null
            };

            await _storage.Client.AddAsync(client).ConfigureAwait(false);
            await _storage.SaveChangesAsync().ConfigureAwait(false);
        }

        [HttpPost]
        [Route("Update")]
        public async Task Update(ClientModel model)
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

            var client = await _storage.Client.FirstOrDefaultAsync(x => x.Id == model.Id).ConfigureAwait(false);
            if (client.StoreId != UserContext.StoreId)
            {
                throw new NotAccessChangingException();
            }

            if (client.IsDeleted)
            {
                throw new ObjectIsDeletedException();
            }

            client.Name = model.Name;
            client.ModifyDate = DateTime.Now;

            _storage.Update(client);
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

            var client = await _storage.Client.FirstOrDefaultAsync(x => x.Id == id).ConfigureAwait(false);
            if (client.StoreId != UserContext.StoreId)
            {
                throw new NotAccessChangingException();
            }

            client.IsDeleted = !client.IsDeleted;
            await _storage.SaveChangesAsync().ConfigureAwait(false);
        }

        [NonAction]
        private IQueryable<Client> GetQuery(ClientParameterModel model)
        {
            return _storage.Client
                .Include(x => x.ClientAttributeLinks)
                .Where(x => x.StoreId == UserContext.StoreId
                            && (string.IsNullOrEmpty(model.Name) || x.Name.Contains(model.Name))
                            && (!model.MinCreateDate.ToNullableDate().HasValue || x.CreateDate > model.MinCreateDate.ToNullableDate())
                            && (!model.MaxCreateDate.ToNullableDate().HasValue || x.CreateDate < model.MaxCreateDate.ToNullableDate())
                            && (!model.IsDeleted.HasValue || x.IsDeleted == model.IsDeleted)
                            && (x.ClientAttributeLinks == null || model.Attributes == null || !model.Attributes.Any()
                                || model.Attributes.Where(a => !string.IsNullOrWhiteSpace(a.Value)).All(a =>
                                    x.ClientAttributeLinks.Any(ca =>
                                        a.Key == ca.AttributeId && !ca.IsDeleted && ca.Value.Trim().Contains(a.Value.Trim())))));
        }

        [NonAction]
        private static IQueryable<Client> GetOrder(BaseParameterModel model, IQueryable<Client> query)
        {
            switch (model.SortingColumn)
            {
                case "Name":
                    return model.IsDescSortingOrder ? query.OrderBy(x => x.IsDeleted).ThenByDescending(x => x.Name) : query.OrderBy(x => x.IsDeleted).ThenBy(x => x.Name);
                default:
                    return model.IsDescSortingOrder ? query.OrderBy(x => x.IsDeleted).ThenByDescending(x => x.CreateDate) : query.OrderBy(x => x.IsDeleted).ThenBy(x => x.CreateDate);
            }
        }
    }
}