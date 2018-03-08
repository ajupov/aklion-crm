using System.Collections.Generic;
using System.Threading.Tasks;
using Aklion.Crm.Domain.Order;
using Aklion.Infrastructure.Dao;
using Aklion.Infrastructure.DataBaseExecutor;

namespace Aklion.Crm.Dao.Order
{
    public class OrderDao : IOrderDao
    {
        private readonly IDao _dao;
        private readonly IDataBaseExecutor _executor;

        public OrderDao(IDao dao, IDataBaseExecutor executor)
        {
            _dao = dao;
            _executor = executor;
        }

        public Task<(int TotalCount, List<OrderModel> List)> GetPagedListAsync(OrderParameterModel parameter)
        {
            return _dao.GetPagedListAsync<OrderModel, OrderParameterModel>(parameter);
        }

        public Task<OrderModel> GetAsync(int id)
        {
            return _dao.GetAsync<OrderModel>(id);
        }

        public Task<List<int>> GetAutocompleteAsync(int pattern, int storeId)
        {
            return _executor.SelectListAsync<int>(Queries.GetAutocomplete, new {pattern, storeId});
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