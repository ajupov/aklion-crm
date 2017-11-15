using System.Collections.Generic;
using System.Threading.Tasks;
using Aklion.Crm.Domain;
using Aklion.Crm.Domain.Product;
using Aklion.Infrastructure.Storage.DataBaseExecutor.Models;

namespace Aklion.Crm.Dao.Product
{
    public interface IProductDao
    {
        Task<Paging<ProductModel>> GetPagedList(ProductParameterModel parameterModel);

        Task<List<AutocompleteModel>> GetForAutocompleteByNamePattern(string pattern, int storeId);

        Task<ProductModel> Get(int id);

        Task<int> Create(ProductModel model);

        Task Update(ProductModel model);

        Task Delete(int id);
    }
}