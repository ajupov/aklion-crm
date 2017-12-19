using System.Collections.Generic;
using System.Linq;
using Aklion.Crm.Models;
using Aklion.Crm.Models.Administration.ProductAttribute;
using Aklion.Infrastructure.DateTime;
using Aklion.Infrastructure.Storage.DataBaseExecutor.Pagingation;
using ProductAttributeParameterModel = Aklion.Crm.Models.Administration.ProductAttribute.ProductAttributeParameterModel;

namespace Aklion.Crm.Mappers.Administration.ProductAttribute
{
    public static class ProductAttributeMapper
    {
        public static PagingModel<ProductAttributeModel> Map(this Paging<ProductAttributeModel> model, int page, int size)
        {
            return model == null
                ? null
                : new PagingModel<ProductAttributeModel>(model.List.Map(), model.TotalCount, page, size);
        }

        private static List<ProductAttributeModel> Map(this IEnumerable<ProductAttributeModel> models)
        {
            return models?.Select(Map).ToList();
        }

        public static ProductAttributeModel Map(this ProductAttributeModel model)
        {
            return model == null
                ? null
                : new ProductAttributeModel
                {
                    Id = model.Id,
                    StoreId = model.StoreId,
                    StoreName = model.StoreName,
                    ProductId = model.ProductId,
                    ProductName = model.ProductName,
                    AttributeId = model.AttributeId,
                    AttributeName = model.AttributeName,
                    Value = model.Value,
                    IsDeleted = model.IsDeleted,
                    CreateDate = model.CreateDate,
                    ModifyDate = model.ModifyDate
                };
        }

        public static ProductAttributeModel Map(this ProductAttributeModel model)
        {
            return model == null
                ? null
                : new ProductAttributeModel
                {
                    Id = model.Id,
                    StoreId = model.StoreId,
                    StoreName = model.StoreName,
                    ProductId = model.ProductId,
                    ProductName = model.ProductName,
                    AttributeId = model.AttributeId,
                    AttributeName = model.AttributeName,
                    Value = model.Value,
                    IsDeleted = model.IsDeleted,
                    CreateDate = model.CreateDate,
                    ModifyDate = model.ModifyDate
                };
        }

        public static ProductAttributeParameterModel Map(this Models.Administration.ProductAttribute.ProductAttributeParameterModel model)
        {
            return model == null
                ? null
                : new ProductAttributeParameterModel
                {
                    Id = model.Id,
                    StoreId = model.StoreId,
                    StoreName = model.StoreName,
                    ProductId = model.ProductId,
                    ProductName = model.ProductName,
                    AttributeId = model.AttributeId,
                    AttributeName = model.AttributeName,
                    Value = model.Value,
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

        public static void Map(this ProductAttributeModel viewModel, ProductAttributeModel domainModel)
        {
            domainModel.Id = viewModel.Id;
            domainModel.StoreId = viewModel.StoreId;
            domainModel.StoreName = viewModel.StoreName;
            domainModel.ProductId = viewModel.ProductId;
            domainModel.ProductName = viewModel.ProductName;
            domainModel.AttributeId = viewModel.AttributeId;
            domainModel.AttributeName = viewModel.AttributeName;
            domainModel.Value = viewModel.Value;
            domainModel.IsDeleted = viewModel.IsDeleted;
            domainModel.CreateDate = viewModel.CreateDate;
            domainModel.ModifyDate = viewModel.ModifyDate;
        }
    }
}