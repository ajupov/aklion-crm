using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Aklion.Crm.Domain.UserAttribute;

namespace Aklion.Crm.Dao.UserAttribute
{
    public interface IUserAttributeDao
    {
        Task<Tuple<int, List<UserAttributeModel>>> GetPagedListAsync(UserAttributeParameterModel parameter);

        Task<Dictionary<string, int>> GetForAutocompleteAsync(UserAttributeAutocompleteParameterModel parameter);

        Task<UserAttributeModel> GetAsync(int id);

        Task<int> CreateAsync(UserAttributeModel model);

        Task UpdateAsync(UserAttributeModel model);

        Task DeleteAsync(int id);
    }
}