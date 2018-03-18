using System.Collections.Generic;
using System.Threading.Tasks;
using Crm.Domain.Client;

namespace Crm.Dao.Client
{
    public interface IClientDao
    {
        Task<(int TotalCount, List<ClientModel> List)> GetPagedListAsync(ClientParameterModel parameter);

        //Task<(int TotalCount, List<ShortClientModel> List)> GetPagedListAsync(UserClientParameterModel parameter);

        Task<Dictionary<string, int>> GetAutocompleteAsync(ClientAutocompleteParameterModel parameter);

        Task<ClientModel> GetAsync(int id);

        Task<int> CreateAsync(ClientModel model);

        Task UpdateAsync(ClientModel model);

        Task DeleteAsync(int id);
    }
}