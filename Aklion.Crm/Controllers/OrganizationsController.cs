using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading.Tasks;
using Aklion.Crm.Models;
using Aklion.Infrastructure.ApiClient;
using Microsoft.AspNetCore.Mvc;

namespace Aklion.Crm.Controllers
{
    public class OrganizationsController : Controller
    {
        private readonly IApiClient _apiClient;

        public OrganizationsController(IApiClient apiClient)
        {
            _apiClient = apiClient;
        }

        public IActionResult Index()
        {
            return View();
        }

      //  public async Task<IActionResult> GetList()
       // {
            //var result1 = await _apiClient.Get<List<OrganizationModel>>("organizations").ConfigureAwait(false);

            //var result2 = await _apiClient.Get<OrganizationModel>("organizations", new {id = 5}).ConfigureAwait(false);

            //var model1 = new OrganizationModel
            //{
            //    Name = "Горбатая гора",
            //    IsDeleted = false
            //};
            //model1.Id = await _apiClient.Post<OrganizationModel, int>("organizations", model1).ConfigureAwait(false);

            //model1.Name = $"Рога и копыта {DateTime.Now.ToString(CultureInfo.InvariantCulture)}";
            //await _apiClient.Put("organizations", model1).ConfigureAwait(false);

            //model1.Name = $"Рога и копыта 2 {DateTime.Now.ToString(CultureInfo.InvariantCulture)}";
            //await _apiClient.Patch("organizations", model1).ConfigureAwait(false);

            //await _apiClient.Delete("organizations", new {id = 3}).ConfigureAwait(false);

            //return new JsonResult("");
        //}
    }
}