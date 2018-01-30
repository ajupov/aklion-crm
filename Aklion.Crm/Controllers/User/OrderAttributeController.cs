using System.Collections.Generic;
using System.Threading.Tasks;
using Aklion.Crm.Attributes;
using Aklion.Crm.Business.AuditLog;
using Aklion.Crm.Dao.OrderAttribute;
using Aklion.Crm.Mappers.Administration.OrderAttribute;
using Aklion.Crm.Models;
using Aklion.Crm.Models.Administration.OrderAttribute;
using Microsoft.AspNetCore.Mvc;

namespace Aklion.Crm.Controllers.Administration
{
    [Route("OrderAttributes")]
    public class OrderAttributeController : BaseController
    {
        private readonly IAuditLogService _auditLogService;
        private readonly IOrderAttributeDao _orderAttributeDao;

        public OrderAttributeController(
            IAuditLogService auditLogService,
            IOrderAttributeDao orderAttributeDao)
        {
            _auditLogService = auditLogService;
            _orderAttributeDao = orderAttributeDao;
        }

        [HttpGet]
        [Route("GetList")]
        public async Task<PagingModel<OrderAttributeModel>> GetList(OrderAttributeParameterModel model)
        {
            var result = await _orderAttributeDao.GetPagedListAsync(model.MapNew()).ConfigureAwait(false);

            return result.MapNew(model.Page, model.Size);
        }

        [HttpGet]
        [Route("GetForAutocompleteByDescriptionPattern")]
        public Task<Dictionary<string, int>> GetForAutocompleteByDescriptionPattern(string pattern, int storeId = 0)
        {
            return _orderAttributeDao.GetForAutocompleteAsync(pattern.MapNew(storeId));
        }

        [HttpPost]
        [Route("Create")]
        [AjaxErrorHandle]
        public async Task Create(OrderAttributeModel model)
        {
            var newModel = model.MapNew();

            newModel.Id = await _orderAttributeDao.CreateAsync(newModel).ConfigureAwait(false);

            _auditLogService.LogInserting(UserContext.UserId, UserContext.StoreId, newModel);
        }

        [HttpPost]
        [Route("Update")]
        [AjaxErrorHandle]
        public async Task Update(OrderAttributeModel model)
        {
            var oldModel = await _orderAttributeDao.GetAsync(model.Id).ConfigureAwait(false);
            var oldModelClone = oldModel.Clone();

            var newModel = oldModel.MapFrom(model);

            await _orderAttributeDao.UpdateAsync(newModel).ConfigureAwait(false);

            _auditLogService.LogUpdating(UserContext.UserId, UserContext.StoreId, oldModelClone, newModel);
        }

        [HttpPost]
        [Route("Delete")]
        [AjaxErrorHandle]
        public async Task Delete(int id)
        {
            var oldModel = await _orderAttributeDao.GetAsync(id).ConfigureAwait(false);

            await _orderAttributeDao.DeleteAsync(id).ConfigureAwait(false);

            _auditLogService.LogDeleting(UserContext.UserId, UserContext.StoreId, oldModel);
        }
    }
}