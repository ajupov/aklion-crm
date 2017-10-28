using System.Threading.Tasks;
using Aklion.Crm.Domain.Models.User;
using Aklion.Infrastructure.Storage.DataBaseExecutor.Models;

namespace Aklion.Crm.Domain.Interfaces.User
{
    public interface IUserDao
    {
        Task<Paging<Models.User.User>> GetPagedList(UserParameter parameter);

        Task<Models.User.User> Get(int id);

        Task<int> Insert(Models.User.User model);

        Task Update(Models.User.User model);

        Task Delete(int id);
    }
}