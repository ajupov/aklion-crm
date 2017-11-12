using System.Threading.Tasks;
using Aklion.Crm.Domain.Post;
using Aklion.Infrastructure.Storage.DataBaseExecutor;
using Aklion.Infrastructure.Storage.DataBaseExecutor.Models;

namespace Aklion.Crm.Dao.Post
{
    public class PostDao : IPostDao
    {
        private readonly IDataBaseExecutor _dataBaseExecutor;

        public PostDao(IDataBaseExecutor dataBaseExecutor)
        {
            _dataBaseExecutor = dataBaseExecutor;
        }

        public Task<Paging<PostModel>> GetPagedList(PostParameterModel parameterModel)
        {
            return _dataBaseExecutor.SelectListWithTotalCount<PostModel>(Queries.GetPagedList, parameterModel);
        }

        public Task<PostModel> Get(int id)
        {
            return _dataBaseExecutor.SelectOne<PostModel>(Queries.Get, new {id});
        }

        public Task<int> Create(PostModel model)
        {
            return _dataBaseExecutor.SelectOne<int>(Queries.Create, model);
        }

        public Task Update(PostModel model)
        {
            return _dataBaseExecutor.Execute(Queries.Update, model);
        }

        public Task Delete(int id)
        {
            return _dataBaseExecutor.Execute(Queries.Delete, new {id});
        }
    }
}