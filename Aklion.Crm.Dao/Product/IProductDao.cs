using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Aklion.Crm.Domain.Product;

namespace Aklion.Crm.Dao.Product
{
    public interface IProductDao
    {
        Task<Tuple<int, List<ProductModel>>> GetPagedListAsync(ProductParameterModel parameter);

        Task<Dictionary<string, int>> GetForAutocompleteAsync(ProductAutocompleteParameterModel parameter);

        Task<ProductModel> GetAsync(int id);

        Task<int> CreateAsync(ProductModel model);

        Task UpdateAsync(ProductModel model);

        Task DeleteAsync(int id);
    }
}