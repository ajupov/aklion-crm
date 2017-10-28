using System.Threading.Tasks;
using Aklion.Crm.Domain.Interfaces.User;
using Aklion.Crm.Mappers.User;
using Aklion.Crm.Models;
using Aklion.Crm.Models.Administration.Users;
using Microsoft.AspNetCore.Mvc;

namespace Aklion.Crm.Controllers
{
    [Route("Administration/Users")]
    public class AdministrationUsersController : Controller
    {
        private readonly IUserDao _userDao;

        public AdministrationUsersController(IUserDao userDao)
        {
            _userDao = userDao;
        }

        [Route("")]
        [Route("Index")]
        public IActionResult Index()
        {
            return View();
        }

        [Route("GetList")]
        public async Task<PagingModel<UserModel>> GetList(UserParameterModel model)
        {
            var result = await _userDao.GetPagedList(model.Map()).ConfigureAwait(false);

            return result.Map(model.Page, model.Size);
        }
    }
}