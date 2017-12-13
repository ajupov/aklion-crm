using System.Threading.Tasks;
using Aklion.Crm.Domain.ProductAttribute;
using Aklion.Infrastructure.DataBaseExecutor;
using Aklion.Infrastructure.Storage.DataBaseExecutor.Pagingation;

namespace Aklion.Crm.Dao.ProductAttribute
{
    public class ProductAttributeDao : IProductAttributeDao
    {
        private readonly IDataBaseExecutor _dataBaseExecutor;

        public ProductAttributeDao(IDataBaseExecutor dataBaseExecutor)
        {
            _dataBaseExecutor = dataBaseExecutor;
        }

        public Task<Paging<ProductAttributeModel>> GetPagedList(ProductAttributeParameterModel parameterModel)
        {
            return _dataBaseExecutor.SelectListWithTotalCount<ProductAttributeModel>(Queries.GetPagedList, parameterModel);
        }

        public Task<ProductAttributeModel> Get(int id)
        {
            return _dataBaseExecutor.SelectOneAsync<ProductAttributeModel>(Queries.Get, new {id});
        }

        public Task<int> Create(ProductAttributeModel model)
        {
            return _dataBaseExecutor.SelectOneAsync<int>(Queries.Create, model);
        }

        public Task Update(ProductAttributeModel model)
        {
            return _dataBaseExecutor.ExecuteAsync(Queries.Update, model);
        }

        public Task Delete(int id)
        {
            return _dataBaseExecutor.ExecuteAsync(Queries.Delete, new {id});
        }
    }
}