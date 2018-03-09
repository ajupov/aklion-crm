using System.Collections.Generic;
using System.Threading.Tasks;
using Crm.Domain.OrderSource;
using Infrastructure.Dao;

namespace Crm.Dao.OrderSource
{
    public class OrderSourceDao : IOrderSourceDao
    {
        private readonly IDao _dao;

        public OrderSourceDao(IDao dao)
        {
            _dao = dao;
        }

        public Task<(int TotalCount, List<OrderSourceModel> List)> GetPagedListAsync(OrderSourceParameterModel parameter)
        {
            return _dao.GetPagedListAsync<OrderSourceModel, OrderSourceParameterModel>(parameter);
        }

        public Task<Dictionary<string, int>> GetSelectAsync(OrderSourceSelectParameterModel parameter)
        {
            return _dao.GetForSelectAsync<OrderSourceModel, OrderSourceSelectParameterModel>(parameter);
        }

        public Task<OrderSourceModel> GetAsync(int id)
        {
            return _dao.GetAsync<OrderSourceModel>(id);
        }

        public Task<int> CreateAsync(OrderSourceModel model)
        {
            return _dao.CreateAsync(model);
        }

        public Task UpdateAsync(OrderSourceModel model)
        {
            return _dao.UpdateAsync(model);
        }

        public Task DeleteAsync(int id)
        {
            return _dao.DeleteAsync<OrderSourceModel>(id);
        }
    }
}