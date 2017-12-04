using System.Threading.Tasks;
using Aklion.Crm.Attributes;
using Aklion.Crm.Dao.Post;
using Aklion.Crm.Dao.UserPost;
using Aklion.Crm.Mappers.Administration.UserPost;
using Aklion.Crm.Models;
using Aklion.Crm.Models.Administration.UserPost;
using Microsoft.AspNetCore.Mvc;

namespace Aklion.Crm.Controllers.Administration
{
    [Route("Administration/UserPosts")]
    public class AdministrationUserPostController : BaseController
    {
        private readonly IUserPostDao _userPostDao;
        private readonly IPostDao _postDao;

        public AdministrationUserPostController(IUserPostDao userPostDao, IPostDao postDao)
        {
            _userPostDao = userPostDao;
            _postDao = postDao;
        }

        [HttpGet]
        [Route("GetList")]
        public async Task<PagingModel<UserPostModel>> GetList(UserPostParameterModel model)
        {
            var result = await _userPostDao.GetPagedList(model.Map()).ConfigureAwait(false);

            return result.Map(model.Page, model.Size);
        }

        [HttpPost]
        [Route("Create")]
        [AjaxErrorHandle]
        public async Task<bool> Create(UserPostModel model)
        {
            var post = await _postDao.Get(model.PostId).ConfigureAwait(false);
            if (post.StoreId != model.StoreId)
            {
                return false;
            }

            var userPost = model.Map();

            await _userPostDao.Create(userPost).ConfigureAwait(false);

            return true;
        }

        [HttpPost]
        [Route("Update")]
        [AjaxErrorHandle]
        public async Task<bool> Update(UserPostModel model)
        {
            var post = await _postDao.Get(model.PostId).ConfigureAwait(false);
            if (post.StoreId != model.StoreId)
            {
                return false;
            }

            var userPost = await _userPostDao.Get(model.Id).ConfigureAwait(false);
            if (userPost == null)
            {
                return false;
            }

            model.Map(userPost);

            await _userPostDao.Update(userPost).ConfigureAwait(false);

            return true;
        }

        [HttpPost]
        [Route("Delete")]
        [AjaxErrorHandle]
        public async Task<bool> Delete(int id)
        {
            await _userPostDao.Delete(id).ConfigureAwait(false);

            return true;
        }
    }
}