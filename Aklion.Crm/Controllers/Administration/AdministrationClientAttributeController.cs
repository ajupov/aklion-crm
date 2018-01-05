using System.Collections.Generic;
using System.Threading.Tasks;
using Aklion.Crm.Attributes;
using Aklion.Crm.Business.AuditLog;
using Aklion.Crm.Dao.ClientAttribute;
using Aklion.Crm.Mappers.Administration.ClientAttribute;
using Aklion.Crm.Models;
using Aklion.Crm.Models.Administration.ClientAttribute;
using Microsoft.AspNetCore.Mvc;

namespace Aklion.Crm.Controllers.Administration
{
    [Route("Administration/ClientAttributes")]
    public class AdministrationClientAttributeController : BaseController
    {
        private readonly IClientAttributeDao _clientAttributeDao;
        private readonly IAuditLogService _auditLogService;

        public AdministrationClientAttributeController(IClientAttributeDao clientAttributeDao, IAuditLogService auditLogService)
        {
            _clientAttributeDao = clientAttributeDao;
            _auditLogService = auditLogService;
        }

        [HttpGet]
        [Route("GetList")]
        public async Task<PagingModel<ClientAttributeModel>> GetList(ClientAttributeParameterModel model)
        {
            var result = await _clientAttributeDao.GetPagedListAsync(model.MapNew()).ConfigureAwait(false);

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
            var newModel = model.MapNew();

            newModel.Id = await _clientAttributeDao.CreateAsync(newModel).ConfigureAwait(false);

            _auditLogService.LogInserting(UserContext.UserId, UserContext.StoreId, newModel);
        }

        [HttpPost]
        [Route("Update")]
        [AjaxErrorHandle]
        public async Task Update(ClientAttributeModel model)
        {
            var oldModel = await _clientAttributeDao.GetAsync(model.Id).ConfigureAwait(false);
            var newModel = oldModel.MapFrom(model);

            await _clientAttributeDao.UpdateAsync(newModel).ConfigureAwait(false);

            _auditLogService.LogUpdating(UserContext.UserId, UserContext.StoreId, oldModel, newModel);
        }

        [HttpPost]
        [Route("Delete")]
        [AjaxErrorHandle]
        public async Task Delete(int id)
        {
            var oldModel = await _clientAttributeDao.GetAsync(id).ConfigureAwait(false);

            await _clientAttributeDao.DeleteAsync(id).ConfigureAwait(false);

            _auditLogService.LogDeleting(UserContext.UserId, UserContext.StoreId, oldModel);
        }
    }
}