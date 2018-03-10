using System.Threading.Tasks;
using Crm.Attributes;
using Crm.Dao.ProductImageKeyLink;
using Crm.Exceptions;
using Crm.Mappers.User.ProductImageKeyLink;
using Crm.Models;
using Crm.Models.User.ProductImageKeyLink;
using Infrastructure.FileFormat;
using Microsoft.AspNetCore.Mvc;

namespace Crm.Controllers
{
    [AjaxErrorHandle]
    [Route("ProductImageKeyLinks")]
    public class ProductImageKeyLinksController : BaseController
    {
        private readonly IProductImageKeyLinkDao _dao;

        public ProductImageKeyLinksController(IProductImageKeyLinkDao dao)
        {
            _dao = dao;
        }

        [HttpGet]
        [Route("GetList")]
        public async Task<PagingModel<ProductImageKeyLinkModel>> GetList(ProductImageKeyLinkParameterModel model)
        {
            var result = await _dao.GetPagedListAsync(model.MapNew(UserContext.StoreId)).ConfigureAwait(false);
            return result.MapNew(model.Page, model.Size);
        }

        [HttpPost]
        [Route("Create")]
        public Task Create(ProductImageKeyLinkModel model)
        {
            return _dao.CreateAsync(model.MapNew(UserContext.StoreId));
        }

        [HttpPost]
        [Route("Update")]
        public async Task Update(ProductImageKeyLinkModel model)
        {
            var result = await _dao.GetAsync(model.Id).ConfigureAwait(false);
            if (result.StoreId != UserContext.StoreId)
            {
                throw new NotAccessChangingException();
            }

            await _dao.UpdateAsync(result.MapFrom(model, UserContext.StoreId)).ConfigureAwait(false);
        }

        [HttpPost]
        [Route("LoadImage")]
        public async Task LoadImage(ProductImageKeyLinkLoadImageModel model)
        {
            var result = await _dao.GetAsync(model.Id).ConfigureAwait(false);
            if (result.StoreId != UserContext.StoreId)
            {
                throw new NotAccessChangingException();
            }

            if (!model.ImageFile.FileName.IsImage())
            {
                return;
            }

            await _dao.SetImageAsync(result.Id, model.ImageFile.OpenReadStream()).ConfigureAwait(false);
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

            result.IsDeleted = true;
            await _dao.UpdateAsync(result).ConfigureAwait(false);
        }
    }
}