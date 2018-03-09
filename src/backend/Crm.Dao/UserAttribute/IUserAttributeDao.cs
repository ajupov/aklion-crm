using System.Collections.Generic;
using System.Threading.Tasks;
using Crm.Domain.UserAttribute;

namespace Crm.Dao.UserAttribute
{
    public interface IUserAttributeDao
    {
        Task<(int TotalCount, List<UserAttributeModel> List)> GetPagedListAsync(UserAttributeParameterModel parameter);

        Task<Dictionary<string, int>> GetAutocompleteAsync(UserAttributeAutocompleteParameterModel parameter);

        Task<UserAttributeModel> GetAsync(int id);

        Task<int> CreateAsync(UserAttributeModel model);

        Task UpdateAsync(UserAttributeModel model);

        Task DeleteAsync(int id);
    }
}