using System.Threading.Tasks;
using Aklion.Crm.Attributes;
using Aklion.Crm.Dao.UserPermission;
using Aklion.Crm.Mappers.Administration.UserPermission;
using Aklion.Crm.Models;
using Aklion.Crm.Models.Administration.UserPermission;
using Microsoft.AspNetCore.Mvc;

namespace Aklion.Crm.Controllers.AdministrationControllers
{
    [Route("Administration/UserPermissions")]
    public class AdministrationUserPermissionController : BaseController
    {
        private readonly IAuditLogger _auditLogService;
        private readonly IUserPermissionDao _userPermissionDao;

        public AdministrationUserPermissionController(
            IUserPermissionDao userPermissionDao,
            IAuditLogger auditLogService)
        {
            _auditLogService = auditLogService;
            _userPermissionDao = userPermissionDao;
        }

        [HttpGet]
        [Route("GetList")]
        public async Task<PagingModel<UserPermissionModel>> GetList(UserPermissionParameterModel model)
        {
            var result = await _userPermissionDao.GetPagedListAsync(model.MapNew()).ConfigureAwait(false);

            return result.MapNew(model.Page, model.Size);
        }

        [HttpPost]
        [Route("Create")]
        [AjaxErrorHandle]
        public async Task Create(UserPermissionModel model)
        {
            var newModel = model.MapNew();

            newModel.Id = await _userPermissionDao.CreateAsync(newModel).ConfigureAwait(false);

            _auditLogService.LogInserting(UserContext.UserId, UserContext.StoreId, newModel);
        }

        [HttpPost]
        [Route("Update")]
        [AjaxErrorHandle]
        public async Task Update(UserPermissionModel model)
        {
            var oldModel = await _userPermissionDao.GetAsync(model.Id).ConfigureAwait(false);
            var oldModelClone = oldModel.Clone();

            var newModel = oldModel.MapFrom(model);

            await _userPermissionDao.UpdateAsync(newModel).ConfigureAwait(false);

            _auditLogService.LogUpdating(UserContext.UserId, UserContext.StoreId, oldModelClone, newModel);
        }

        [HttpPost]
        [Route("Delete")]
        [AjaxErrorHandle]
        public async Task Delete(int id)
        {
            var oldModel = await _userPermissionDao.GetAsync(id).ConfigureAwait(false);

            await _userPermissionDao.DeleteAsync(id).ConfigureAwait(false);

            _auditLogService.LogDeleting(UserContext.UserId, UserContext.StoreId, oldModel);
        }
    }
}