using System.Collections.Generic;
using System.Threading.Tasks;
using Aklion.Crm.Attributes;
using Aklion.Crm.Dao.Post;
using Aklion.Crm.Mappers;
using Aklion.Crm.Mappers.Administration.Post;
using Aklion.Crm.Models;
using Aklion.Crm.Models.Administration.Post;
using Microsoft.AspNetCore.Mvc;

namespace Aklion.Crm.Controllers.Administration
{
    [Route("Administration/Posts")]
    public class AdministrationPostController : Controller
    {
        private readonly IPostDao _postDao;

        public AdministrationPostController(IPostDao postDao)
        {
            _postDao = postDao;
        }

        [HttpGet]
        [Route("")]
        [Route("Index")]
        public IActionResult Index()
        {
            return View("~/Views/Administration/Post/Index.cshtml");
        }

        [HttpGet]
        [Route("GetList")]
        public async Task<PagingModel<PostModel>> GetList(PostParameterModel model)
        {
            var result = await _postDao.GetPagedList(model.Map()).ConfigureAwait(false);

            return result.Map(model.Page, model.Size);
        }

        [HttpGet]
        [Route("GetForAutocompleteByNamePattern")]
        public async Task<List<AutocompleteModel>> GetForAutocompleteByNamePattern(string pattern, int storeId = 0)
        {
            var result = await _postDao.GetForAutocompleteByNamePattern(pattern, storeId).ConfigureAwait(false);

            return result.Map();
        }

        [HttpPost]
        [Route("Create")]
        [AjaxErrorHandle]
        public async Task<bool> Create(PostModel model)
        {
            var post = model.Map();

            await _postDao.Create(post).ConfigureAwait(false);

            return true;
        }

        [HttpPost]
        [Route("Update")]
        [AjaxErrorHandle]
        public async Task<bool> Update(PostModel model)
        {
            var post = await _postDao.Get(model.Id).ConfigureAwait(false);
            if (post == null)
            {
                return false;
            }

            model.Map(post);

            await _postDao.Update(post).ConfigureAwait(false);

            return true;
        }

        [HttpPost]
        [Route("Delete")]
        [AjaxErrorHandle]
        public async Task<bool> Delete(int id)
        {
            await _postDao.Delete(id).ConfigureAwait(false);

            return true;
        }
    }
}