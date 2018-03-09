using System.Collections.Generic;
using System.Threading.Tasks;
using Crm.Domain.ProductStatus;

namespace Crm.Dao.ProductStatus
{
    public interface IProductStatusDao
    {
        Task<(int TotalCount, List<ProductStatusModel> List)> GetPagedListAsync(ProductStatusParameterModel parameter);

        Task<Dictionary<string, int>> GetSelectAsync(ProductStatusSelectParameterModel parameter);

        Task<ProductStatusModel> GetAsync(int id);

        Task<int> CreateAsync(ProductStatusModel model);

        Task UpdateAsync(ProductStatusModel model);

        Task DeleteAsync(int id);
    }
}