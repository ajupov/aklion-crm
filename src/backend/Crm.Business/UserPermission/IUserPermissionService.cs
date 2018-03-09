using Crm.Domain.UserPermission;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Crm.Business.UserPermission
{
    public interface IUserPermissionService
    {
        Task CreateForRegisteredUserAsync(int userId, int storeId);

        Task CreateForAddedUserAsync(int userId, int ownerUserId);

        Task<List<UserPermissionExistModel>> GetForUserAsync(int userId, int storeId);

        Task SwitchAsync(int userId, int storeId, Enums.Permission permission);
    }
}