using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Aklion.Crm.Domain.OrderSource;

namespace Aklion.Crm.Dao.OrderSource
{
    public interface IOrderSourceDao
    {
        Task<Tuple<int, List<OrderSourceModel>>> GetPagedListAsync(OrderSourceParameterModel parameter);

        Task<Dictionary<string, int>> GetForAutocompleteAsync(OrderSourceAutocompleteParameterModel parameter);

        Task<OrderSourceModel> GetAsync(int id);

        Task<int> CreateAsync(OrderSourceModel model);

        Task UpdateAsync(OrderSourceModel model);

        Task DeleteAsync(int id);
    }
}