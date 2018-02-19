using System.Collections.Generic;
using System.Threading.Tasks;
using Aklion.Crm.Domain.OrderItem;

namespace Aklion.Crm.Dao.OrderItem
{
    public interface IOrderItemDao
    {
        Task<(int TotalCount, List<OrderItemModel> List)> GetPagedListAsync(OrderItemParameterModel parameter);

        Task<OrderItemModel> GetAsync(int id);

        Task<int> CreateAsync(OrderItemModel model);

        Task UpdateAsync(OrderItemModel model);

        Task DeleteAsync(int id);
    }
}