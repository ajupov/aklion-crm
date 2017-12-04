using System.Collections.Generic;
using System.Linq;
using Aklion.Crm.Models;
using Aklion.Crm.Models.User.Category;
using Aklion.Infrastructure.Storage.DataBaseExecutor.Models;
using Aklion.Infrastructure.Utils.DateTime;
using CategoryParameterModel = Aklion.Crm.Domain.Category.CategoryParameterModel;

namespace Aklion.Crm.Mappers.User.Category
{
    public static class CategoryMapper
    {
        public static PagingModel<CategoryModel> Map(this Paging<Domain.Category.CategoryModel> model, int storeId, int page, int size)
        {
            return model == null
                ? null
                : new PagingModel<CategoryModel>(model.List.Map(storeId), model.TotalCount, page, size);
        }

        private static List<CategoryModel> Map(this IEnumerable<Domain.Category.CategoryModel> models, int storeId)
        {
            return models?.Select(x => x.Map(storeId)).ToList();
        }

        public static CategoryModel Map(this Domain.Category.CategoryModel model, int storeId)
        {
            return model == null
                ? null
                : new CategoryModel
                {
                    Id = model.Id,
                    Name = model.Name,
                    ParentId = model.ParentId,
                    ParentName = model.ParentName,
                    CreateDate = model.CreateDate,
                };
        }

        public static Domain.Category.CategoryModel Map(this CategoryModel model, int storeId)
        {
            return model == null
                ? null
                : new Domain.Category.CategoryModel
                {
                    Id = model.Id,
                    StoreId = storeId,
                    StoreName = null,
                    Name = model.Name,
                    ParentId = model.ParentId,
                    ParentName = model.ParentName,
                    IsDeleted = false,
                    CreateDate = model.CreateDate,
                    ModifyDate = null
                };
        }

        public static CategoryParameterModel Map(this Models.User.Category.CategoryParameterModel model, int storeId)
        {
            return model == null
                ? null
                : new CategoryParameterModel
                {
                    Id = model.Id,
                    Name = model.Name,
                    StoreId = storeId,
                    StoreName = null,
                    ParentId = model.ParentId,
                    ParentName = model.ParentName,
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

        public static void Map(this CategoryModel viewModel, Domain.Category.CategoryModel domainModel, int storeId)
        {
            domainModel.Id = viewModel.Id;
            domainModel.Name = viewModel.Name;
            domainModel.StoreId = storeId;
            domainModel.StoreName = null;
            domainModel.ParentId = viewModel.ParentId;
            domainModel.ParentName = viewModel.ParentName;
            domainModel.CreateDate = viewModel.CreateDate;
        }
    }
}