using System;
using System.Threading.Tasks;
using Aklion.Crm.Dao;
using Aklion.Crm.Dao.Store;
using Aklion.Crm.Dao.Store.Models;
using Aklion.Crm.Models.JqGrid;
using Aklion.Crm.Models.Store;
using Microsoft.AspNetCore.Mvc;

namespace Aklion.Crm.Controllers
{
    public class StoresController : Controller
    {
        private readonly IStoreDao _storeDao;
        private readonly IDao _dao;

        public StoresController(IStoreDao storeDao, IDao dao)
        {
            _storeDao = storeDao;
            _dao = dao;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<JqGridDataModel> Get(StoreGetModel model)
        {
            var rows = await _storeDao.GetList(0, int.MaxValue).ConfigureAwait(false);
            return new JqGridDataModel(rows, 1000, 1, 10);
        }

        [HttpPost]
        public async Task Edit(StoreEditModel model)
        {
            switch (model.oper)
            {
                case "add":
                {
                    var store = new Store
                    {
                        CreateUserId = 0,
                        Name = model.Name,
                        ApiKey = null,
                        ApiSecret = null,
                        IsLocked = model.IsLocked,
                        IsDeleted = model.IsDeleted,
                        CreateDate = DateTime.Now,
                        ModifyDate = null
                    };
                    
                    await _dao.Create(store).ConfigureAwait(false);
                    break;
                }
                case "edit":
                {
                    var store = await _storeDao.Get(model.Id).ConfigureAwait(false);

                    store.CreateUserId = model.CreateUserId;
                    store.Name = model.Name;
                    store.ApiKey = model.ApiKey;
                    store.ApiSecret = model.ApiSecret;
                    store.ModifyDate = DateTime.Now;
                    store.IsLocked = model.IsLocked;
                    store.IsDeleted = model.IsDeleted;

                    await _storeDao.Update(store).ConfigureAwait(false);
                    break;
                }
                case "del":
                {
                    await _storeDao.Delete(model.Id).ConfigureAwait(false);
                    break;
                }
            }
        }
    }
}