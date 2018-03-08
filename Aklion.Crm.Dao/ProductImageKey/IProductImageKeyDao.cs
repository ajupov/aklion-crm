using System.Collections.Generic;
using System.Threading.Tasks;
using Aklion.Crm.Domain.ProductImageKey;

namespace Aklion.Crm.Dao.ProductImageKey
{
    public interface IProductImageKeyDao
    {
        Task<(int TotalCount, List<ProductImageKeyModel> List)> GetPagedListAsync(ProductImageKeyParameterModel parameter);

        Task<Dictionary<string, int>> GetSelectAsync(ProductImageKeySelectParameterModel parameter);

        Task<ProductImageKeyModel> GetAsync(int id);

        Task<int> CreateAsync(ProductImageKeyModel model);

        Task UpdateAsync(ProductImageKeyModel model);

        Task DeleteAsync(int id);
    }
}