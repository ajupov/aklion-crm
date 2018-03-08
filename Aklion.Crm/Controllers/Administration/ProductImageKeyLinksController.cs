using System.Threading.Tasks;
using Aklion.Crm.Attributes;
using Aklion.Crm.Dao.ProductImageKeyLink;
using Aklion.Crm.Mappers.Administration.ProductImageKeyLink;
using Aklion.Crm.Models;
using Aklion.Crm.Models.Administration.ProductImageKeyLink;
using Aklion.Infrastructure.FileFormat;
using Microsoft.AspNetCore.Mvc;

namespace Aklion.Crm.Controllers.Administration
{
    [AjaxErrorHandle]
    public class ProductImageKeyLinksController : BaseController
    {
        private readonly IProductImageKeyLinkDao _dao;

        public ProductImageKeyLinksController(IProductImageKeyLinkDao dao)
        {
            _dao = dao;
        }

        [HttpGet]
        public async Task<PagingModel<ProductImageKeyLinkModel>> GetList(ProductImageKeyLinkParameterModel model)
        {
            var result = await _dao.GetPagedListAsync(model.MapNew()).ConfigureAwait(false);
            return result.MapNew(model.Page, model.Size);
        }

        [HttpPost]
        public Task Create(ProductImageKeyLinkModel model)
        {
            return _dao.CreateAsync(model.MapNew());
        }

        [HttpPost]
        public async Task Update(ProductImageKeyLinkModel model)
        {
            var result = await _dao.GetAsync(model.Id).ConfigureAwait(false);
            await _dao.UpdateAsync(result.MapFrom(model)).ConfigureAwait(false);
        }

        [HttpPost]
        public async Task LoadImage(ProductImageKeyLinkLoadImageModel model)
        {
            if (!model.ImageFile.FileName.IsImage())
            {
                return;
            }

            await _dao.SetImageAsync(model.Id, model.ImageFile.OpenReadStream()).ConfigureAwait(false);
        }

        [HttpPost]
        public Task Delete(int id)
        {
            return _dao.GetAsync(id);
        }
    }
}