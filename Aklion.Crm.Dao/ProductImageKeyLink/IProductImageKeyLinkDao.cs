using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Aklion.Crm.Domain.ProductImageKeyLink;

namespace Aklion.Crm.Dao.ProductImageKeyLink
{
    public interface IProductImageKeyLinkDao
    {
        Task<Tuple<int, List<ProductImageKeyLinkModel>>> GetPagedListAsync(ProductImageKeyLinkParameterModel parameter);

        Task<ProductImageKeyLinkModel> GetAsync(int id);

        Task<int> CreateAsync(ProductImageKeyLinkModel model);

        Task UpdateAsync(ProductImageKeyLinkModel model);

        Task DeleteAsync(int id);
    }
}