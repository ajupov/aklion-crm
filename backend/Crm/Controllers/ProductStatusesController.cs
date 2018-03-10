using System.Collections.Generic;
using System.Threading.Tasks;
using Crm.Attributes;
using Crm.Dao.ProductStatus;
using Crm.Exceptions;
using Crm.Mappers.User.ProductStatus;
using Crm.Models;
using Crm.Models.User.ProductStatus;
using Microsoft.AspNetCore.Mvc;

namespace Crm.Controllers
{
    [AjaxErrorHandle]
    [Route("ProductStatuses")]
    public class ProductStatusesController : BaseController
    {
        private readonly IProductStatusDao _dao;

        public ProductStatusesController(IProductStatusDao dao)
        {
            _dao = dao;
        }

        [HttpGet]
        [Route("GetList")]
        public async Task<PagingModel<ProductStatusModel>> GetList(ProductStatusParameterModel model)
        {
            var result = await _dao.GetPagedListAsync(model.MapNew(UserContext.StoreId)).ConfigureAwait(false);
            return result.MapNew(model.Page, model.Size);
        }

        [HttpGet]
        [Route("GetSelect")]
        public async Task<Dictionary<string, int>> GetSelect()
        {
            var result = await _dao.GetSelectAsync(UserContext.StoreId.MapNew()).ConfigureAwait(false);
            return result.MapNew();
        }

        [HttpPost]
        [Route("Create")]
        public Task Create(ProductStatusModel model)
        {
            return _dao.CreateAsync(model.MapNew(UserContext.StoreId));
        }

        [HttpPost]
        [Route("Update")]
        public async Task Update(ProductStatusModel model)
        {
            var result = await _dao.GetAsync(model.Id).ConfigureAwait(false);
            if (result.StoreId != UserContext.StoreId)
            {
                throw new NotAccessChangingException();
            }

            await _dao.UpdateAsync(result.MapFrom(model, UserContext.StoreId)).ConfigureAwait(false);
        }

        [HttpPost]
        [Route("Delete")]
        public async Task Delete(int id)
        {
            var result = await _dao.GetAsync(id).ConfigureAwait(false);
            if (result.StoreId != UserContext.StoreId)
            {
                throw new NotAccessChangingException();
            }
            
            await _dao.DeleteAsync(id).ConfigureAwait(false);
        }
    }
}