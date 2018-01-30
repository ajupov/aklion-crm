using System.Threading.Tasks;

namespace Aklion.Crm.Business.UserPermission
{
    public interface IUserPermissionService
    {
        Task CreateForRegisteredUserAsync(int userId, int storeId);
    }
}