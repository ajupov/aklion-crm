using System.Collections.Generic;
using System.Threading.Tasks;
using Aklion.Crm.Domain.OrderSource;

namespace Aklion.Crm.Dao.OrderSource
{
    public interface IOrderSourceDao
    {
        Task<(int TotalCount, List<OrderSourceModel> List)> GetPagedListAsync(OrderSourceParameterModel parameter);

        Task<Dictionary<string, int>> GetSelectAsync(OrderSourceSelectParameterModel parameter);

        Task<OrderSourceModel> GetAsync(int id);

        Task<int> CreateAsync(OrderSourceModel model);

        Task UpdateAsync(OrderSourceModel model);

        Task DeleteAsync(int id);
    }
}