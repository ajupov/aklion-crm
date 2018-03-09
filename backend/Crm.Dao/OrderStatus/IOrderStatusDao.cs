using System.Collections.Generic;
using System.Threading.Tasks;
using Crm.Domain.OrderStatus;

namespace Crm.Dao.OrderStatus
{
    public interface IOrderStatusDao
    {
        Task<(int TotalCount, List<OrderStatusModel> List)> GetPagedListAsync(OrderStatusParameterModel parameter);

        Task<Dictionary<string, int>> GetSelectAsync(OrderStatusSelectParameterModel parameter);

        Task<OrderStatusModel> GetAsync(int id);

        Task<int> CreateAsync(OrderStatusModel model);

        Task UpdateAsync(OrderStatusModel model);

        Task DeleteAsync(int id);
    }
}