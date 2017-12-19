using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Aklion.Crm.Domain.Order;
using Aklion.Infrastructure.Dao;

namespace Aklion.Crm.Dao.Order
{
    public class OrderDao : IOrderDao
    {
        private readonly IDao _dao;

        public OrderDao(IDao dao)
        {
            _dao = dao;
        }

        public Task<Tuple<int, List<OrderModel>>> GetPagedListAsync(OrderParameterModel parameter)
        {
            return _dao.GetPagedListAsync<OrderModel, OrderParameterModel>(parameter);
        }

        public Task<OrderModel> GetAsync(int id)
        {
            return _dao.GetAsync<OrderModel>(id);
        }

        public Task<int> CreateAsync(OrderModel model)
        {
            return _dao.CreateAsync(model);
        }

        public Task UpdateAsync(OrderModel model)
        {
            return _dao.UpdateAsync(model);
        }

        public Task DeleteAsync(int id)
        {
            return _dao.DeleteAsync<OrderModel>(id);
        }
    }
}