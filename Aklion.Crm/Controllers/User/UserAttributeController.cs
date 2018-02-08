using System.Collections.Generic;
using System.Threading.Tasks;
using Aklion.Crm.Attributes;
using Aklion.Crm.Business.AuditLog;
using Aklion.Crm.Dao.UserAttribute;
using Aklion.Crm.Mappers.User.UserAttribute;
using Aklion.Crm.Models;
using Aklion.Crm.Models.User.UserAttribute;
using Microsoft.AspNetCore.Mvc;

namespace Aklion.Crm.Controllers.User
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
            var result = await _userAttributeDao.GetPagedListAsync(model.MapNew(UserContext.StoreId)).ConfigureAwait(false);

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
            var newModel = model.MapNew(UserContext.StoreId);

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

            var newModel = oldModel.MapFrom(model, UserContext.StoreId);

            await _userAttributeDao.UpdateAsync(newModel).ConfigureAwait(false);

            _auditLogService.LogUpdating(UserContext.UserId, UserContext.StoreId, oldModelClone, newModel);
        }

        [HttpPost]
        [Route("Delete")]
        [AjaxErrorHandle]
        public async Task Delete(int id)
        {
            var model = await _userAttributeDao.GetAsync(id).ConfigureAwait(false);
            if (model.StoreId != UserContext.StoreId)
            {
                return;
            }

            var oldModelClone = model.Clone();

            model.IsDeleted = true;

            await _userAttributeDao.UpdateAsync(model).ConfigureAwait(false);

            _auditLogService.LogUpdating(UserContext.UserId, UserContext.StoreId, oldModelClone, model);
        }
    }
}