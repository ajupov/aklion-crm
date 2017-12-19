using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Aklion.Crm.Domain.ClientAttributeLink;

namespace Aklion.Crm.Dao.ClientAttributeLink
{
    public interface IClientAttributeLinkDao
    {
        Task<Tuple<int, List<ClientAttributeLinkModel>>> GetPagedListAsync(ClientAttributeLinkParameterModel parameter);

        Task<ClientAttributeLinkModel> GetAsync(int id);

        Task<int> CreateAsync(ClientAttributeLinkModel model);

        Task UpdateAsync(ClientAttributeLinkModel model);

        Task DeleteAsync(int id);
    }
}