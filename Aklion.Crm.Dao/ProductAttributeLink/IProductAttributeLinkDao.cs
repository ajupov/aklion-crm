using System.Threading.Tasks;
using Aklion.Crm.Domain.ProductAttributeLink;
using Aklion.Infrastructure.Storage.DataBaseExecutor.Pagingation;

namespace Aklion.Crm.Dao.ProductAttributeLink
{
    public interface IProductAttributeLinkDao
    {
        Task<Paging<ProductAttributeLinkModel>> GetPagedList(ProductAttributeLinkParameterModel parameterModel);

        Task<ProductAttributeLinkModel> Get(int id);

        Task<int> Create(ProductAttributeLinkModel model);

        Task Update(ProductAttributeLinkModel model);

        Task Delete(int id);
    }
}