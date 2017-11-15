using System.Threading.Tasks;
using Aklion.Crm.Domain.ProductCategory;
using Aklion.Infrastructure.Storage.DataBaseExecutor.Models;

namespace Aklion.Crm.Dao.ProductCategory
{
    public interface IProductCategoryDao
    {
        Task<Paging<ProductCategoryModel>> GetPagedList(ProductCategoryParameterModel parameterModel);

        Task<ProductCategoryModel> Get(int id);

        Task<int> Create(ProductCategoryModel model);

        Task Update(ProductCategoryModel model);

        Task Delete(int id);
    }
}