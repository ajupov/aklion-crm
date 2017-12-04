using System.Collections.Generic;
using System.Threading.Tasks;
using Aklion.Crm.Attributes;
using Aklion.Crm.Dao.Attribute;
using Aklion.Crm.Mappers;
using Aklion.Crm.Mappers.Administration.Attribute;
using Aklion.Crm.Models;
using Aklion.Crm.Models.Administration.Attribute;
using Microsoft.AspNetCore.Mvc;

namespace Aklion.Crm.Controllers.Administration
{
    [Route("Administration/Attributes")]
    public class AdministrationAttributeController : BaseController
    {
        private readonly IAttributeDao _attributeDao;

        public AdministrationAttributeController(IAttributeDao attributeDao)
        {
            _attributeDao = attributeDao;
        }

        [HttpGet]
        [Route("GetList")]
        public async Task<PagingModel<AttributeModel>> GetList(AttributeParameterModel model)
        {
            var result = await _attributeDao.GetPagedList(model.Map()).ConfigureAwait(false);

            return result.Map(model.Page, model.Size);
        }

        [HttpGet]
        [Route("GetForAutocompleteByNamePattern")]
        public async Task<List<AutocompleteModel>> GetForAutocompleteByNamePattern(string pattern, int storeId = 0)
        {
            var result = await _attributeDao.GetForAutocompleteByNamePattern(pattern, storeId).ConfigureAwait(false);

            return result.Map();
        }

        [HttpPost]
        [Route("Create")]
        [AjaxErrorHandle]
        public async Task<bool> Create(AttributeModel model)
        {
            var attribute = model.Map();

            await _attributeDao.Create(attribute).ConfigureAwait(false);

            return true;
        }

        [HttpPost]
        [Route("Update")]
        [AjaxErrorHandle]
        public async Task<bool> Update(AttributeModel model)
        {
            var attribute = await _attributeDao.Get(model.Id).ConfigureAwait(false);
            if (attribute == null)
            {
                return false;
            }

            model.Map(attribute);

            await _attributeDao.Update(attribute).ConfigureAwait(false);

            return true;
        }

        [HttpPost]
        [Route("Delete")]
        [AjaxErrorHandle]
        public async Task<bool> Delete(int id)
        {
            await _attributeDao.Delete(id).ConfigureAwait(false);

            return true;
        }
    }
}