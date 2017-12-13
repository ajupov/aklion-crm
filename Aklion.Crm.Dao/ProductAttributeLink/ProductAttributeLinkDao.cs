using System.Threading.Tasks;
using Aklion.Crm.Domain.ProductAttributeLink;
using Aklion.Infrastructure.DataBaseExecutor;
using Aklion.Infrastructure.Storage.DataBaseExecutor.Pagingation;

namespace Aklion.Crm.Dao.ProductAttributeLink
{
    public class ProductAttributeLinkDao : IProductAttributeLinkDao
    {
        private readonly IDataBaseExecutor _dataBaseExecutor;

        public ProductAttributeLinkDao(IDataBaseExecutor dataBaseExecutor)
        {
            _dataBaseExecutor = dataBaseExecutor;
        }

        public Task<Paging<ProductAttributeLinkModel>> GetPagedList(ProductAttributeLinkParameterModel parameterModel)
        {
            return _dataBaseExecutor.SelectListWithTotalCount<ProductAttributeLinkModel>(Queries.GetPagedList, parameterModel);
        }

        public Task<ProductAttributeLinkModel> Get(int id)
        {
            return _dataBaseExecutor.SelectOneAsync<ProductAttributeLinkModel>(Queries.Get, new {id});
        }

        public Task<int> Create(ProductAttributeLinkModel model)
        {
            return _dataBaseExecutor.SelectOneAsync<int>(Queries.Create, model);
        }

        public Task Update(ProductAttributeLinkModel model)
        {
            return _dataBaseExecutor.ExecuteAsync(Queries.Update, model);
        }

        public Task Delete(int id)
        {
            return _dataBaseExecutor.ExecuteAsync(Queries.Delete, new {id});
        }
    }
}