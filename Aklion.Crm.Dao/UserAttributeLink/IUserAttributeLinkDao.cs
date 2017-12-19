using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Aklion.Crm.Domain.UserAttributeLink;

namespace Aklion.Crm.Dao.UserAttributeLink
{
    public interface IUserAttributeLinkDao
    {
        Task<Tuple<int, List<UserAttributeLinkModel>>> GetPagedListAsync(UserAttributeLinkParameterModel parameter);

        Task<UserAttributeLinkModel> GetAsync(int id);

        Task<int> CreateAsync(UserAttributeLinkModel model);

        Task UpdateAsync(UserAttributeLinkModel model);

        Task DeleteAsync(int id);
    }
}