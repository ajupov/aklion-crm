using System.Collections.Generic;
using System.Linq;
using Aklion.Crm.Models;
using Aklion.Crm.Models.Administration.Post;
using Aklion.Infrastructure.Storage.DataBaseExecutor.Models;
using Aklion.Infrastructure.Utils.DateTime;
using PostParameterModel = Aklion.Crm.Domain.Post.PostParameterModel;

namespace Aklion.Crm.Mappers.Administration.Post
{
    public static class PostMapper
    {
        public static PagingModel<PostModel> Map(this Paging<Domain.Post.PostModel> model, int page, int size)
        {
            return model == null
                ? null
                : new PagingModel<PostModel>(model.List.Map(), model.TotalCount, page, size);
        }

        private static List<PostModel> Map(this IEnumerable<Domain.Post.PostModel> models)
        {
            return models?.Select(Map).ToList();
        }

        public static PostModel Map(this Domain.Post.PostModel model)
        {
            return model == null
                ? null
                : new PostModel
                {
                    Id = model.Id,
                    StoreId = model.StoreId,
                    StoreName = model.StoreName,
                    Name = model.Name,
                    IsDeleted = model.IsDeleted,
                    CreateDate = model.CreateDate,
                    ModifyDate = model.ModifyDate
                };
        }

        public static Domain.Post.PostModel Map(this PostModel model)
        {
            return model == null
                ? null
                : new Domain.Post.PostModel
                {
                    Id = model.Id,
                    StoreId = model.StoreId,
                    StoreName = model.StoreName,
                    Name = model.Name,
                    IsDeleted = model.IsDeleted,
                    CreateDate = model.CreateDate,
                    ModifyDate = model.ModifyDate
                };
        }

        public static PostParameterModel Map(this Models.Administration.Post.PostParameterModel model)
        {
            return model == null
                ? null
                : new PostParameterModel
                {
                    Id = model.Id,
                    Name = model.Name,
                    StoreId = model.StoreId,
                    StoreName = model.StoreName,
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

        public static void Map(this PostModel viewModel, Domain.Post.PostModel domainModel)
        {
            domainModel.Id = viewModel.Id;
            domainModel.Name = viewModel.Name;
            domainModel.StoreId = viewModel.StoreId;
            domainModel.IsDeleted = viewModel.IsDeleted;
            domainModel.CreateDate = viewModel.CreateDate;
            domainModel.ModifyDate = viewModel.ModifyDate;
        }
    }
}