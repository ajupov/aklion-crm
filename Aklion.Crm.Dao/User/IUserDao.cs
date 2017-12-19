using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Aklion.Crm.Domain.User;

namespace Aklion.Crm.Dao.User
{
    public interface IUserDao
    {
        Task<Tuple<int, List<UserModel>>> GetPagedListAsync(UserParameterModel parameter);

        Task<Dictionary<string, int>> GetForAutocompleteAsync(UserAutocompleteParameterModel parameter);

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