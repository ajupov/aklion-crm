using System.Collections.Generic;
using System.Threading.Tasks;
using Aklion.Crm.Domain.ProductAttribute;

namespace Aklion.Crm.Dao.ProductAttribute
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