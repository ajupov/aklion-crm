using System.Collections.Generic;
using System.Threading.Tasks;
using Crm.Domain.OrderAttribute;

namespace Crm.Dao.OrderAttribute
{
    public interface IOrderAttributeDao
    {
        Task<(int TotalCount, List<OrderAttributeModel> List)> GetPagedListAsync(OrderAttributeParameterModel parameter);

        Task<Dictionary<string, int>> GetAutocompleteAsync(OrderAttributeAutocompleteParameterModel parameter);

        Task<OrderAttributeModel> GetAsync(int id);

        Task<int> CreateAsync(OrderAttributeModel model);

        Task UpdateAsync(OrderAttributeModel model);

        Task DeleteAsync(int id);
    }
}