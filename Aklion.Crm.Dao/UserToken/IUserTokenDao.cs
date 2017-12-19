using System.Threading.Tasks;
using Aklion.Crm.Domain.UserToken;

namespace Aklion.Crm.Dao.UserToken
{
    public interface IUserTokenDao
    {
        Task<UserTokenModel> GetAsync(UserTokenParameterModel parameter);

        Task<int> CreateAsync(UserTokenModel model);

        Task SetUsedAsync(int id);
    }
}