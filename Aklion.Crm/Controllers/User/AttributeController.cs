using System.Collections.Generic;
using System.Threading.Tasks;
using Aklion.Crm.Attributes;
using Aklion.Crm.Dao.Attribute;
using Aklion.Crm.Mappers;
using Aklion.Crm.Mappers.User.Attribute;
using Aklion.Crm.Models;
using Aklion.Crm.Models.User.Attribute;
using Microsoft.AspNetCore.Mvc;

namespace Aklion.Crm.Controllers.User
{
    [Route("Attributes")]
    public class AttributeController : BaseController
    {
        private readonly IAttributeDao _attributeDao;

        public AttributeController(IAttributeDao attributeDao)
        {
            _attributeDao = attributeDao;
        }

        [HttpGet]
        [Route("GetList")]
        public async Task<PagingModel<AttributeModel>> GetList(AttributeParameterModel model)
        {
            var result = await _attributeDao.GetPagedList(model.Map(UserContext.StoreId)).ConfigureAwait(false);

            return result.Map(UserContext.StoreId, model.Page, model.Size);
        }

        [HttpGet]
        [Route("GetForAutocompleteByNamePattern")]
        public async Task<List<AutocompleteModel>> GetForAutocompleteByNamePattern(string pattern)
        {
            var result = await _attributeDao.GetForAutocompleteByNamePattern(pattern, UserContext.StoreId).ConfigureAwait(false);

            return result.Map();
        }

        [HttpPost]
        [Route("Create")]
        [AjaxErrorHandle]
        public async Task<bool> Create(AttributeModel model)
        {
            var attribute = model.Map(UserContext.StoreId);

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

            model.Map(attribute, UserContext.StoreId);

            await _attributeDao.Update(attribute).ConfigureAwait(false);

            return true;
        }

        [HttpPost]
        [Route("Delete")]
        [AjaxErrorHandle]
        public async Task<bool> Delete(int id)
        {
            var attribute = await _attributeDao.Get(id).ConfigureAwait(false);
            if (attribute == null)
            {
                return false;
            }

            if (attribute. == null)
            {
                return false;
            }


            await _attributeDao.Delete(id).ConfigureAwait(false);

            return true;
        }
    }
}