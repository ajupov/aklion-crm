using System.Threading.Tasks;
using Aklion.Crm.Domain.ProductCategory;
using Aklion.Infrastructure.Storage.DataBaseExecutor;
using Aklion.Infrastructure.Storage.DataBaseExecutor.Models;

namespace Aklion.Crm.Dao.ProductCategory
{
    public class ProductCategoryDao : IProductCategoryDao
    {
        private readonly IDataBaseExecutor _dataBaseExecutor;

        public ProductCategoryDao(IDataBaseExecutor dataBaseExecutor)
        {
            _dataBaseExecutor = dataBaseExecutor;
        }

        public Task<Paging<ProductCategoryModel>> GetPagedList(ProductCategoryParameterModel parameterModel)
        {
            return _dataBaseExecutor.SelectListWithTotalCount<ProductCategoryModel>(Queries.GetPagedList, parameterModel);
        }

        public Task<ProductCategoryModel> Get(int id)
        {
            return _dataBaseExecutor.SelectOne<ProductCategoryModel>(Queries.Get, new {id});
        }

        public Task<int> Create(ProductCategoryModel model)
        {
            return _dataBaseExecutor.SelectOne<int>(Queries.Create, model);
        }

        public Task Update(ProductCategoryModel model)
        {
            return _dataBaseExecutor.Execute(Queries.Update, model);
        }

        public Task Delete(int id)
        {
            return _dataBaseExecutor.Execute(Queries.Delete, new {id});
        }
    }
}