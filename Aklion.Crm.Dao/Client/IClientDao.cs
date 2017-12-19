using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Aklion.Crm.Domain.Client;

namespace Aklion.Crm.Dao.Client
{
    public interface IClientDao
    {
        Task<Tuple<int, List<ClientModel>>> GetPagedListAsync(ClientParameterModel parameter);

        Task<Dictionary<string, int>> GetForAutocompleteAsync(ClientAutocompleteParameterModel parameter);

        Task<ClientModel> GetAsync(int id);

        Task<int> CreateAsync(ClientModel model);

        Task UpdateAsync(ClientModel model);

        Task DeleteAsync(int id);
    }
}