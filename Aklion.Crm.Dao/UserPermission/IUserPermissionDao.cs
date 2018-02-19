using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Aklion.Crm.Domain.UserPermission;
using Aklion.Crm.Enums;

namespace Aklion.Crm.Dao.UserPermission
{
    public interface IUserPermissionDao
    {
        Task<Tuple<int, List<UserPermissionModel>>> GetPagedListAsync(UserPermissionParameterModel parameter);

        Task<List<Permission>> GetListForUserAsync(int userId, int storeId);

        Task<UserPermissionModel> GetAsync(int id);

        Task<UserPermissionModel> GetForUserAsync(int userId, int storeId, Permission permission);

        Task<bool> IsExistAsync(int userId, int storeId);

        Task<int> CreateAsync(UserPermissionModel model);

        Task CreateListAsync(List<UserPermissionModel> models);

        Task UpdateAsync(UserPermissionModel model);

        Task DeleteAsync(int id);
    }
}