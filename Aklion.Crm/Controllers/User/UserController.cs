using System.Collections.Generic;
using System.Threading.Tasks;
using Aklion.Crm.Attributes;
using Aklion.Crm.Business.AuditLog;
using Aklion.Crm.Dao.User;
using Aklion.Crm.Mappers.User.User;
using Aklion.Crm.Models;
using Aklion.Crm.Models.User.User;
using Microsoft.AspNetCore.Mvc;

namespace Aklion.Crm.Controllers.User
{
    [Route("Users")]
    public class UserController : BaseController
    {
        private readonly IAuditLogService _auditLogService;
        private readonly IUserDao _userDao;

        public UserController(
            IAuditLogService auditLogService,
            IUserDao userDao)
        {
            _auditLogService = auditLogService;
            _userDao = userDao;
        }

        [HttpGet]
        [Route("")]
        public IActionResult Index()
        {
            return View("~/Views/User/User/Index.cshtml");
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
            var model = await _userDao.GetAsync(id).ConfigureAwait(false);

            var oldModelClone = model.Clone();

            model.IsDeleted = true;

            await _userDao.UpdateAsync(model).ConfigureAwait(false);

            _auditLogService.LogUpdating(UserContext.UserId, UserContext.StoreId, oldModelClone, model);
        }
    }
}