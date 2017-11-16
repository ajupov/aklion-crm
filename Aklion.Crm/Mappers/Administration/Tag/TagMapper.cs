using System.Collections.Generic;
using System.Linq;
using Aklion.Crm.Models;
using Aklion.Crm.Models.Administration.Tag;
using Aklion.Infrastructure.Storage.DataBaseExecutor.Models;
using Aklion.Infrastructure.Utils.DateTime;
using TagParameterModel = Aklion.Crm.Domain.Tag.TagParameterModel;

namespace Aklion.Crm.Mappers.Administration.Tag
{
    public static class TagMapper
    {
        public static PagingModel<TagModel> Map(this Paging<Domain.Tag.TagModel> model, int page, int size)
        {
            return model == null
                ? null
                : new PagingModel<TagModel>(model.List.Map(), model.TotalCount, page, size);
        }

        private static List<TagModel> Map(this IEnumerable<Domain.Tag.TagModel> models)
        {
            return models?.Select(Map).ToList();
        }

        public static TagModel Map(this Domain.Tag.TagModel model)
        {
            return model == null
                ? null
                : new TagModel
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

        public static Domain.Tag.TagModel Map(this TagModel model)
        {
            return model == null
                ? null
                : new Domain.Tag.TagModel
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

        public static TagParameterModel Map(this Models.Administration.Tag.TagParameterModel model)
        {
            return model == null
                ? null
                : new TagParameterModel
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

        public static void Map(this TagModel viewModel, Domain.Tag.TagModel domainModel)
        {
            domainModel.Id = viewModel.Id;
            domainModel.Name = viewModel.Name;
            domainModel.StoreId = viewModel.StoreId;
            domainModel.StoreName = viewModel.StoreName;
            domainModel.IsDeleted = viewModel.IsDeleted;
            domainModel.CreateDate = viewModel.CreateDate;
            domainModel.ModifyDate = viewModel.ModifyDate;
        }
    }
}