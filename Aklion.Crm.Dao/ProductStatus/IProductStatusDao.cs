using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Aklion.Crm.Domain.ProductStatus;

namespace Aklion.Crm.Dao.ProductStatus
{
    public interface IProductStatusDao
    {
        Task<Tuple<int, List<ProductStatusModel>>> GetPagedListAsync(ProductStatusParameterModel parameter);

        Task<Dictionary<string, int>> GetForSelectAsync(ProductStatusSelectParameterModel parameter);

        Task<ProductStatusModel> GetAsync(int id);

        Task<int> CreateAsync(ProductStatusModel model);

        Task UpdateAsync(ProductStatusModel model);

        Task DeleteAsync(int id);
    }
}