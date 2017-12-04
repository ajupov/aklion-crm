using System.Collections.Generic;
using System.Linq;
using Aklion.Crm.Models;
using Aklion.Crm.Models.User.ProductCategory;
using Aklion.Infrastructure.Storage.DataBaseExecutor.Models;
using Aklion.Infrastructure.Utils.DateTime;
using ProductCategoryParameterModel = Aklion.Crm.Domain.ProductCategory.ProductCategoryParameterModel;

namespace Aklion.Crm.Mappers.User.ProductCategory
{
    public static class ProductCategoryMapper
    {
        public static PagingModel<ProductCategoryModel> Map(this Paging<Domain.ProductCategory.ProductCategoryModel> model, int storeId, int page, int size)
        {
            return model == null
                ? null
                : new PagingModel<ProductCategoryModel>(model.List.Map(storeId), model.TotalCount, page, size);
        }

        private static List<ProductCategoryModel> Map(this IEnumerable<Domain.ProductCategory.ProductCategoryModel> models, int storeId)
        {
            return models?.Select(x => x.Map(storeId)).ToList();
        }

        public static ProductCategoryModel Map(this Domain.ProductCategory.ProductCategoryModel model, int storeId)
        {
            return model == null
                ? null
                : new ProductCategoryModel
                {
                    Id = model.Id,
                    ProductId = model.ProductId,
                    ProductName = model.ProductName,
                    CategoryId = model.CategoryId,
                    CategoryName = model.CategoryName,
                    CreateDate = model.CreateDate
                };
        }

        public static Domain.ProductCategory.ProductCategoryModel Map(this ProductCategoryModel model, int storeId)
        {
            return model == null
                ? null
                : new Domain.ProductCategory.ProductCategoryModel
                {
                    Id = model.Id,
                    StoreId = storeId,
                    StoreName = null,
                    ProductId = model.ProductId,
                    ProductName = model.ProductName,
                    CategoryId = model.CategoryId,
                    CategoryName = model.CategoryName,
                    IsDeleted = false,
                    CreateDate = model.CreateDate,
                    ModifyDate = null
                };
        }

        public static ProductCategoryParameterModel Map(this Models.User.ProductCategory.ProductCategoryParameterModel model, int storeId)
        {
            return model == null
                ? null
                : new ProductCategoryParameterModel
                {
                    Id = model.Id,
                    StoreId = storeId,
                    StoreName = null,
                    ProductId = model.ProductId,
                    ProductName = model.ProductName,
                    CategoryId = model.CategoryId,
                    CategoryName = model.CategoryName,
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

        public static void Map(this ProductCategoryModel viewModel, Domain.ProductCategory.ProductCategoryModel domainModel, int storeId)
        {
            domainModel.Id = viewModel.Id;
            domainModel.StoreId = storeId;
            domainModel.StoreName = null;
            domainModel.ProductId = viewModel.ProductId;
            domainModel.ProductName = viewModel.ProductName;
            domainModel.CategoryId = viewModel.CategoryId;
            domainModel.CategoryName = viewModel.CategoryName;
            domainModel.CreateDate = viewModel.CreateDate;
        }
    }
}