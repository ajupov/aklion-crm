using System.Collections.Generic;
using System.Threading.Tasks;
using Crm.Domain.ClientAttributeLink;

namespace Crm.Dao.ClientAttributeLink
{
    public interface IClientAttributeLinkDao
    {
        Task<(int TotalCount, List<ClientAttributeLinkModel> List)> GetPagedListAsync(ClientAttributeLinkParameterModel parameter);

        Task<ClientAttributeLinkModel> GetAsync(int id);

        Task<int> CreateAsync(ClientAttributeLinkModel model);

        Task UpdateAsync(ClientAttributeLinkModel model);

        Task DeleteAsync(int id);
    }
}