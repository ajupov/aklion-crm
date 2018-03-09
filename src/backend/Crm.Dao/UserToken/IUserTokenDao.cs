using System.Threading.Tasks;
using Crm.Domain.UserToken;

namespace Crm.Dao.UserToken
{
    public interface IUserTokenDao
    {
        Task<UserTokenModel> GetAsync(UserTokenParameterModel parameter);

        Task<int> CreateAsync(UserTokenModel model);

        Task SetUsedAsync(int id);
    }
}