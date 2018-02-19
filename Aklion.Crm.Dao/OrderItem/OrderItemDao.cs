using System.Collections.Generic;
using System.Threading.Tasks;
using Aklion.Crm.Domain.OrderItem;
using Aklion.Infrastructure.Dao;

namespace Aklion.Crm.Dao.OrderItem
{
    public class OrderItemDao : IOrderItemDao
    {
        private readonly IDao _dao;

        public OrderItemDao(IDao dao)
        {
            _dao = dao;
        }

        public Task<(int TotalCount, List<OrderItemModel> List)> GetPagedListAsync(OrderItemParameterModel parameter)
        {
            return _dao.GetPagedListAsync<OrderItemModel, OrderItemParameterModel>(parameter);
        }

        public Task<OrderItemModel> GetAsync(int id)
        {
            return _dao.GetAsync<OrderItemModel>(id);
        }

        public Task<int> CreateAsync(OrderItemModel model)
        {
            return _dao.CreateAsync(model);
        }

        public Task UpdateAsync(OrderItemModel model)
        {
            return _dao.UpdateAsync(model);
        }

        public Task DeleteAsync(int id)
        {
            return _dao.DeleteAsync<OrderItemModel>(id);
        }
    }
}