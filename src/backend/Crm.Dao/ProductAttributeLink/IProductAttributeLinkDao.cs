using System.Collections.Generic;
using System.Threading.Tasks;
using Crm.Domain.ProductAttributeLink;

namespace Crm.Dao.ProductAttributeLink
{
    public interface IProductAttributeLinkDao
    {
        Task<(int TotalCount, List<ProductAttributeLinkModel> List)> GetPagedListAsync(ProductAttributeLinkParameterModel parameter);

        Task<ProductAttributeLinkModel> GetAsync(int id);

        Task<int> CreateAsync(ProductAttributeLinkModel model);

        Task UpdateAsync(ProductAttributeLinkModel model);

        Task DeleteAsync(int id);
    }
}