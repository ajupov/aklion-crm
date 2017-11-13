using System.Threading.Tasks;
using Aklion.Crm.Attributes;
using Aklion.Crm.Dao.UserPermission;
using Aklion.Crm.Enums;
using Aklion.Crm.Mappers.UserPermission;
using Aklion.Crm.Models;
using Aklion.Crm.Models.Administration.UserPermission;
using Microsoft.AspNetCore.Mvc;

namespace Aklion.Crm.Controllers.Administration
{
    [Route("Administration/UserPermissions")]
    public class AdministrationUserPermissionController : Controller
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
            var result = await _userPermissionDao.GetPagedList(model.Map()).ConfigureAwait(false);

            return result.Map(model.Page, model.Size);
        }

        [HttpPost]
        [Route("Create")]
        [AjaxErrorHandle]
        public async Task<bool> Create(UserPermissionModel model)
        {
            if (model.Permission == Permission.None)
            {
                return false;
            }

            var userPermission = model.Map();

            await _userPermissionDao.Create(userPermission).ConfigureAwait(false);

            return true;
        }

        [HttpPost]
        [Route("Update")]
        [AjaxErrorHandle]
        public async Task<bool> Update(UserPermissionModel model)
        {
            if (model.Permission == Permission.None)
            {
                return false;
            }

            var userPermission = await _userPermissionDao.Get(model.Id).ConfigureAwait(false);
            if (userPermission == null)
            {
                return false;
            }

            model.Map(userPermission);

            await _userPermissionDao.Update(userPermission).ConfigureAwait(false);

            return true;
        }

        [HttpPost]
        [Route("Delete")]
        [AjaxErrorHandle]
        public async Task<bool> Delete(int id)
        {
            await _userPermissionDao.Delete(id).ConfigureAwait(false);

            return true;
        }
    }
}