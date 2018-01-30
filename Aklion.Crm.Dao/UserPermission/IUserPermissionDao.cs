using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Aklion.Crm.Domain.UserPermission;

namespace Aklion.Crm.Dao.UserPermission
{
    public interface IUserPermissionDao
    {
        Task<Tuple<int, List<UserPermissionModel>>> GetPagedListAsync(UserPermissionParameterModel parameter);

        Task<UserPermissionModel> GetAsync(int id);

        Task<int> CreateAsync(UserPermissionModel model);

        Task CreateListAsync(List<UserPermissionModel> models);

        Task UpdateAsync(UserPermissionModel model);

        Task DeleteAsync(int id);
    }
}