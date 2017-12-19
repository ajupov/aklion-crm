using System.Collections.Generic;
using System.Linq;
using Aklion.Crm.Models;
using Aklion.Crm.Models.Administration.UserPermission;
using Aklion.Infrastructure.DateTime;
using Aklion.Infrastructure.Storage.DataBaseExecutor.Pagingation;
using UserPermissionParameterModel = Aklion.Crm.Models.Administration.UserPermission.UserPermissionParameterModel;

namespace Aklion.Crm.Mappers.Administration.UserPermission
{
    public static class UserPermissionMapper
    {
        public static PagingModel<UserPermissionModel> Map(this Paging<UserPermissionModel> model, int page, int size)
        {
            return model == null
                ? null
                : new PagingModel<UserPermissionModel>(model.List.Map(), model.TotalCount, page, size);
        }

        private static List<UserPermissionModel> Map(this IEnumerable<UserPermissionModel> models)
        {
            return models?.Select(Map).ToList();
        }

        public static UserPermissionModel Map(this UserPermissionModel model)
        {
            return model == null
                ? null
                : new UserPermissionModel
                {
                    Id = model.Id,
                    UserId = model.UserId,
                    UserLogin = model.UserLogin,
                    StoreId = model.StoreId,
                    StoreName = model.StoreName,
                    Permission = model.Permission,
                    CreateDate = model.CreateDate,
                    ModifyDate = model.ModifyDate
                };
        }

        public static UserPermissionModel Map(this UserPermissionModel model)
        {
            return model == null
                ? null
                : new UserPermissionModel
                {
                    Id = model.Id,
                    UserId = model.UserId,
                    UserLogin = model.UserLogin,
                    StoreId = model.StoreId,
                    StoreName = model.StoreName,
                    Permission = model.Permission,
                    CreateDate = model.CreateDate,
                    ModifyDate = model.ModifyDate
                };
        }

        public static UserPermissionParameterModel Map(this Models.Administration.UserPermission.UserPermissionParameterModel model)
        {
            return model == null
                ? null
                : new UserPermissionParameterModel
                {
                    Id = model.Id,
                    UserId = model.UserId,
                    UserLogin = model.UserLogin,
                    StoreId = model.StoreId,
                    StoreName = model.StoreName,
                    Permission = model.Permission,
                    CreateDate = model.CreateDate.ToNullableDate(),
                    ModifyDate = model.ModifyDate.ToNullableDate(),
                    IsSearch = model.IsSearch,
                    Timestamp = model.Timestamp,
                    SortingColumn = model.SortingColumn,
                    SortingOrder = model.SortingOrder,
                    Page = model.Page - 1,
                    Size = model.Size
                };
        }

        public static void Map(this UserPermissionModel viewModel, UserPermissionModel domainModel)
        {
            domainModel.Id = viewModel.Id;
            domainModel.UserId = viewModel.UserId;
            domainModel.UserLogin = viewModel.UserLogin;
            domainModel.StoreId = viewModel.StoreId;
            domainModel.StoreName = viewModel.StoreName;
            domainModel.Permission = viewModel.Permission;
            domainModel.CreateDate = viewModel.CreateDate;
            domainModel.ModifyDate = viewModel.ModifyDate;
        }
    }
}