using System.Collections.Generic;
using System.Threading.Tasks;
using Aklion.Crm.Domain.ClientAttribute;

namespace Aklion.Crm.Dao.ClientAttribute
{
    public interface IClientAttributeDao
    {
        Task<(int TotalCount, List<ClientAttributeModel> List)> GetPagedListAsync(
            ClientAttributeParameterModel parameter);

        Task<Dictionary<string, int>> GetAutocompleteAsync(ClientAttributeAutocompleteParameterModel parameter);

        Task<ClientAttributeModel> GetAsync(int id);

        Task<int> CreateAsync(ClientAttributeModel model);

        Task UpdateAsync(ClientAttributeModel model);

        Task DeleteAsync(int id);
    }
}