using System.Threading.Tasks;
using Aklion.Crm.Domain.ProductAttribute;
using Aklion.Infrastructure.Storage.DataBaseExecutor.Models;

namespace Aklion.Crm.Dao.ProductAttribute
{
    public interface IProductAttributeDao
    {
        Task<Paging<ProductAttributeModel>> GetPagedList(ProductAttributeParameterModel parameterModel);

        Task<ProductAttributeModel> Get(int id);

        Task<int> Create(ProductAttributeModel model);

        Task Update(ProductAttributeModel model);

        Task Delete(int id);
    }
}