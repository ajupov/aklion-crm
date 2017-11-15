using System.Collections.Generic;
using System.Linq;
using Aklion.Crm.Models;
using Aklion.Crm.Models.Administration.ProductCategory;
using Aklion.Infrastructure.Storage.DataBaseExecutor.Models;
using Aklion.Infrastructure.Utils.DateTime;
using ProductCategoryParameterModel = Aklion.Crm.Domain.ProductCategory.ProductCategoryParameterModel;

namespace Aklion.Crm.Mappers.Administration.ProductCategory
{
    public static class ProductCategoryMapper
    {
        public static PagingModel<ProductCategoryModel> Map(this Paging<Domain.ProductCategory.ProductCategoryModel> model, int page, int size)
        {
            return model == null
                ? null
                : new PagingModel<ProductCategoryModel>(model.List.Map(), model.TotalCount, page, size);
        }

        private static List<ProductCategoryModel> Map(this IEnumerable<Domain.ProductCategory.ProductCategoryModel> models)
        {
            return models?.Select(Map).ToList();
        }

        public static ProductCategoryModel Map(this Domain.ProductCategory.ProductCategoryModel model)
        {
            return model == null
                ? null
                : new ProductCategoryModel
                {
                    Id = model.Id,
                    StoreId = model.StoreId,
                    StoreName = model.StoreName,
                    ProductId = model.ProductId,
                    ProductName = model.ProductName,
                    CategoryId = model.CategoryId,
                    CategoryName = model.CategoryName,
                    IsDeleted = model.IsDeleted,
                    CreateDate = model.CreateDate,
                    ModifyDate = model.ModifyDate
                };
        }

        public static Domain.ProductCategory.ProductCategoryModel Map(this ProductCategoryModel model)
        {
            return model == null
                ? null
                : new Domain.ProductCategory.ProductCategoryModel
                {
                    Id = model.Id,
                    StoreId = model.StoreId,
                    StoreName = model.StoreName,
                    ProductId = model.ProductId,
                    ProductName = model.ProductName,
                    CategoryId = model.CategoryId,
                    CategoryName = model.CategoryName,
                    IsDeleted = model.IsDeleted,
                    CreateDate = model.CreateDate,
                    ModifyDate = model.ModifyDate
                };
        }

        public static ProductCategoryParameterModel Map(this Models.Administration.ProductCategory.ProductCategoryParameterModel model)
        {
            return model == null
                ? null
                : new ProductCategoryParameterModel
                {
                    Id = model.Id,
                    StoreId = model.StoreId,
                    StoreName = model.StoreName,
                    ProductId = model.ProductId,
                    ProductName = model.ProductName,
                    CategoryId = model.CategoryId,
                    CategoryName = model.CategoryName,
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

        public static void Map(this ProductCategoryModel viewModel, Domain.ProductCategory.ProductCategoryModel domainModel)
        {
            domainModel.Id = viewModel.Id;
            domainModel.StoreId = viewModel.StoreId;
            domainModel.StoreName = viewModel.StoreName;
            domainModel.ProductId = viewModel.ProductId;
            domainModel.ProductName = viewModel.ProductName;
            domainModel.CategoryId = viewModel.CategoryId;
            domainModel.CategoryName = viewModel.CategoryName;
            domainModel.IsDeleted = viewModel.IsDeleted;
            domainModel.CreateDate = viewModel.CreateDate;
            domainModel.ModifyDate = viewModel.ModifyDate;
        }
    }
}