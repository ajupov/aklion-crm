using System.Collections.Generic;
using System.Threading.Tasks;
using Aklion.Crm.Attributes;
using Aklion.Crm.Dao.Tag;
using Aklion.Crm.Mappers;
using Aklion.Crm.Mappers.Administration.Tag;
using Aklion.Crm.Models;
using Aklion.Crm.Models.Administration.Tag;
using Microsoft.AspNetCore.Mvc;

namespace Aklion.Crm.Controllers.Administration
{
    [Route("Administration/Tags")]
    public class AdministrationTagController : Controller
    {
        private readonly ITagDao _tagDao;

        public AdministrationTagController(ITagDao tagDao)
        {
            _tagDao = tagDao;
        }

        [HttpGet]
        [Route("GetList")]
        public async Task<PagingModel<TagModel>> GetList(TagParameterModel model)
        {
            var result = await _tagDao.GetPagedList(model.Map()).ConfigureAwait(false);

            return result.Map(model.Page, model.Size);
        }

        [HttpGet]
        [Route("GetForAutocompleteByNamePattern")]
        public async Task<List<AutocompleteModel>> GetForAutocompleteByNamePattern(string pattern, int storeId = 0)
        {
            var result = await _tagDao.GetForAutocompleteByNamePattern(pattern, storeId).ConfigureAwait(false);

            return result.Map();
        }

        [HttpPost]
        [Route("Create")]
        [AjaxErrorHandle]
        public async Task<bool> Create(TagModel model)
        {
            var tag = model.Map();

            await _tagDao.Create(tag).ConfigureAwait(false);

            return true;
        }

        [HttpPost]
        [Route("Update")]
        [AjaxErrorHandle]
        public async Task<bool> Update(TagModel model)
        {
            var tag = await _tagDao.Get(model.Id).ConfigureAwait(false);
            if (tag == null)
            {
                return false;
            }

            model.Map(tag);

            await _tagDao.Update(tag).ConfigureAwait(false);

            return true;
        }

        [HttpPost]
        [Route("Delete")]
        [AjaxErrorHandle]
        public async Task<bool> Delete(int id)
        {
            await _tagDao.Delete(id).ConfigureAwait(false);

            return true;
        }
    }
}