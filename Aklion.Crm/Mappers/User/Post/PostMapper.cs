using System.Collections.Generic;
using System.Linq;
using Aklion.Crm.Models;
using Aklion.Crm.Models.User.Post;
using Aklion.Infrastructure.DateTime;
using Aklion.Infrastructure.Storage.DataBaseExecutor.Pagingation;
using PostParameterModel = Aklion.Crm.Domain.Post.PostParameterModel;

namespace Aklion.Crm.Mappers.User.Post
{
    public static class PostMapper
    {
        public static PagingModel<PostModel> Map(this Paging<Domain.Post.PostModel> model, int storeId, int page, int size)
        {
            return model == null
                ? null
                : new PagingModel<PostModel>(model.List.Map(storeId), model.TotalCount, page, size);
        }

        private static List<PostModel> Map(this IEnumerable<Domain.Post.PostModel> models, int storeId)
        {
            return models?.Select(x => x.Map(storeId)).ToList();
        }

        public static PostModel Map(this Domain.Post.PostModel model, int storeId)
        {
            return model == null
                ? null
                : new PostModel
                {
                    Id = model.Id,
                    Name = model.Name,
                    CreateDate = model.CreateDate
                };
        }

        public static Domain.Post.PostModel Map(this PostModel model, int storeId)
        {
            return model == null
                ? null
                : new Domain.Post.PostModel
                {
                    Id = model.Id,
                    StoreId = storeId,
                    StoreName = null,
                    Name = model.Name,
                    IsDeleted = false,
                    CreateDate = model.CreateDate,
                    ModifyDate = null
                };
        }

        public static PostParameterModel Map(this Models.User.Post.PostParameterModel model, int storeId)
        {
            return model == null
                ? null
                : new PostParameterModel
                {
                    Id = model.Id,
                    Name = model.Name,
                    StoreId = storeId,
                    StoreName = null,
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

        public static void Map(this PostModel viewModel, Domain.Post.PostModel domainModel, int storeId)
        {
            domainModel.Id = viewModel.Id;
            domainModel.Name = viewModel.Name;
            domainModel.StoreId = storeId;
            domainModel.StoreName = null;
            domainModel.CreateDate = viewModel.CreateDate;
        }
    }
}