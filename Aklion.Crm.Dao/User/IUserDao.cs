using System.Collections.Generic;
using System.Threading.Tasks;
using Aklion.Crm.Domain.User;

namespace Aklion.Crm.Dao.User
{
    public interface IUserDao
    {
        Task<(int TotalCount, List<UserModel> List)> GetPagedListAsync(UserParameterModel parameter);

        Task<(int TotalCount, List<UserByStoreModel> List)> GetPagedListAsync(UserByStoreParameterModel parameter);

        Task<Dictionary<string, int>> GetAutocompleteAsync(UserAutocompleteParameterModel parameter);

        Task<UserModel> GetAsync(int id);

        Task<int> CreateAsync(UserModel model);

        Task UpdateAsync(UserModel model);

        Task DeleteAsync(int id);

        Task<UserModel> GetByLoginAsync(string login);

        Task<UserModel> GetByEmailAsync(string email);

        Task<bool> IsExistByLoginAsync(string login);

        Task<bool> IsExistByEmailAsync(string email);

        Task<bool> IsExistByPhoneAsync(string phone);
    }
}