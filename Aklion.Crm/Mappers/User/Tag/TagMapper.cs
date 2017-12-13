using System.Collections.Generic;
using System.Linq;
using Aklion.Crm.Models;
using Aklion.Crm.Models.User.Tag;
using Aklion.Infrastructure.DateTime;
using Aklion.Infrastructure.Storage.DataBaseExecutor.Pagingation;
using TagParameterModel = Aklion.Crm.Domain.Tag.TagParameterModel;

namespace Aklion.Crm.Mappers.User.Tag
{
    public static class TagMapper
    {
        public static PagingModel<TagModel> Map(this Paging<Domain.Tag.TagModel> model, int storeId, int page, int size)
        {
            return model == null
                ? null
                : new PagingModel<TagModel>(model.List.Map(storeId), model.TotalCount, page, size);
        }

        private static List<TagModel> Map(this IEnumerable<Domain.Tag.TagModel> models, int storeId)
        {
            return models?.Select(x => x.Map(storeId)).ToList();
        }

        public static TagModel Map(this Domain.Tag.TagModel model, int storeId)
        {
            return model == null
                ? null
                : new TagModel
                {
                    Id = model.Id,
                    Name = model.Name,
                    IsDeleted = model.IsDeleted,
                    CreateDate = model.CreateDate
                };
        }

        public static Domain.Tag.TagModel Map(this TagModel model, int storeId)
        {
            return model == null
                ? null
                : new Domain.Tag.TagModel
                {
                    Id = model.Id,
                    StoreId = storeId,
                    StoreName = null,
                    Name = model.Name,
                    IsDeleted = model.IsDeleted,
                    CreateDate = model.CreateDate,
                    ModifyDate = null
                };
        }

        public static TagParameterModel Map(this Models.User.Tag.TagParameterModel model, int storeId)
        {
            return model == null
                ? null
                : new TagParameterModel
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

        public static void Map(this TagModel viewModel, Domain.Tag.TagModel domainModel, int storeId)
        {
            domainModel.Id = viewModel.Id;
            domainModel.Name = viewModel.Name;
            domainModel.StoreId = storeId;
            domainModel.StoreName = null;
            domainModel.IsDeleted = viewModel.IsDeleted;
            domainModel.CreateDate = viewModel.CreateDate;
        }
    }
}