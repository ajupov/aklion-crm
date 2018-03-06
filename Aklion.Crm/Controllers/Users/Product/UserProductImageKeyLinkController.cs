using System.Threading.Tasks;
using Aklion.Crm.Attributes;
using Aklion.Crm.Dao.ProductImageKeyLink;
using Aklion.Crm.Exceptions;
using Aklion.Crm.Mappers.User.ProductImageKeyLink;
using Aklion.Crm.Models;
using Aklion.Crm.Models.User.ProductImageKeyLink;
using Aklion.Infrastructure.FileFormat;
using Microsoft.AspNetCore.Mvc;

namespace Aklion.Crm.Controllers.Users.Product
{
    [AjaxErrorHandle]
    [Route("ProductImageKeyLinks")]
    public class UserProductImageKeyLinkController : BaseController
    {
        private readonly IProductImageKeyLinkDao _dao;

        public UserProductImageKeyLinkController(IProductImageKeyLinkDao dao)
        {
            _dao = dao;
        }

        [HttpGet]
        public async Task<PagingModel<ProductImageKeyLinkModel>> GetList(ProductImageKeyLinkParameterModel model)
        {
            var result = await _dao.GetPagedListAsync(model.MapNew(UserContext.StoreId)).ConfigureAwait(false);
            return result.MapNew(model.Page, model.Size);
        }

        [HttpPost]
        public Task Create(ProductImageKeyLinkModel model)
        {
            return _dao.CreateAsync(model.MapNew(UserContext.StoreId));
        }

        [HttpPost]
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