using System.Threading.Tasks;
using Aklion.Crm.Domain.UserToken;

namespace Aklion.Crm.Dao.UserToken
{
    public interface IUserTokenDao
    {
        Task<UserTokenModel> Get(UserTokenParameterModel parameterModel);

        Task<int> Create(UserTokenModel model);

        Task SetUsed(int id);
    }
}