using System.Threading.Tasks;
using Aklion.Crm.Domain.ProductTag;
using Aklion.Infrastructure.Storage.DataBaseExecutor.Pagingation;

namespace Aklion.Crm.Dao.ProductTag
{
    public interface IProductTagDao
    {
        Task<Paging<ProductTagModel>> GetPagedList(ProductTagParameterModel parameterModel);

        Task<ProductTagModel> Get(int id);

        Task<int> Create(ProductTagModel model);

        Task Update(ProductTagModel model);

        Task Delete(int id);
    }
}