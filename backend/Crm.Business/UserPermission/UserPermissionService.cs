using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Crm.Business.Permission;
using Crm.Dao.Store;
using Crm.Dao.UserPermission;
using Crm.Domain.Store;
using Crm.Domain.UserPermission;

namespace Crm.Business.UserPermission
{
    public class UserPermissionService : IUserPermissionService
    {
        private readonly IPermissionService _permissionService;
        private readonly IUserPermissionDao _userPermissionDao;
        private readonly IStoreDao _storeDao;

        public UserPermissionService(
            IPermissionService permissionService,
            IUserPermissionDao userPermissionDao,
            IStoreDao storeDao)
        {
            _userPermissionDao = userPermissionDao;
            _permissionService = permissionService;
            _storeDao = storeDao;
        }

        public Task CreateForRegisteredUserAsync(int userId, int storeId)
        {
            var permissions = _permissionService.GetForRegistration();

            var now = DateTime.Now;

            var models = permissions.Select(p => new UserPermissionModel
            {
                UserId = userId,
                StoreId = storeId,
                CreateDate = now,
                ModifyDate = null,
                Permission = p
            }).ToList();

            return _userPermissionDao.CreateListAsync(models);
        }

        public async Task CreateForAddedUserAsync(int userId, int ownerUserId)
        {
            var parameter = new StoreByUserParameterModel
            {
                UserId = ownerUserId
            };

            var stores = await _storeDao.GetListAsync(parameter).ConfigureAwait(false);
            if (!stores.Any())
            {
                return;
            }

            var now = DateTime.Now;

            var models = stores.Select(s => new UserPermissionModel
            {
                UserId = userId,
                StoreId = s.Id,
                CreateDate = now,
                ModifyDate = null,
                Permission = Enums.Permission.None
            }).ToList();

            await _userPermissionDao.CreateListAsync(models).ConfigureAwait(false);
        }

        public async Task<List<UserPermissionExistModel>> GetForUserAsync(int userId, int storeId)
        {
            var userPermissions = await _userPermissionDao.GetListForUserAsync(userId, storeId).ConfigureAwait(false);

            var allPermissions = _permissionService.GetForUser();

            return allPermissions.Select(p => new UserPermissionExistModel
            {
                Permission = p,
                IsExist = userPermissions.Contains(p)
            }).ToList();
        }

        public async Task SwitchAsync(int userId, int storeId, Enums.Permission permission)
        {
            var userPermission = await _userPermissionDao.GetForUserAsync(userId, storeId, permission).ConfigureAwait(false);
            if(userPermission == null)
            {
                userPermission = new UserPermissionModel
                {
                    Id = 0,
                    UserId = userId,
                    StoreId = storeId,
                    Permission = permission,
                    CreateDate = DateTime.Now,
                    ModifyDate = null
                };

                userPermission.Id = await _userPermissionDao.CreateAsync(userPermission).ConfigureAwait(false);
            }
            else
            {
                await _userPermissionDao.DeleteAsync(userPermission.Id).ConfigureAwait(false);
            }
        }
    }
}