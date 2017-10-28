//using System.Threading.Tasks;
//using Aklion.Crm.Domain.Interfaces.User;
//using Aklion.Crm.Mappers.User;
//using Aklion.Crm.Models;
//using Aklion.Crm.Models.Users;
//using Microsoft.AspNetCore.Mvc;

//namespace Aklion.Crm.Controllers
//{
//    public class UsersController : Controller
//    {
//        private readonly IUserDao _userDao;

//        public UsersController(IUserDao userDao)
//        {
//            _userDao = userDao;
//        }

//        [HttpGet]
//        public PartialViewResult GetTable()
//        {
//            return PartialView("Templates/_Table", typeof(User));
//        }

//        [HttpGet]
//        public async Task<PagingModel<User>> GetList(UserExtendedModel model)
//        {
//            var result = await _userDao.GetPagedList(model).ConfigureAwait(false);
//            return null;
//            //  return result.Map(model.Page, model.Size);
//        }

//        [HttpGet]
//        public async Task<User> Get(int id)
//        {
//            var result = await _userDao.Get(id).ConfigureAwait(false);
//            return result.Map();
//        }

//        [HttpPost]
//        public async Task Insert(User model)
//        {
//            await _userDao.Insert(model.Map()).ConfigureAwait(false);
//        }

//        [HttpPost]
//        public async Task Update(User model)
//        {
//            await _userDao.Update(model.Map()).ConfigureAwait(false);
//        }

//        [HttpPost]
//        public async Task Delete(int id)
//        {
//            await _userDao.Delete(id).ConfigureAwait(false);
//        }
//    }
//}