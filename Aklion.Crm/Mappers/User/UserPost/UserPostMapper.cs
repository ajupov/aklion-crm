using System.Collections.Generic;
using System.Linq;
using Aklion.Crm.Models;
using Aklion.Crm.Models.User.UserPost;
using Aklion.Infrastructure.DateTime;
using Aklion.Infrastructure.Storage.DataBaseExecutor.Pagingation;
using UserPostParameterModel = Aklion.Crm.Domain.UserPost.UserPostParameterModel;

namespace Aklion.Crm.Mappers.User.UserPost
{
    public static class UserPostMapper
    {
        public static PagingModel<UserPostModel> Map(this Paging<Domain.UserPost.UserPostModel> model, int storeId, int page, int size)
        {
            return model == null
                ? null
                : new PagingModel<UserPostModel>(model.List.Map(storeId), model.TotalCount, page, size);
        }

        private static List<UserPostModel> Map(this IEnumerable<Domain.UserPost.UserPostModel> models, int storeId)
        {
            return models?.Select(x => x.Map(storeId)).ToList();
        }

        public static UserPostModel Map(this Domain.UserPost.UserPostModel model, int storeId)
        {
            return model == null
                ? null
                : new UserPostModel
                {
                    Id = model.Id,
                    UserId = model.UserId,
                    UserLogin = model.UserLogin,
                    PostId = model.PostId,
                    PostName = model.PostName,
                    CreateDate = model.CreateDate
                };
        }

        public static Domain.UserPost.UserPostModel Map(this UserPostModel model, int storeId)
        {
            return model == null
                ? null
                : new Domain.UserPost.UserPostModel
                {
                    Id = model.Id,
                    UserId = model.UserId,
                    UserLogin = model.UserLogin,
                    StoreId = storeId,
                    StoreName = null,
                    PostId = model.PostId,
                    PostName = model.PostName,
                    IsDeleted = false,
                    CreateDate = model.CreateDate,
                    ModifyDate = null
                };
        }

        public static UserPostParameterModel Map(this Models.User.UserPost.UserPostParameterModel model, int storeId)
        {
            return model == null
                ? null
                : new UserPostParameterModel
                {
                    Id = model.Id,
                    UserId = model.UserId,
                    UserLogin = model.UserLogin,
                    StoreId = storeId,
                    StoreName = null,
                    PostId = model.PostId,
                    PostName = model.PostName,
                    IsDeleted = false,
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

        public static void Map(this UserPostModel viewModel, Domain.UserPost.UserPostModel domainModel, int storeId)
        {
            domainModel.Id = viewModel.Id;
            domainModel.UserId = viewModel.UserId;
            domainModel.UserLogin = viewModel.UserLogin;
            domainModel.StoreId = storeId;
            domainModel.StoreName = null;
            domainModel.PostId = viewModel.PostId;
            domainModel.PostName = viewModel.PostName;
            domainModel.CreateDate = viewModel.CreateDate;
        }
    }
}