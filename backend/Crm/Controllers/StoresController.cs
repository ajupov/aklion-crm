using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Crm.Attributes;
using Crm.Business.Permission;
using Crm.Exceptions;
using Crm.Models;
using Crm.Models.User.Store;
using Crm.Storages;
using Crm.Storages.Models;
using Infrastructure.Dao.Models;
using Infrastructure.DateTime;
using Infrastructure.Random;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Crm.Controllers
{
    [AjaxErrorHandle]
    [Route("Stores")]
    public class StoresController : BaseController
    {
        private readonly Storage _storage;
        private readonly IPermissionService _permissionService;

        public StoresController(Storage storage, IPermissionService permissionService)
        {
            _storage = storage;
            _permissionService = permissionService;
        }

        [HttpGet]
        [Route("")]
        [Route("Index")]
        public IActionResult Index()
        {
            return View("~/Views/Store/Index.cshtml");
        }

        [HttpGet]
        [Route("GetList")]
        public async Task<PagingModel<StoreModel>> GetList(StoreParameterModel model)
        {
            var query = GetQuery(model);
            var list = await GetOrder(model, query).Skip(model.SkipCount).Take(model.TakeCount).ToListAsync().ConfigureAwait(false);
            var count = await query.CountAsync().ConfigureAwait(false);

            var result = list.Select(x => new StoreModel
            {
                Id = x.Id,
                Name = x.Name,
                ApiSecret = x.ApiSecret,
                IsDeleted = x.IsDeleted,
                CreateDate = x.CreateDate.ToDateTimeString()
            }).ToList();

            return new PagingModel<StoreModel>(result, count, model.Page, model.Size);
        }

        [HttpGet]
        [Route("GetAutocomplete")]
        public async Task<Dictionary<string, int>> GetAutocomplete(string pattern)
        {
            pattern = pattern.ToLower();

            return await GetUserStores().Where(x => !x.IsDeleted && x.Name.ToLower().Contains(pattern))
                .ToDictionaryAsync(k => k.Name, v => v.Id).ConfigureAwait(false);
        }

        [HttpPost]
        [Route("Create")]
        public async Task Create(StoreModel model)
        {
            var store = new Store
            {
                Name = model.Name,
                CreateDate = DateTime.Now
            };

            var savedStore = await _storage.Store.AddAsync(store).ConfigureAwait(false);
            await _storage.SaveChangesAsync().ConfigureAwait(false);
            var storeId = savedStore.Entity.Id;

            var userPermissions = _permissionService.GetForRegistration().Select(p => new UserPermission
            {
                UserId = UserContext.UserId,
                StoreId = storeId,
                Permission = p
            }).ToList();

            await _storage.UserPermission.AddRangeAsync(userPermissions).ConfigureAwait(false);
            await _storage.SaveChangesAsync().ConfigureAwait(false);
        }

        [HttpPost]
        [Route("Update")]
        public async Task Update(StoreModel model)
        {
            var hasPermission = await _storage.UserPermission.AnyAsync(x => x.UserId == UserContext.UserId && x.StoreId == model.Id)
                .ConfigureAwait(false);
            if (!hasPermission)
            {
                throw new NotAccessChangingException();
            }

            var store = await _storage.Store.FirstOrDefaultAsync(x => x.Id == model.Id).ConfigureAwait(false);

            store.Name = model.Name.Trim();
            store.ModifyDate = DateTime.Now;

            _storage.Store.Update(store);
            await _storage.SaveChangesAsync().ConfigureAwait(false);
        }

        [HttpPost]
        [Route("Delete")]
        public async Task Delete(int id)
        {
            var hasPermission = await _storage.UserPermission.AnyAsync(x => x.UserId == UserContext.UserId && x.StoreId == id)
                .ConfigureAwait(false);
            if (!hasPermission)
            {
                throw new NotAccessChangingException();
            }

            var store = await _storage.Store.FirstOrDefaultAsync(x => x.Id == id).ConfigureAwait(false);

            store.IsDeleted = !store.IsDeleted;

            _storage.Store.Update(store);
            await _storage.SaveChangesAsync().ConfigureAwait(false);
        }

        [HttpPost]
        [Route("GenerateApiSecret")]
        public async Task<string> GenerateApiSecret(int id)
        {
            var hasPermission = await _storage.UserPermission.AnyAsync(x => x.UserId == UserContext.UserId && x.StoreId == id)
                .ConfigureAwait(false);
            if (!hasPermission)
            {
                throw new NotAccessChangingException();
            }

            var store = await _storage.Store.FirstOrDefaultAsync(x => x.Id == id).ConfigureAwait(false);

            store.ApiSecret = RandomGenerator.GenerateAlphaNumbericString(16);

            _storage.Store.Update(store);
            await _storage.SaveChangesAsync().ConfigureAwait(false);

            return store.ApiSecret;
        }

        [NonAction]
        private IQueryable<Store> GetQuery(StoreParameterModel model)
        {
            model.Name = !string.IsNullOrWhiteSpace(model.Name) ? model.Name.Trim().ToLower() : null;

            return GetUserStores()
                .Where(x => (!model.Id.HasValue || x.Id == model.Id)
                            && (string.IsNullOrEmpty(model.Name) || x.Name.Trim().ToLower().Contains(model.Name))
                            && (!model.MinCreateDate.ToNullableDate().HasValue || x.CreateDate > model.MinCreateDate.ToNullableDate())
                            && (!model.MaxCreateDate.ToNullableDate().HasValue || x.CreateDate < model.MaxCreateDate.ToNullableDate())
                            && (!model.IsDeleted.HasValue || x.IsDeleted == model.IsDeleted));
        }

        [NonAction]
        private static IQueryable<Store> GetOrder(BaseParameterModel model, IQueryable<Store> query)
        {
            switch (model.SortingColumn)
            {
                case "Name":
                    return model.IsDescSortingOrder
                        ? query.OrderBy(x => x.IsDeleted).ThenByDescending(x => x.Name)
                        : query.OrderBy(x => x.IsDeleted).ThenBy(x => x.Name);
                default:
                    return model.IsDescSortingOrder
                        ? query.OrderBy(x => x.IsDeleted).ThenByDescending(x => x.CreateDate)
                        : query.OrderBy(x => x.IsDeleted).ThenBy(x => x.CreateDate);
            }
        }

        [NonAction]
        private IQueryable<Store> GetUserStores()
        {
            return _storage.UserPermission.Where(x => x.UserId == UserContext.UserId)
                .Join(_storage.Store, p => p.StoreId, s => s.Id, (p, s) => s);
        }
    }
}