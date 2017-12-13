using System.Threading.Tasks;
using Aklion.Crm.Domain.UserPermission;
using Aklion.Infrastructure.Storage.DataBaseExecutor.Pagingation;

namespace Aklion.Crm.Dao.UserPermission
{
    public interface IUserPermissionDao
    {
        Task<Paging<UserPermissionModel>> GetPagedList(UserPermissionParameterModel parameterModel);

        Task<UserPermissionModel> Get(int id);

        Task<int> Create(UserPermissionModel model);

        Task Update(UserPermissionModel model);

        Task Delete(int id);
    }
}