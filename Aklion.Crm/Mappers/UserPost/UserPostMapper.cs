using System.Collections.Generic;
using System.Linq;
using Aklion.Crm.Models;
using Aklion.Crm.Models.Administration.UserPost;
using Aklion.Infrastructure.Storage.DataBaseExecutor.Models;
using Aklion.Infrastructure.Utils.DateTime;
using UserPostParameterModel = Aklion.Crm.Domain.UserPost.UserPostParameterModel;

namespace Aklion.Crm.Mappers.UserPost
{
    public static class UserPostMapper
    {
        public static PagingModel<UserPostModel> Map(this Paging<Domain.UserPost.UserPostModel> model, int page, int size)
        {
            return model == null
                ? null
                : new PagingModel<UserPostModel>(model.List.Map(), model.TotalCount, page, size);
        }

        private static List<UserPostModel> Map(this IEnumerable<Domain.UserPost.UserPostModel> models)
        {
            return models?.Select(Map).ToList();
        }

        public static UserPostModel Map(this Domain.UserPost.UserPostModel model)
        {
            return model == null
                ? null
                : new UserPostModel
                {
                    Id = model.Id,
                    UserId = model.UserId,
                    UserLogin = model.UserLogin,
                    StoreId = model.StoreId,
                    StoreName = model.StoreName,
                    PostId = model.PostId,
                    PostName = model.PostName,
                    IsDeleted = model.IsDeleted,
                    CreateDate = model.CreateDate,
                    ModifyDate = model.ModifyDate
                };
        }

        public static Domain.UserPost.UserPostModel Map(this UserPostModel model)
        {
            return model == null
                ? null
                : new Domain.UserPost.UserPostModel
                {
                    Id = model.Id,
                    UserId = model.UserId,
                    UserLogin = model.UserLogin,
                    StoreId = model.StoreId,
                    StoreName = model.StoreName,
                    PostId = model.PostId,
                    PostName = model.PostName,
                    IsDeleted = model.IsDeleted,
                    CreateDate = model.CreateDate,
                    ModifyDate = model.ModifyDate
                };
        }

        public static UserPostParameterModel Map(this Models.Administration.UserPost.UserPostParameterModel model)
        {
            return model == null
                ? null
                : new UserPostParameterModel
                {
                    Id = model.Id,
                    UserId = model.UserId,
                    UserLogin = model.UserLogin,
                    StoreId = model.StoreId,
                    StoreName = model.StoreName,
                    PostId = model.PostId,
                    PostName = model.PostName,
                    IsDeleted = model.IsDeleted,
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

        public static void Map(this UserPostModel viewModel, Domain.UserPost.UserPostModel domainModel)
        {
            domainModel.Id = viewModel.Id;
            domainModel.UserId = viewModel.UserId;
            domainModel.UserLogin = viewModel.UserLogin;
            domainModel.StoreId = viewModel.StoreId;
            domainModel.StoreName = viewModel.StoreName;
            domainModel.PostId = viewModel.PostId;
            domainModel.PostName = viewModel.PostName;
            domainModel.IsDeleted = viewModel.IsDeleted;
            domainModel.CreateDate = viewModel.CreateDate;
            domainModel.ModifyDate = viewModel.ModifyDate;
        }
    }
}