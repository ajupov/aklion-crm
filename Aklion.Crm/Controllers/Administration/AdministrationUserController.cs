using System.Collections.Generic;
using System.Threading.Tasks;
using Aklion.Crm.Attributes;
using Aklion.Crm.Dao.User;
using Aklion.Crm.Mappers;
using Aklion.Crm.Mappers.User;
using Aklion.Crm.Models;
using Aklion.Crm.Models.Administration.User;
using Microsoft.AspNetCore.Mvc;

namespace Aklion.Crm.Controllers.Administration
{
    [Route("Administration/Users")]
    public class AdministrationUserController : Controller
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
            var result = await _userDao.GetPagedList(model.Map()).ConfigureAwait(false);

            return result.Map(model.Page, model.Size);
        }

        [HttpGet]
        [Route("GetForAutocompleteByLoginPattern")]
        public async Task<List<AutocompleteModel>> GetForAutocompleteByLoginPattern(string loginPattern)
        {
            var result = await _userDao.GetForAutocompleteByLoginPattern(loginPattern).ConfigureAwait(false);

            return result.Map();
        }

        [HttpPost]
        [Route("Update")]
        [AjaxErrorHandle]
        public async Task Update(UserModel model)
        {
            var user = await _userDao.Get(model.Id).ConfigureAwait(false);
            if (user == null)
            {
                return;
            }

            model.Map(user);

            await _userDao.Update(user).ConfigureAwait(false);
        }

        [HttpPost]
        [Route("Delete")]
        [AjaxErrorHandle]
        public async Task Delete(int id)
        {
            await _userDao.Delete(id).ConfigureAwait(false);
        }
    }
}