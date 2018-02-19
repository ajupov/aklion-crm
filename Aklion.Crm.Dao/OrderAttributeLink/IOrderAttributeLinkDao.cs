using System.Collections.Generic;
using System.Threading.Tasks;
using Aklion.Crm.Domain.OrderAttributeLink;

namespace Aklion.Crm.Dao.OrderAttributeLink
{
    public interface IOrderAttributeLinkDao
    {
        Task<(int TotalCount, List<OrderAttributeLinkModel> List)> GetPagedListAsync(
            OrderAttributeLinkParameterModel parameter);

        Task<OrderAttributeLinkModel> GetAsync(int id);

        Task<int> CreateAsync(OrderAttributeLinkModel model);

        Task UpdateAsync(OrderAttributeLinkModel model);

        Task DeleteAsync(int id);
    }
}