using System.Threading.Tasks;
using Aklion.Crm.Attributes;
using Aklion.Crm.Dao.UserPermission;
using Aklion.Crm.Enums;
using Aklion.Crm.Mappers.Administration.UserPermission;
using Aklion.Crm.Models;
using Aklion.Crm.Models.Administration.UserPermission;
using Microsoft.AspNetCore.Mvc;

namespace Aklion.Crm.Controllers.Administration
{
    [Route("Administration/UserPermissions")]
    public class AdministrationUserPermissionController : BaseController
    {
        private readonly IUserPermissionDao _userPermissionDao;

        public AdministrationUserPermissionController(IUserPermissionDao userPermissionDao)
        {
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
        public Task Create(UserPermissionModel model)
        {
            if (model.Permission == Permission.None)
            {
                return Task.CompletedTask;
            }

            return _userPermissionDao.CreateAsync(model.MapNew());
        }

        [HttpPost]
        [Route("Update")]
        [AjaxErrorHandle]
        public async Task Update(UserPermissionModel model)
        {
            if (model.Permission == Permission.None)
            {
                return;
            }

            var userPermission = await _userPermissionDao.GetAsync(model.Id).ConfigureAwait(false);

            await _userPermissionDao.UpdateAsync(userPermission.MapFrom(model)).ConfigureAwait(false);
        }

        [HttpPost]
        [Route("Delete")]
        [AjaxErrorHandle]
        public Task Delete(int id)
        {
            return _userPermissionDao.DeleteAsync(id);
        }
    }
}