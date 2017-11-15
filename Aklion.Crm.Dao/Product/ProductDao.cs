using System.Collections.Generic;
using System.Threading.Tasks;
using Aklion.Crm.Domain;
using Aklion.Crm.Domain.Product;
using Aklion.Infrastructure.Storage.DataBaseExecutor;
using Aklion.Infrastructure.Storage.DataBaseExecutor.Models;

namespace Aklion.Crm.Dao.Product
{
    public class ProductDao : IProductDao
    {
        private readonly IDataBaseExecutor _dataBaseExecutor;

        public ProductDao(IDataBaseExecutor dataBaseExecutor)
        {
            _dataBaseExecutor = dataBaseExecutor;
        }

        public Task<Paging<ProductModel>> GetPagedList(ProductParameterModel parameterModel)
        {
            return _dataBaseExecutor.SelectListWithTotalCount<ProductModel>(Queries.GetPagedList, parameterModel);
        }

        public Task<List<AutocompleteModel>> GetForAutocompleteByNamePattern(string pattern, int storeId)
        {
            return _dataBaseExecutor.SelectList<AutocompleteModel>(Queries.GetForAutocompleteByNamePattern,
                new {pattern, storeId});
        }

        public Task<ProductModel> Get(int id)
        {
            return _dataBaseExecutor.SelectOne<ProductModel>(Queries.Get, new {id});
        }

        public Task<int> Create(ProductModel model)
        {
            return _dataBaseExecutor.SelectOne<int>(Queries.Create, model);
        }

        public Task Update(ProductModel model)
        {
            return _dataBaseExecutor.Execute(Queries.Update, model);
        }

        public Task Delete(int id)
        {
            return _dataBaseExecutor.Execute(Queries.Delete, new {id});
        }
    }
}