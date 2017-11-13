using System.Threading.Tasks;
using Aklion.Crm.Domain.UserPost;
using Aklion.Infrastructure.Storage.DataBaseExecutor.Models;

namespace Aklion.Crm.Dao.UserPost
{
    public interface IUserPostDao
    {
        Task<Paging<UserPostModel>> GetPagedList(UserPostParameterModel parameterModel);

        Task<UserPostModel> Get(int id);

        Task<int> Create(UserPostModel model);

        Task Update(UserPostModel model);

        Task Delete(int id);
    }
}