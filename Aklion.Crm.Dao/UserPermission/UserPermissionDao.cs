using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Aklion.Crm.Domain.UserPermission;
using Aklion.Crm.Enums;
using Aklion.Infrastructure.Dao;
using Aklion.Infrastructure.DataBaseExecutor;

namespace Aklion.Crm.Dao.UserPermission
{
    public class UserPermissionDao : IUserPermissionDao
    {
        private readonly IDao _dao;
        private readonly IDataBaseExecutor _dataBaseExecutor;

        public UserPermissionDao(IDao dao, IDataBaseExecutor dataBaseExecutor)
        {
            _dao = dao;
            _dataBaseExecutor = dataBaseExecutor;
        }

        public Task<Tuple<int, List<UserPermissionModel>>> GetPagedListAsync(UserPermissionParameterModel parameter)
        {
            return _dao.GetPagedListAsync<UserPermissionModel, UserPermissionParameterModel>(parameter);
        }

        public Task<List<Permission>> GetListForUserAsync(int userId, int storeId)
        {
            return _dataBaseExecutor.SelectListAsync<Permission>(Queries.GetListForUser, new { userId, storeId });
        }

        public Task<UserPermissionModel> GetAsync(int id)
        {
            return _dao.GetAsync<UserPermissionModel>(id);
        }

        public Task<UserPermissionModel> GetForUserAsync(int userId, int storeId, Permission permission)
        {
            return _dataBaseExecutor.SelectOneAsync<UserPermissionModel>(Queries.GetForUser, new { userId, storeId, permission });
        }

        public Task<bool> IsExistAsync(int userId, int storeId)
        {
            return _dataBaseExecutor.SelectOneAsync<bool>(Queries.IsExist, new {userId, storeId});
        }

        public Task<int> CreateAsync(UserPermissionModel model)
        {
            return _dao.CreateAsync(model);
        }

        public Task CreateListAsync(List<UserPermissionModel> models)
        {
            return _dao.CreateListAsync(models);
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