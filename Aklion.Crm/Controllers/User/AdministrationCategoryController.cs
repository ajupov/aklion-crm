using System.Collections.Generic;
using System.Threading.Tasks;
using Aklion.Crm.Attributes;
using Aklion.Crm.Dao.Category;
using Aklion.Crm.Mappers;
using Aklion.Crm.Mappers.Administration.Category;
using Aklion.Crm.Models;
using Aklion.Crm.Models.Administration.Category;
using Microsoft.AspNetCore.Mvc;

namespace Aklion.Crm.Controllers.Administration
{
    [Route("Administration/Categories")]
    public class AdministrationCategoryController : BaseController
    {
        private readonly ICategoryDao _categoryDao;

        public AdministrationCategoryController(ICategoryDao categoryDao)
        {
            _categoryDao = categoryDao;
        }

        [HttpGet]
        [Route("GetList")]
        public async Task<PagingModel<CategoryModel>> GetList(CategoryParameterModel model)
        {
            var result = await _categoryDao.GetPagedList(model.Map()).ConfigureAwait(false);

            return result.Map(model.Page, model.Size);
        }

        [HttpGet]
        [Route("GetForAutocompleteByNamePattern")]
        public async Task<List<AutocompleteModel>> GetForAutocompleteByNamePattern(string pattern, int storeId = 0)
        {
            var result = await _categoryDao.GetForAutocompleteByNamePattern(pattern, storeId).ConfigureAwait(false);

            return result.Map();
        }

        [HttpPost]
        [Route("Create")]
        [AjaxErrorHandle]
        public async Task<bool> Create(CategoryModel model)
        {
            var category = model.Map();

            await _categoryDao.Create(category).ConfigureAwait(false);

            return true;
        }

        [HttpPost]
        [Route("Update")]
        [AjaxErrorHandle]
        public async Task<bool> Update(CategoryModel model)
        {
            var category = await _categoryDao.Get(model.Id).ConfigureAwait(false);
            if (category == null)
            {
                return false;
            }

            model.Map(category);

            await _categoryDao.Update(category).ConfigureAwait(false);

            return true;
        }

        [HttpPost]
        [Route("Delete")]
        [AjaxErrorHandle]
        public async Task<bool> Delete(int id)
        {
            await _categoryDao.Delete(id).ConfigureAwait(false);

            return true;
        }
    }
}