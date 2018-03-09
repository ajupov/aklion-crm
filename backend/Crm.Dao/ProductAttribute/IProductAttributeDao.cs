using System.Collections.Generic;
using System.Threading.Tasks;
using Crm.Domain.ProductAttribute;

namespace Crm.Dao.ProductAttribute
{
    public interface IProductAttributeDao
    {
        Task<(int TotalCount, List<ProductAttributeModel> List)> GetPagedListAsync(ProductAttributeParameterModel parameter);

        Task<Dictionary<string, int>> GetAutocompleteAsync(ProductAttributeAutocompleteParameterModel parameter);

        Task<ProductAttributeModel> GetAsync(int id);

        Task<int> CreateAsync(ProductAttributeModel model);

        Task UpdateAsync(ProductAttributeModel model);

        Task DeleteAsync(int id);
    }
}