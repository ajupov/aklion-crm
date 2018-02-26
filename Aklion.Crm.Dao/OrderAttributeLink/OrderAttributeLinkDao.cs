using System.Collections.Generic;
using System.Threading.Tasks;
using Aklion.Crm.Domain.OrderAttributeLink;
using Aklion.Infrastructure.Dao;

namespace Aklion.Crm.Dao.OrderAttributeLink
{
    public class OrderAttributeLinkDao : IOrderAttributeLinkDao
    {
        private readonly IDao _dao;

        public OrderAttributeLinkDao(IDao dao)
        {
            _dao = dao;
        }

        public Task<(int TotalCount, List<OrderAttributeLinkModel> List)> GetPagedListAsync(OrderAttributeLinkParameterModel parameter)
        {
            return _dao.GetPagedListAsync<OrderAttributeLinkModel, OrderAttributeLinkParameterModel>(parameter);
        }

        public Task<OrderAttributeLinkModel> GetAsync(int id)
        {
            return _dao.GetAsync<OrderAttributeLinkModel>(id);
        }

        public Task<int> CreateAsync(OrderAttributeLinkModel model)
        {
            return _dao.CreateAsync(model);
        }

        public Task UpdateAsync(OrderAttributeLinkModel model)
        {
            return _dao.UpdateAsync(model);
        }

        public Task DeleteAsync(int id)
        {
            return _dao.DeleteAsync<OrderAttributeLinkModel>(id);
        }
    }
}