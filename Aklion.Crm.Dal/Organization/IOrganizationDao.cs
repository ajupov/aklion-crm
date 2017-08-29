using System.Collections.Generic;
using System.Threading.Tasks;

namespace Aklion.Crm.Dao.Organization
{
    public interface IOrganizationDao
    {
        Task<Models.Organization> Get(int id);

        Task<List<Models.Organization>> GetList(int page, int size);

        Task<int> Create(Models.Organization model);

        Task Update(Models.Organization model);

        Task Delete(int id);
    }
}