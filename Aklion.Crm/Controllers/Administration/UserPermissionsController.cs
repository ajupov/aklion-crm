using System.Threading.Tasks;
using Aklion.Crm.Attributes;
using Aklion.Crm.Dao.UserPermission;
using Aklion.Crm.Mappers.Administration.UserPermission;
using Aklion.Crm.Models;
using Aklion.Crm.Models.Administration.UserPermission;
using Microsoft.AspNetCore.Mvc;

namespace Aklion.Crm.Controllers.Administration
{
    [AjaxErrorHandle]
    public class UserPermissionsController : BaseController
    {
        private readonly IUserPermissionDao _dao;

        public UserPermissionsController(IUserPermissionDao dao)
        {
            _dao = dao;
        }

        [HttpGet]
        public async Task<PagingModel<UserPermissionModel>> GetList(UserPermissionParameterModel model)
        {
            var result = await _dao.GetPagedListAsync(model.MapNew()).ConfigureAwait(false);
            return result.MapNew(model.Page, model.Size);
        }

        [HttpPost]
        public Task Create(UserPermissionModel model)
        {
            return _dao.CreateAsync(model.MapNew());
        }

        [HttpPost]
        public async Task Update(UserPermissionModel model)
        {
            var result = await _dao.GetAsync(model.Id).ConfigureAwait(false);
            await _dao.UpdateAsync(result.MapFrom(model)).ConfigureAwait(false);
        }

        [HttpPost]
        public Task Delete(int id)
        {
            return _dao.DeleteAsync(id);
        }
    }
}