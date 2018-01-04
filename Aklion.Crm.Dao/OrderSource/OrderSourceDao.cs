using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Aklion.Crm.Domain.OrderSource;
using Aklion.Infrastructure.Dao;

namespace Aklion.Crm.Dao.OrderSource
{
    public class OrderSourceDao : IOrderSourceDao
    {
        private readonly IDao _dao;

        public OrderSourceDao(IDao dao)
        {
            _dao = dao;
        }

        public Task<Tuple<int, List<OrderSourceModel>>> GetPagedListAsync(OrderSourceParameterModel parameter)
        {
            return _dao.GetPagedListAsync<OrderSourceModel, OrderSourceParameterModel>(parameter);
        }

        public Task<Dictionary<string, int>> GetForAutocompleteAsync(OrderSourceAutocompleteParameterModel parameter)
        {
            return _dao.GetForAutoCompleteAsync<OrderSourceModel, OrderSourceAutocompleteParameterModel>(parameter);
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