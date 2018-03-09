using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Crm.Domain.ProductImageKeyLink;

namespace Crm.Dao.ProductImageKeyLink
{
    public interface IProductImageKeyLinkDao
    {
        Task<(int TotalCount, List<ProductImageKeyLinkModel> List)> GetPagedListAsync(ProductImageKeyLinkParameterModel parameter);

        Task<ProductImageKeyLinkModel> GetAsync(int id);

        Task<int> CreateAsync(ProductImageKeyLinkModel model);

        Task UpdateAsync(ProductImageKeyLinkModel model);

        Task SetImageAsync(int id, Stream stream);

        Task DeleteAsync(int id);
    }
}