using System.Collections.Generic;
using System.Threading.Tasks;
using Aklion.Crm.Domain.Models.User;

namespace Aklion.Crm.Domain.Interfaces.User
{
    public interface IUserDao
    {
        Task<KeyValuePair<int, List<UserModel>>> GetList(object parameters);

        Task<UserModel> Get(int id);

        Task<int> Insert(UserModel model);

        Task Update(UserModel model);

        Task Delete(int id);
    }
}