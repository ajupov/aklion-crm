using System.Collections.Generic;
using System.Threading.Tasks;
using Aklion.Crm.Attributes;
using Aklion.Crm.Business.AuditLog;
using Aklion.Crm.Dao.UserAttribute;
using Aklion.Crm.Mappers.Administration.UserAttribute;
using Aklion.Crm.Models;
using Aklion.Crm.Models.Administration.UserAttribute;
using Microsoft.AspNetCore.Mvc;

namespace Aklion.Crm.Controllers.Administration
{
    [Route("UserAttributes")]
    public class UserAttributeController : BaseController
    {
        private readonly IAuditLogService _auditLogService;
        private readonly IUserAttributeDao _userAttributeDao;

        public UserAttributeController(
            IAuditLogService auditLogService,
            IUserAttributeDao userAttributeDao)
        {
            _auditLogService = auditLogService;
            _userAttributeDao = userAttributeDao;
        }

        [HttpGet]
        [Route("GetList")]
        public async Task<PagingModel<UserAttributeModel>> GetList(UserAttributeParameterModel model)
        {
            var result = await _userAttributeDao.GetPagedListAsync(model.MapNew()).ConfigureAwait(false);

            return result.MapNew(model.Page, model.Size);
        }

        [HttpGet]
        [Route("GetForAutocompleteByDescriptionPattern")]
        public Task<Dictionary<string, int>> GetForAutocompleteByDescriptionPattern(string pattern, int storeId = 0)
        {
            return _userAttributeDao.GetForAutocompleteAsync(pattern.MapNew(storeId));
        }

        [HttpPost]
        [Route("Create")]
        [AjaxErrorHandle]
        public async Task Create(UserAttributeModel model)
        {
            var newModel = model.MapNew();

            newModel.Id = await _userAttributeDao.CreateAsync(newModel).ConfigureAwait(false);

            _auditLogService.LogInserting(UserContext.UserId, UserContext.StoreId, newModel);
        }

        [HttpPost]
        [Route("Update")]
        [AjaxErrorHandle]
        public async Task Update(UserAttributeModel model)
        {
            var oldModel = await _userAttributeDao.GetAsync(model.Id).ConfigureAwait(false);
            var oldModelClone = oldModel.Clone();

            var newModel = oldModel.MapFrom(model);

            await _userAttributeDao.UpdateAsync(newModel).ConfigureAwait(false);

            _auditLogService.LogUpdating(UserContext.UserId, UserContext.StoreId, oldModelClone, newModel);
        }

        [HttpPost]
        [Route("Delete")]
        [AjaxErrorHandle]
        public async Task Delete(int id)
        {
            var oldModel = await _userAttributeDao.GetAsync(id).ConfigureAwait(false);

            await _userAttributeDao.DeleteAsync(id).ConfigureAwait(false);

            _auditLogService.LogDeleting(UserContext.UserId, UserContext.StoreId, oldModel);
        }
    }
}