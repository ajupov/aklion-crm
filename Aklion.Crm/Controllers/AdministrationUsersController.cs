using System.Threading.Tasks;
using Aklion.Crm.Domain.Interfaces.User;
using Aklion.Crm.Mappers.User;
using Aklion.Crm.Models;
using Aklion.Crm.Models.Administration.Users;
using Aklion.Crm.Models.Client;
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

        [HttpGet]
        [Route("")]
        [Route("Index")]
        public IActionResult Index()
        {
            return View();
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
        public async Task Update(UserModel model, string Operation)
        {
        }
    }
}