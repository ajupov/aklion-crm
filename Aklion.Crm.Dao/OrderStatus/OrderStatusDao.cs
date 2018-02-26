using System.Collections.Generic;
using System.Threading.Tasks;
using Aklion.Crm.Domain.OrderStatus;
using Aklion.Infrastructure.Dao;

namespace Aklion.Crm.Dao.OrderStatus
{
    public class OrderStatusDao : IOrderStatusDao
    {
        private readonly IDao _dao;

        public OrderStatusDao(IDao dao)
        {
            _dao = dao;
        }

        public Task<(int TotalCount, List<OrderStatusModel> List)> GetPagedListAsync(OrderStatusParameterModel parameter)
        {
            return _dao.GetPagedListAsync<OrderStatusModel, OrderStatusParameterModel>(parameter);
        }

        public Task<Dictionary<string, int>> GetSelectAsync(OrderStatusSelectParameterModel parameter)
        {
            return _dao.GetForSelectAsync<OrderStatusModel, OrderStatusSelectParameterModel>(parameter);
        }

        public Task<OrderStatusModel> GetAsync(int id)
        {
            return _dao.GetAsync<OrderStatusModel>(id);
        }

        public Task<int> CreateAsync(OrderStatusModel model)
        {
            return _dao.CreateAsync(model);
        }

        public Task UpdateAsync(OrderStatusModel model)
        {
            return _dao.UpdateAsync(model);
        }

        public Task DeleteAsync(int id)
        {
            return _dao.DeleteAsync<OrderStatusModel>(id);
        }
    }
}