using System.Threading.Tasks;
using Aklion.Crm.Domain.UserToken;
using Aklion.Infrastructure.Dao;

namespace Aklion.Crm.Dao.UserToken
{
    public class UserTokenDao : IUserTokenDao
    {
        private readonly IDao _dao;

        public UserTokenDao(IDao dao)
        {
            _dao = dao;
        }

        public Task<UserTokenModel> GetAsync(UserTokenParameterModel parameter)
        {
            return _dao.GetAsync<UserTokenModel, UserTokenParameterModel>(parameter);
        }

        public Task<int> CreateAsync(UserTokenModel model)
        {
            return _dao.CreateAsync(model);
        }

        public async Task SetUsedAsync(int id)
        {
            var result =  await _dao.GetAsync<UserTokenModel>(id).ConfigureAwait(false);

            result.IsUsed = true;

            await _dao.UpdateAsync(result).ConfigureAwait(false);
        }
    }
}