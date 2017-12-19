using System.Collections.Generic;
using System.Linq;
using Aklion.Crm.Models;
using Aklion.Crm.Models.User.UserPermission;
using Aklion.Infrastructure.DateTime;
using Aklion.Infrastructure.Storage.DataBaseExecutor.Pagingation;
using UserPermissionParameterModel = Aklion.Crm.Models.Administration.UserPermission.UserPermissionParameterModel;

namespace Aklion.Crm.Mappers.User.UserPermission
{
    public static class UserPermissionMapper
    {
        public static PagingModel<UserPermissionModel> Map(this Paging<Models.Administration.UserPermission.UserPermissionModel> model, int storeId, int page, int size)
        {
            return model == null
                ? null
                : new PagingModel<UserPermissionModel>(model.List.Map(storeId), model.TotalCount, page, size);
        }

        private static List<UserPermissionModel> Map(this IEnumerable<Models.Administration.UserPermission.UserPermissionModel> models, int storeId)
        {
            return models?.Select(x => x.Map(storeId)).ToList();
        }

        public static UserPermissionModel Map(this Models.Administration.UserPermission.UserPermissionModel model, int storeId)
        {
            return model == null
                ? null
                : new UserPermissionModel
                {
                    Id = model.Id,
                    UserId = model.UserId,
                    UserLogin = model.UserLogin,
                    Permission = model.Permission,
                    CreateDate = model.CreateDate
                };
        }

        public static Models.Administration.UserPermission.UserPermissionModel Map(this UserPermissionModel model, int storeId)
        {
            return model == null
                ? null
                : new Models.Administration.UserPermission.UserPermissionModel
                {
                    Id = model.Id,
                    UserId = model.UserId,
                    UserLogin = model.UserLogin,
                    StoreId = storeId,
                    StoreName = null,
                    Permission = model.Permission,
                    CreateDate = model.CreateDate,
                    ModifyDate = null
                };
        }

        public static UserPermissionParameterModel Map(this Models.User.UserPermission.UserPermissionParameterModel model, int storeId)
        {
            return model == null
                ? null
                : new UserPermissionParameterModel
                {
                    Id = model.Id,
                    UserId = model.UserId,
                    UserLogin = model.UserLogin,
                    StoreId = storeId,
                    StoreName = null,
                    Permission = model.Permission,
                    CreateDate = model.CreateDate.ToNullableDate(),
                    ModifyDate = null,
                    IsSearch = model.IsSearch,
                    Timestamp = model.Timestamp,
                    SortingColumn = model.SortingColumn,
                    SortingOrder = model.SortingOrder,
                    Page = model.Page - 1,
                    Size = model.Size
                };
        }

        public static void Map(this UserPermissionModel viewModel, Models.Administration.UserPermission.UserPermissionModel domainModel, int storeId)
        {
            domainModel.Id = viewModel.Id;
            domainModel.UserId = viewModel.UserId;
            domainModel.UserLogin = viewModel.UserLogin;
            domainModel.StoreId = storeId;
            domainModel.StoreName = null;
            domainModel.Permission = viewModel.Permission;
            domainModel.CreateDate = viewModel.CreateDate;
            domainModel.ModifyDate = null;
        }
    }
}