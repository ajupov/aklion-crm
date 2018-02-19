using System.Collections.Generic;
using System.Threading.Tasks;
using Aklion.Crm.Attributes;
using Aklion.Crm.Dao.User;
using Aklion.Crm.Mappers.Administration.User;
using Aklion.Crm.Models;
using Aklion.Crm.Models.Administration.User;
using Microsoft.AspNetCore.Mvc;

namespace Aklion.Crm.Controllers.AdministrationControllers
{
    [Route("Administration/Users")]
    public class AdministrationUserController : BaseController
    {
        private readonly IAuditLogger _auditLogService;
        private readonly IUserDao _userDao;

        public AdministrationUserController(
            IAuditLogger auditLogService,
            IUserDao userDao)
        {
            _auditLogService = auditLogService;
            _userDao = userDao;
        }

        [HttpGet]
        [Route("")]
        public IActionResult Index()
        {
            return View("~/Views/Administration/User/Index.cshtml");
        }

        [HttpGet]
        [Route("GetList")]
        public async Task<PagingModel<UserModel>> GetList(UserParameterModel model)
        {
            var result = await _userDao.GetPagedListAsync(model.MapNew()).ConfigureAwait(false);

            return result.MapNew(model.Page, model.Size);
        }

        [HttpGet]
        [Route("GetForAutocompleteByLoginPattern")]
        public Task<Dictionary<string, int>> GetForAutocompleteByLoginPattern(string pattern)
        {
            return _userDao.GetForAutocompleteAsync(pattern.MapNew());
        }

        [HttpPost]
        [Route("Update")]
        [AjaxErrorHandle]
        public async Task Update(UserModel model)
        {
            var oldModel = await _userDao.GetAsync(model.Id).ConfigureAwait(false);
            var oldModelClone = oldModel.Clone();

            var newModel = oldModel.MapFrom(model);

            await _userDao.UpdateAsync(newModel).ConfigureAwait(false);

            _auditLogService.LogUpdating(UserContext.UserId, UserContext.StoreId, oldModelClone, newModel);
        }

        [HttpPost]
        [Route("Delete")]
        [AjaxErrorHandle]
        public async Task Delete(int id)
        {
            var oldModel = await _userDao.GetAsync(id).ConfigureAwait(false);

            await _userDao.DeleteAsync(id).ConfigureAwait(false);

            _auditLogService.LogDeleting(UserContext.UserId, UserContext.StoreId, oldModel);
        }
    }
}