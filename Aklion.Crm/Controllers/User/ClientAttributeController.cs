using System.Collections.Generic;
using System.Threading.Tasks;
using Aklion.Crm.Attributes;
using Aklion.Crm.Business.AuditLog;
using Aklion.Crm.Dao.ClientAttribute;
using Aklion.Crm.Mappers.User.ClientAttribute;
using Aklion.Crm.Models;
using Aklion.Crm.Models.User.ClientAttribute;
using Microsoft.AspNetCore.Mvc;

namespace Aklion.Crm.Controllers.User
{
    [Route("ClientAttributes")]
    public class ClientAttributeController : BaseController
    {
        private readonly IAuditLogService _auditLogService;
        private readonly IClientAttributeDao _clientAttributeDao;

        public ClientAttributeController(
            IAuditLogService auditLogService,
            IClientAttributeDao clientAttributeDao)
        {
            _auditLogService = auditLogService;
            _clientAttributeDao = clientAttributeDao;
        }

        [HttpGet]
        [Route("GetList")]
        public async Task<PagingModel<ClientAttributeModel>> GetList(ClientAttributeParameterModel model)
        {
            var result = await _clientAttributeDao.GetPagedListAsync(model.MapNew(UserContext.StoreId)).ConfigureAwait(false);

            return result.MapNew(model.Page, model.Size);
        }

        [HttpGet]
        [Route("GetForAutocompleteByDescriptionPattern")]
        public Task<Dictionary<string, int>> GetForAutocompleteByDescriptionPattern(string pattern, int storeId = 0)
        {
            return _clientAttributeDao.GetForAutocompleteAsync(pattern.MapNew(storeId));
        }

        [HttpPost]
        [Route("Create")]
        [AjaxErrorHandle]
        public async Task Create(ClientAttributeModel model)
        {
            var newModel = model.MapNew(UserContext.StoreId);

            newModel.Id = await _clientAttributeDao.CreateAsync(newModel).ConfigureAwait(false);

            _auditLogService.LogInserting(UserContext.UserId, UserContext.StoreId, newModel);
        }

        [HttpPost]
        [Route("Update")]
        [AjaxErrorHandle]
        public async Task Update(ClientAttributeModel model)
        {
            var oldModel = await _clientAttributeDao.GetAsync(model.Id).ConfigureAwait(false);
            var oldModelClone = oldModel.Clone();

            var newModel = oldModel.MapFrom(model, UserContext.StoreId);

            await _clientAttributeDao.UpdateAsync(newModel).ConfigureAwait(false);

            _auditLogService.LogUpdating(UserContext.UserId, UserContext.StoreId, oldModelClone, newModel);
        }

        [HttpPost]
        [Route("Delete")]
        [AjaxErrorHandle]
        public async Task Delete(int id)
        {
            var model = await _clientAttributeDao.GetAsync(id).ConfigureAwait(false);
            if (model.StoreId != UserContext.StoreId)
            {
                return;
            }

            var oldModelClone = model.Clone();

            model.IsDeleted = true;

            await _clientAttributeDao.UpdateAsync(model).ConfigureAwait(false);

            _auditLogService.LogUpdating(UserContext.UserId, UserContext.StoreId, oldModelClone, model);
        }
    }
}