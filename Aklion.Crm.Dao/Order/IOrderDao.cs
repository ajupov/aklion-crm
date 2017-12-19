using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Aklion.Crm.Domain.Order;

namespace Aklion.Crm.Dao.Order
{
    public interface IOrderDao
    {
        Task<Tuple<int, List<OrderModel>>> GetPagedListAsync(OrderParameterModel parameter);

        Task<OrderModel> GetAsync(int id);

        Task<int> CreateAsync(OrderModel model);

        Task UpdateAsync(OrderModel model);

        Task DeleteAsync(int id);
    }
}