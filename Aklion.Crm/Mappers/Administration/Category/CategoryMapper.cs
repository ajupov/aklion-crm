using System.Collections.Generic;
using System.Linq;
using Aklion.Crm.Models;
using Aklion.Crm.Models.Administration.Category;
using Aklion.Infrastructure.Storage.DataBaseExecutor.Models;
using Aklion.Infrastructure.Utils.DateTime;
using CategoryParameterModel = Aklion.Crm.Domain.Category.CategoryParameterModel;

namespace Aklion.Crm.Mappers.Administration.Category
{
    public static class CategoryMapper
    {
        public static PagingModel<CategoryModel> Map(this Paging<Domain.Category.CategoryModel> model, int page, int size)
        {
            return model == null
                ? null
                : new PagingModel<CategoryModel>(model.List.Map(), model.TotalCount, page, size);
        }

        private static List<CategoryModel> Map(this IEnumerable<Domain.Category.CategoryModel> models)
        {
            return models?.Select(Map).ToList();
        }

        public static CategoryModel Map(this Domain.Category.CategoryModel model)
        {
            return model == null
                ? null
                : new CategoryModel
                {
                    Id = model.Id,
                    StoreId = model.StoreId,
                    StoreName = model.StoreName,
                    Name = model.Name,
                    ParentId = model.ParentId,
                    ParentName = model.ParentName,
                    IsDeleted = model.IsDeleted,
                    CreateDate = model.CreateDate,
                    ModifyDate = model.ModifyDate
                };
        }

        public static Domain.Category.CategoryModel Map(this CategoryModel model)
        {
            return model == null
                ? null
                : new Domain.Category.CategoryModel
                {
                    Id = model.Id,
                    StoreId = model.StoreId,
                    StoreName = model.StoreName,
                    Name = model.Name,
                    ParentId = model.ParentId,
                    ParentName = model.ParentName,
                    IsDeleted = model.IsDeleted,
                    CreateDate = model.CreateDate,
                    ModifyDate = model.ModifyDate
                };
        }

        public static CategoryParameterModel Map(this Models.Administration.Category.CategoryParameterModel model)
        {
            return model == null
                ? null
                : new CategoryParameterModel
                {
                    Id = model.Id,
                    Name = model.Name,
                    StoreId = model.StoreId,
                    StoreName = model.StoreName,
                    ParentId = model.ParentId,
                    ParentName = model.ParentName,
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

        public static void Map(this CategoryModel viewModel, Domain.Category.CategoryModel domainModel)
        {
            domainModel.Id = viewModel.Id;
            domainModel.Name = viewModel.Name;
            domainModel.StoreId = viewModel.StoreId;
            domainModel.ParentId = viewModel.ParentId;
            domainModel.ParentName = viewModel.ParentName;
            domainModel.IsDeleted = viewModel.IsDeleted;
            domainModel.CreateDate = viewModel.CreateDate;
            domainModel.ModifyDate = viewModel.ModifyDate;
        }
    }
}