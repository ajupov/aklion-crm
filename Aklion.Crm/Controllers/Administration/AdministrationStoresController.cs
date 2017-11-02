using System.Threading.Tasks;
using Aklion.Crm.Attributes;
using Aklion.Crm.Domain.Interfaces.User;
using Aklion.Crm.Mappers.User;
using Aklion.Crm.Models;
using Aklion.Crm.Models.Administration.Users;
using Microsoft.AspNetCore.Mvc;

namespace Aklion.Crm.Controllers.Administration
{
    [Route("Administration/Stores")]
    public class AdministrationStoresController : Controller
    {
        private readonly IUserDao _userDao;

        public AdministrationStoresController(IUserDao userDao)
        {
            _userDao = userDao;
        }

        [HttpGet]
        [Route("")]
        [Route("Index")]
        public IActionResult Index()
        {
            return View("~/Views/Administration/Stores/Index.cshtml");
        }

        [HttpGet]
        [Route("GetList")]
        public async Task<PagingModel<UserModel>> GetList(UserParameterModel model)
        {
            var result = await _userDao.GetPagedList(model.Map()).ConfigureAwait(false);

            return result.Map(model.Page, model.Size);
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
    }
}