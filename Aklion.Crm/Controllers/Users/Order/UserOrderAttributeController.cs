using System.Collections.Generic;
using System.Threading.Tasks;
using Aklion.Crm.Attributes;
using Aklion.Crm.Business.AuditLog;
using Aklion.Crm.Dao.OrderAttribute;
using Aklion.Crm.Mappers.User.OrderAttribute;
using Aklion.Crm.Models;
using Aklion.Crm.Models.User.OrderAttribute;
using Microsoft.AspNetCore.Mvc;

namespace Aklion.Crm.Controllers.UsersControllers
{
    [Route("OrderAttributes")]
    public class UserOrderAttributeController : BaseController
    {
        private readonly IAuditLogger _auditLogService;
        private readonly IOrderAttributeDao _orderAttributeDao;

        public UserOrderAttributeController(
            IAuditLogger auditLogService,
            IOrderAttributeDao orderAttributeDao)
        {
            _auditLogService = auditLogService;
            _orderAttributeDao = orderAttributeDao;
        }

        [HttpGet]
        [Route("GetList")]
        public async Task<PagingModel<OrderAttributeModel>> GetList(OrderAttributeParameterModel model)
        {
            var result = await _orderAttributeDao.GetPagedListAsync(model.MapNew(UserContext.StoreId)).ConfigureAwait(false);

            return result.MapNew(model.Page, model.Size);
        }

        [HttpGet]
        [Route("GetForAutocompleteByDescriptionPattern")]
        public Task<Dictionary<string, int>> GetForAutocompleteByDescriptionPattern(string pattern)
        {
            return _orderAttributeDao.GetForAutocompleteAsync(pattern.MapNew(UserContext.StoreId));
        }

        [HttpPost]
        [Route("Create")]
        [AjaxErrorHandle]
        public async Task Create(OrderAttributeModel model)
        {
            var newModel = model.MapNew(UserContext.StoreId);

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

            var newModel = oldModel.MapFrom(model, UserContext.StoreId);

            await _orderAttributeDao.UpdateAsync(newModel).ConfigureAwait(false);

            _auditLogService.LogUpdating(UserContext.UserId, UserContext.StoreId, oldModelClone, newModel);
        }

        [HttpPost]
        [Route("Delete")]
        [AjaxErrorHandle]
        public async Task Delete(int id)
        {
            var model = await _orderAttributeDao.GetAsync(id).ConfigureAwait(false);
            if (model.StoreId != UserContext.StoreId)
            {
                return;
            }

            var oldModelClone = model.Clone();

            model.IsDeleted = true;

            await _orderAttributeDao.UpdateAsync(model).ConfigureAwait(false);

            _auditLogService.LogUpdating(UserContext.UserId, UserContext.StoreId, oldModelClone, model);
        }
    }
}