using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Aklion.Crm.ApiV1.Models.Organization;
using Aklion.Crm.DateAccessLayer.Organization;
using Microsoft.AspNetCore.Mvc;

namespace Aklion.Crm.ApiV1.Controllers
{
    [Route("api/v1/[controller]")]
    public class OrganizationsController : Controller
    {
        private readonly IOrganizationDao _organizationDao;

        public OrganizationsController(IOrganizationDao organizationDao)
        {
            _organizationDao = organizationDao;
        }

        [HttpGet]
        public async Task<List<OrganizationModel>> Get()
        {
            var organizations = await _organizationDao.GetList().ConfigureAwait(false);
            return organizations.Select(o => new OrganizationModel {Id = o.Id, Name = o.Name}).ToList();
        }

        [HttpGet("{id}")]
        public async Task<OrganizationModel> Get(int id)
        {
            var organization = await _organizationDao.Get(id).ConfigureAwait(false);
            return new OrganizationModel {Id = organization.Id, Name = organization.Name};
        }
    }
}