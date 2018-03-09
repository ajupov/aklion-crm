using System.Collections.Generic;
using System.Threading.Tasks;
using Crm.Domain.UserAttributeLink;

namespace Crm.Dao.UserAttributeLink
{
    public interface IUserAttributeLinkDao
    {
        Task<(int TotalCount, List<UserAttributeLinkModel> List)> GetPagedListAsync(UserAttributeLinkParameterModel parameter);

        Task<UserAttributeLinkModel> GetAsync(int id);

        Task<int> CreateAsync(UserAttributeLinkModel model);

        Task UpdateAsync(UserAttributeLinkModel model);

        Task DeleteAsync(int id);
    }
}