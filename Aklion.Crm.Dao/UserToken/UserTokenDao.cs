using System.Threading.Tasks;
using Aklion.Crm.Domain.UserToken;
using Aklion.Infrastructure.DataBaseExecutor;

namespace Aklion.Crm.Dao.UserToken
{
    public class UserTokenDao : IUserTokenDao
    {
        private readonly IDataBaseExecutor _dataBaseExecutor;

        public UserTokenDao(IDataBaseExecutor dataBaseExecutor)
        {
            _dataBaseExecutor = dataBaseExecutor;
        }

        public Task<UserTokenModel> Get(UserTokenParameterModel parameterModel)
        {
            return _dataBaseExecutor.SelectOneAsync<UserTokenModel>(Queries.Get, parameterModel);
        }

        public Task<int> Create(UserTokenModel model)
        {
            return _dataBaseExecutor.SelectOneAsync<int>(Queries.Create, model);
        }

        public Task SetUsed(int id)
        {
            return _dataBaseExecutor.ExecuteAsync(Queries.SetUsed, new {id});
        }
    }
}