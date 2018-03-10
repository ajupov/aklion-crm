using System.Threading.Tasks;
using Crm.Attributes;
using Crm.Dao.ProductImageKeyLink;
using Crm.Mappers.Administration.ProductImageKeyLink;
using Crm.Models;
using Crm.Models.Administration.ProductImageKeyLink;
using Infrastructure.FileFormat;
using Microsoft.AspNetCore.Mvc;

namespace Crm.Controllers.Administration
{
    [AjaxErrorHandle]
    [Route("Administration/ProductImageKeyLinks")]
    public class AdministrationProductImageKeyLinksController : BaseController
    {
        private readonly IProductImageKeyLinkDao _dao;

        public AdministrationProductImageKeyLinksController(IProductImageKeyLinkDao dao)
        {
            _dao = dao;
        }

        [HttpGet]
        [Route("GetList")]
        public async Task<PagingModel<ProductImageKeyLinkModel>> GetList(ProductImageKeyLinkParameterModel model)
        {
            var result = await _dao.GetPagedListAsync(model.MapNew()).ConfigureAwait(false);
            return result.MapNew(model.Page, model.Size);
        }

        [HttpPost]
        [Route("Create")]
        public Task Create(ProductImageKeyLinkModel model)
        {
            return _dao.CreateAsync(model.MapNew());
        }

        [HttpPost]
        [Route("Update")]
        public async Task Update(ProductImageKeyLinkModel model)
        {
            var result = await _dao.GetAsync(model.Id).ConfigureAwait(false);
            await _dao.UpdateAsync(result.MapFrom(model)).ConfigureAwait(false);
        }

        [HttpPost]
        [Route("LoadImage")]
        public async Task LoadImage(ProductImageKeyLinkLoadImageModel model)
        {
            if (!model.ImageFile.FileName.IsImage())
            {
                return;
            }

            await _dao.SetImageAsync(model.Id, model.ImageFile.OpenReadStream()).ConfigureAwait(false);
        }

        [HttpPost]
        [Route("Delete")]
        public Task Delete(int id)
        {
            return _dao.GetAsync(id);
        }
    }
}