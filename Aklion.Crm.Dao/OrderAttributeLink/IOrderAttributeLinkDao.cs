using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Aklion.Crm.Domain.OrderAttributeLink;

namespace Aklion.Crm.Dao.OrderAttributeLink
{
    public interface IOrderAttributeLinkDao
    {
        Task<Tuple<int, List<OrderAttributeLinkModel>>> GetPagedListAsync(OrderAttributeLinkParameterModel parameter);

        Task<OrderAttributeLinkModel> GetAsync(int id);

        Task<int> CreateAsync(OrderAttributeLinkModel model);

        Task UpdateAsync(OrderAttributeLinkModel model);

        Task DeleteAsync(int id);
    }
}