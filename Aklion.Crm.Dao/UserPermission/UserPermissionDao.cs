using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Aklion.Crm.Domain.UserPermission;
using Aklion.Infrastructure.Dao;

namespace Aklion.Crm.Dao.UserPermission
{
    public class UserPermissionDao : IUserPermissionDao
    {
        private readonly IDao _dao;

        public UserPermissionDao(IDao dao)
        {
            _dao = dao;
        }

        public Task<Tuple<int, List<UserPermissionModel>>> GetPagedListAsync(UserPermissionParameterModel parameter)
        {
            return _dao.GetPagedListAsync<UserPermissionModel, UserPermissionParameterModel>(parameter);
        }

        public Task<UserPermissionModel> GetAsync(int id)
        {
            return _dao.GetAsync<UserPermissionModel>(id);
        }

        public Task<int> CreateAsync(UserPermissionModel model)
        {
            return _dao.CreateAsync(model);
        }

        public Task UpdateAsync(UserPermissionModel model)
        {
            return _dao.UpdateAsync(model);
        }

        public Task DeleteAsync(int id)
        {
            return _dao.DeleteAsync<UserPermissionModel>(id);
        }
    }
}