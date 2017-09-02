using System.Collections.Generic;
using System.Threading.Tasks;

namespace Aklion.Crm.Dao.Store
{
    public interface IStoreDao
    {
        Task<Models.Store> Get(int id);

        Task<List<Models.Store>> GetList(int page, int size);

        Task<int> Create(Models.Store model);

        Task Update(Models.Store model);

        Task Delete(int id);
    }
}