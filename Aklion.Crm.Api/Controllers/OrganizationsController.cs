using System.Collections.Generic;
using System.Threading.Tasks;
using Aklion.Crm.ApiV1.Mappers;
using Aklion.Crm.ApiV1.Models;
using Aklion.Crm.Dao.Organization;
using Microsoft.AspNetCore.Mvc;

namespace Aklion.Crm.ApiV1.Controllers
{
    [Route("api/v1/organizations")]
    public class OrganizationsController : Controller
    {
        private readonly IOrganizationDao _organizationDao;

        public OrganizationsController(IOrganizationDao organizationDao)
        {
            _organizationDao = organizationDao;
        }
        
        [HttpGet]
        public async Task<List<Organization>> Get(int page, int size)
        {
            if (page < 0 || size < 0)
            {
                return null;
            }

            var result = await _organizationDao.GetList(page, size).ConfigureAwait(false);
            return result.Map();
        }
        
        [HttpGet("{id}")]
        public async Task<Organization> Get(int id)
        {
            if (id <= 0)
            {
                return null;
            }

            var result = await _organizationDao.Get(id).ConfigureAwait(false);
            return result.Map();
        }
        
        [HttpPost]
        public async Task<int> Create([FromBody] Organization model)
        {
            if (model == null)
            {
                return 0;
            }

            var result = await _organizationDao.Create(model.Map()).ConfigureAwait(false);
            return result;
        }
        
        [HttpPut]
        [HttpPatch]
        public async Task Update([FromBody] Organization model)
        {
            if (model == null || model.Id <= 0)
            {
                return;
            }

            await _organizationDao.Update(model.Map()).ConfigureAwait(false);
        }
        
        [HttpDelete("{id}")]
        public async Task Delete(int id)
        {
            if (id <= 0)
            {
                return;
            }

            await _organizationDao.Delete(id).ConfigureAwait(false);
        }
    }
}