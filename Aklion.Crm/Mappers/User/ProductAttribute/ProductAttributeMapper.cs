using System.Collections.Generic;
using System.Linq;
using Aklion.Crm.Models;
using Aklion.Crm.Models.User.ProductAttribute;
using Aklion.Infrastructure.Storage.DataBaseExecutor.Models;
using Aklion.Infrastructure.Utils.DateTime;
using ProductAttributeParameterModel = Aklion.Crm.Domain.ProductAttribute.ProductAttributeParameterModel;

namespace Aklion.Crm.Mappers.User.ProductAttribute
{
    public static class ProductAttributeMapper
    {
        public static PagingModel<ProductAttributeModel> Map(this Paging<Domain.ProductAttribute.ProductAttributeModel> model, int storeId, int page, int size)
        {
            return model == null
                ? null
                : new PagingModel<ProductAttributeModel>(model.List.Map(storeId), model.TotalCount, page, size);
        }

        private static List<ProductAttributeModel> Map(this IEnumerable<Domain.ProductAttribute.ProductAttributeModel> models, int storeId)
        {
            return models?.Select(x => x.Map(storeId)).ToList();
        }

        public static ProductAttributeModel Map(this Domain.ProductAttribute.ProductAttributeModel model, int storeId)
        {
            return model == null
                ? null
                : new ProductAttributeModel
                {
                    Id = model.Id,
                    ProductId = model.ProductId,
                    ProductName = model.ProductName,
                    AttributeId = model.AttributeId,
                    AttributeName = model.AttributeName,
                    Value = model.Value,
                    CreateDate = model.CreateDate
                };
        }

        public static Domain.ProductAttribute.ProductAttributeModel Map(this ProductAttributeModel model, int storeId)
        {
            return model == null
                ? null
                : new Domain.ProductAttribute.ProductAttributeModel
                {
                    Id = model.Id,
                    StoreId = storeId,
                    StoreName = null,
                    ProductId = model.ProductId,
                    ProductName = model.ProductName,
                    AttributeId = model.AttributeId,
                    AttributeName = model.AttributeName,
                    Value = model.Value,
                    IsDeleted = false,
                    CreateDate = model.CreateDate,
                    ModifyDate = null
                };
        }

        public static ProductAttributeParameterModel Map(this Models.User.ProductAttribute.ProductAttributeParameterModel model, int storeId)
        {
            return model == null
                ? null
                : new ProductAttributeParameterModel
                {
                    Id = model.Id,
                    StoreId = storeId,
                    StoreName = null,
                    ProductId = model.ProductId,
                    ProductName = model.ProductName,
                    AttributeId = model.AttributeId,
                    AttributeName = model.AttributeName,
                    Value = model.Value,
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

        public static void Map(this ProductAttributeModel viewModel, Domain.ProductAttribute.ProductAttributeModel domainModel, int storeId)
        {
            domainModel.Id = viewModel.Id;
            domainModel.StoreId = storeId;
            domainModel.StoreName = null;
            domainModel.ProductId = viewModel.ProductId;
            domainModel.ProductName = viewModel.ProductName;
            domainModel.AttributeId = viewModel.AttributeId;
            domainModel.AttributeName = viewModel.AttributeName;
            domainModel.Value = viewModel.Value;
            domainModel.CreateDate = viewModel.CreateDate;
        }
    }
}