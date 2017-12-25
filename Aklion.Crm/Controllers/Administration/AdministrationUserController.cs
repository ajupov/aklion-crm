using System.Collections.Generic;
using System.Threading.Tasks;
using Aklion.Crm.Attributes;
using Aklion.Crm.Dao.User;
using Aklion.Crm.Mappers.Administration.User;
using Aklion.Crm.Models;
using Aklion.Crm.Models.Administration.User;
using Microsoft.AspNetCore.Mvc;

namespace Aklion.Crm.Controllers.Administration
{
    [Route("Administration/Users")]
    public class AdministrationUserController : BaseController
    {
        private readonly IUserDao _userDao;

        public AdministrationUserController(IUserDao userDao)
        {
            _userDao = userDao;
        }

        [HttpGet]
        [Route("")]
        [Route("Index")]
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
        public async Task<bool> Update(UserModel model)
        {
            var user = await _userDao.GetAsync(model.Id).ConfigureAwait(false);

            return await _userDao.UpdateAsync(user.MapFrom(model)).ConfigureAwait(false);
        }

        [HttpPost]
        [Route("Delete")]
        [AjaxErrorHandle]
        public Task<bool> Delete(int id)
        {
            return _userDao.DeleteAsync(id);
        }
    }
}