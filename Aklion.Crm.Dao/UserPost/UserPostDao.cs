using System.Threading.Tasks;
using Aklion.Crm.Domain.UserPost;
using Aklion.Infrastructure.Storage.DataBaseExecutor;
using Aklion.Infrastructure.Storage.DataBaseExecutor.Models;

namespace Aklion.Crm.Dao.UserPost
{
    public class UserPostDao : IUserPostDao
    {
        private readonly IDataBaseExecutor _dataBaseExecutor;

        public UserPostDao(IDataBaseExecutor dataBaseExecutor)
        {
            _dataBaseExecutor = dataBaseExecutor;
        }

        public Task<Paging<UserPostModel>> GetPagedList(UserPostParameterModel parameterModel)
        {
            return _dataBaseExecutor.SelectListWithTotalCount<UserPostModel>(Queries.GetPagedList, parameterModel);
        }

        public Task<UserPostModel> Get(int id)
        {
            return _dataBaseExecutor.SelectOne<UserPostModel>(Queries.Get, new {id});
        }

        public Task<int> Create(UserPostModel model)
        {
            return _dataBaseExecutor.SelectOne<int>(Queries.Create, model);
        }

        public Task Update(UserPostModel model)
        {
            return _dataBaseExecutor.Execute(Queries.Update, model);
        }

        public Task Delete(int id)
        {
            return _dataBaseExecutor.Execute(Queries.Delete, new {id});
        }
    }
}