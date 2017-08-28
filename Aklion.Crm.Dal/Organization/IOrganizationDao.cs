using System.Collections.Generic;
using System.Threading.Tasks;
using Aklion.Crm.Domain.Organization;

namespace Aklion.Crm.DateAccessLayer.Organization
{
    public interface IOrganizationDao
    {
        Task<OrganizationDomainModel> Get(int id);

        Task<List<OrganizationDomainModel>> GetList();

        Task<List<OrganizationDomainModel>> GetPagedList(int page, int size);

        Task Create(OrganizationDomainModel model);

        Task Update(OrganizationDomainModel model);

        Task Delete(int id);
    }
}