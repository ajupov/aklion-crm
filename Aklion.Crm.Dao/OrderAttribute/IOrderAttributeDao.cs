using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Aklion.Crm.Domain.OrderAttribute;

namespace Aklion.Crm.Dao.OrderAttribute
{
    public interface IOrderAttributeDao
    {
        Task<Tuple<int, List<OrderAttributeModel>>> GetPagedListAsync(OrderAttributeParameterModel parameter);

        Task<Dictionary<string, int>> GetForAutocompleteAsync(OrderAttributeAutocompleteParameterModel parameter);

        Task<OrderAttributeModel> GetAsync(int id);

        Task<int> CreateAsync(OrderAttributeModel model);

        Task UpdateAsync(OrderAttributeModel model);

        Task DeleteAsync(int id);
    }
}