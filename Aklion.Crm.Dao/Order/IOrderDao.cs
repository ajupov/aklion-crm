using System.Collections.Generic;
using System.Threading.Tasks;
using Aklion.Crm.Domain.Order;

namespace Aklion.Crm.Dao.Order
{
    public interface IOrderDao
    {
        Task<(int TotalCount, List<OrderModel> List)> GetPagedListAsync(OrderParameterModel parameter);

        Task<OrderModel> GetAsync(int id);

        Task<List<int>> GetAutocompleteAsync(int pattern, int storeId);

        Task<int> CreateAsync(OrderModel model);

        Task UpdateAsync(OrderModel model);

        Task DeleteAsync(int id);
    }
}