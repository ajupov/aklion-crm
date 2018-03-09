using System.Collections.Generic;
using System.Threading.Tasks;
using Crm.Domain.Order;

namespace Crm.Dao.Order
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