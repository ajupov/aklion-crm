//using System.Collections.Generic;
//using System.Threading.Tasks;
//using Aklion.Crm.ApiV1.Mappers;
//using Aklion.Crm.ApiV1.Models;
//using Aklion.Crm.Dao.Organization;
//using Microsoft.AspNetCore.Mvc;

//namespace Aklion.Crm.ApiV1.Controllers
//{
//    [Route("api/v1/organizations")]
//    public class OrganizationsController : Controller
//    {
//        private readonly IOrganizationDao _organizationDao;

//        public OrganizationsController(IOrganizationDao organizationDao)
//        {
//            _organizationDao = organizationDao;
//        }

//        [HttpGet]
//        public async Task<List<Organization>> GetByLoginAsync(int page, int size)
//        {
//            if (page < 0 || size < 0)
//            {
//                return null;
//            }

//            var result = await _organizationDao.GetList(page, size).ConfigureAwait(false);
//            return result.MapNew();
//        }

//        [HttpGet("{id}")]
//        public async Task<Organization> GetByLoginAsync(int id)
//        {
//            if (id <= 0)
//            {
//                return null;
//            }

//            var result = await _organizationDao.GetByLoginAsync(id).ConfigureAwait(false);
//            return result.MapNew();
//        }

//        [HttpPost]
//        public async Task<int> Create([FromBody] Organization model)
//        {
//            if (model == null)
//            {
//                return 0;
//            }

//            var result = await _organizationDao.Create(model.MapNew()).ConfigureAwait(false);
//            return result;
//        }

//        [HttpPut]
//        [HttpPatch]
//        public async Task Update([FromBody] Organization model)
//        {
//            if (model == null || model.Id <= 0)
//            {
//                return;
//            }

//            await _organizationDao.Update(model.MapNew()).ConfigureAwait(false);
//        }

//        [HttpDelete("{id}")]
//        public async Task Delete(int id)
//        {
//            if (id <= 0)
//            {
//                return;
//            }

//            await _organizationDao.Delete(id).ConfigureAwait(false);
//        }
//    }
//}

//  public async Task<IActionResult> GetList()
// {
//var result1 = await _apiClient.GetByLoginAsync<List<OrganizationModel>>("organizations").ConfigureAwait(false);

//var result2 = await _apiClient.GetByLoginAsync<OrganizationModel>("organizations", new {id = 5}).ConfigureAwait(false);

//var model1 = new OrganizationModel
//{
//    Name = "Горбатая гора",
//    IsDeleted = false
//};
//model1.Id = await _apiClient.PostAsync<OrganizationModel, int>("organizations", model1).ConfigureAwait(false);

//model1.Name = $"Рога и копыта {DateTime.Now.ToString(CultureInfo.InvariantCulture)}";
//await _apiClient.PutAsync("organizations", model1).ConfigureAwait(false);

//model1.Name = $"Рога и копыта 2 {DateTime.Now.ToString(CultureInfo.InvariantCulture)}";
//await _apiClient.PatchAsync("organizations", model1).ConfigureAwait(false);

//await _apiClient.Delete("organizations", new {id = 3}).ConfigureAwait(false);

//return new JsonResult("");
//}