using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Aklion.Crm.Domain.OrderStatus;

namespace Aklion.Crm.Dao.OrderStatus
{
    public interface IOrderStatusDao
    {
        Task<Tuple<int, List<OrderStatusModel>>> GetPagedListAsync(OrderStatusParameterModel parameter);

        Task<Dictionary<string, int>> GetForSelectAsync(OrderStatusSelectParameterModel parameter);

        Task<OrderStatusModel> GetAsync(int id);

        Task<int> CreateAsync(OrderStatusModel model);

        Task UpdateAsync(OrderStatusModel model);

        Task DeleteAsync(int id);
    }
}