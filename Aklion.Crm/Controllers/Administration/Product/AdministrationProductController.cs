﻿using System.Collections.Generic;
using System.Threading.Tasks;
using Aklion.Crm.Attributes;
using Aklion.Crm.Dao.Product;
using Aklion.Crm.Mappers.Administration.Product;
using Aklion.Crm.Models;
using Aklion.Crm.Models.Administration.Product;
using Microsoft.AspNetCore.Mvc;

namespace Aklion.Crm.Controllers.Administration.Product
{
    [AjaxErrorHandle]
    [Route("Administration/Products")]
    public class AdministrationProductController : BaseController
    {
        private readonly IProductDao _dao;

        public AdministrationProductController(IProductDao dao)
        {
            _dao = dao;
        }

        [HttpGet("")]
        public IActionResult Index()
        {
            return View("~/Views/Administration/Product/Index.cshtml");
        }

        [HttpGet]
        public async Task<PagingModel<ProductModel>> GetList(ProductParameterModel model)
        {
            var result = await _dao.GetPagedListAsync(model.MapNew()).ConfigureAwait(false);
            return result.MapNew(model.Page, model.Size);
        }

        [HttpGet]
        public Task<Dictionary<string, int>> GetAutocomplete(string pattern, int storeId)
        {
            return _dao.GetAutocompleteAsync(pattern.MapNew(storeId));
        }

        [HttpPost]
        public Task Create(ProductModel model)
        {
            return _dao.CreateAsync(model.MapNew());
        }

        [HttpPost]
        public async Task Update(ProductModel model)
        {
            var result = await _dao.GetAsync(model.Id).ConfigureAwait(false);
            await _dao.UpdateAsync(result.MapFrom(model)).ConfigureAwait(false);
        }

        [HttpPost]
        public Task Delete(int id)
        {
            return _dao.GetAsync(id);
        }
    }
}