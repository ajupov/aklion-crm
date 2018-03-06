using System.Collections.Generic;
using System.Threading.Tasks;
using Aklion.Crm.Attributes;
using Aklion.Crm.Dao.ProductImageKey;
using Aklion.Crm.Exceptions;
using Aklion.Crm.Mappers.User.ProductImageKey;
using Aklion.Crm.Models;
using Aklion.Crm.Models.User.ProductImageKey;
using Microsoft.AspNetCore.Mvc;

namespace Aklion.Crm.Controllers.Users.Product
{
    [AjaxErrorHandle]
    [Route("ProductImageKeys")]
    public class UserProductImageKeyController : BaseController
    {
        private readonly IProductImageKeyDao _dao;

        public UserProductImageKeyController(IProductImageKeyDao dao)
        {
            _dao = dao;
        }

        [HttpGet]
        public async Task<PagingModel<ProductImageKeyModel>> GetList(ProductImageKeyParameterModel model)
        {
            var result = await _dao.GetPagedListAsync(model.MapNew(UserContext.StoreId)).ConfigureAwait(false);
            return result.MapNew(model.Page, model.Size);
        }


        [HttpGet]
        public async Task<Dictionary<string, int>> GetSelect()
        {
            var result = await _dao.GetForSelectAsync(UserContext.StoreId.MapNew()).ConfigureAwait(false);
            return result.MapNew();
        }

        [HttpPost]
        public Task Create(ProductImageKeyModel model)
        {
            return _dao.CreateAsync(model.MapNew(UserContext.StoreId));
        }

        [HttpPost]
        public async Task Update(ProductImageKeyModel model)
        {
            var result = await _dao.GetAsync(model.Id).ConfigureAwait(false);
            if (result.StoreId != UserContext.StoreId)
            {
                throw new NotAccessChangingException();
            }

            await _dao.UpdateAsync(result.MapFrom(model, UserContext.StoreId)).ConfigureAwait(false);
        }

        [HttpPost]
        public async Task Delete(int id)
        {
            var result = await _dao.GetAsync(id).ConfigureAwait(false);
            if (result.StoreId != UserContext.StoreId)
            {
                throw new NotAccessChangingException();
            }

            result.IsDeleted = true;
            await _dao.UpdateAsync(result).ConfigureAwait(false);
        }
    }
}