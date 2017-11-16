using System.Threading.Tasks;
using Aklion.Crm.Domain.ProductTag;
using Aklion.Infrastructure.Storage.DataBaseExecutor;
using Aklion.Infrastructure.Storage.DataBaseExecutor.Models;

namespace Aklion.Crm.Dao.ProductTag
{
    public class ProductTagDao : IProductTagDao
    {
        private readonly IDataBaseExecutor _dataBaseExecutor;

        public ProductTagDao(IDataBaseExecutor dataBaseExecutor)
        {
            _dataBaseExecutor = dataBaseExecutor;
        }

        public Task<Paging<ProductTagModel>> GetPagedList(ProductTagParameterModel parameterModel)
        {
            return _dataBaseExecutor.SelectListWithTotalCount<ProductTagModel>(Queries.GetPagedList, parameterModel);
        }

        public Task<ProductTagModel> Get(int id)
        {
            return _dataBaseExecutor.SelectOne<ProductTagModel>(Queries.Get, new {id});
        }

        public Task<int> Create(ProductTagModel model)
        {
            return _dataBaseExecutor.SelectOne<int>(Queries.Create, model);
        }

        public Task Update(ProductTagModel model)
        {
            return _dataBaseExecutor.Execute(Queries.Update, model);
        }

        public Task Delete(int id)
        {
            return _dataBaseExecutor.Execute(Queries.Delete, new {id});
        }
    }
}