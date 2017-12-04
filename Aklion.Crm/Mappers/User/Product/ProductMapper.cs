using System.Collections.Generic;
using System.Linq;
using Aklion.Crm.Models;
using Aklion.Crm.Models.User.Product;
using Aklion.Infrastructure.Storage.DataBaseExecutor.Models;
using Aklion.Infrastructure.Utils.DateTime;
using ProductParameterModel = Aklion.Crm.Domain.Product.ProductParameterModel;

namespace Aklion.Crm.Mappers.User.Product
{
    public static class ProductMapper
    {
        public static PagingModel<ProductModel> Map(this Paging<Domain.Product.ProductModel> model, int storeId, int page, int size)
        {
            return model == null
                ? null
                : new PagingModel<ProductModel>(model.List.Map(storeId), model.TotalCount, page, size);
        }

        private static List<ProductModel> Map(this IEnumerable<Domain.Product.ProductModel> models, int storeId)
        {
            return models?.Select(x => x.Map(storeId)).ToList();
        }

        public static ProductModel Map(this Domain.Product.ProductModel model, int storeId)
        {
            return model == null
                ? null
                : new ProductModel
                {
                    Id = model.Id,
                    Type = model.Type,
                    Name = model.Name,
                    Description = model.Description,
                    Price = model.Price,
                    Status = model.Status,
                    VendorCode = model.VendorCode,
                    ParentId = model.ParentId,
                    ParentName = model.ParentName,
                    CreateDate = model.CreateDate
                };
        }

        public static Domain.Product.ProductModel Map(this ProductModel model, int storeId)
        {
            return model == null
                ? null
                : new Domain.Product.ProductModel
                {
                    Id = model.Id,
                    StoreId = storeId,
                    StoreName = null,
                    Type = model.Type,
                    Name = model.Name,
                    Description = model.Description,
                    Price = model.Price,
                    Status = model.Status,
                    VendorCode = model.VendorCode,
                    ParentId = model.ParentId,
                    ParentName = model.ParentName,
                    IsDeleted = false,
                    CreateDate = model.CreateDate,
                    ModifyDate = null
                };
        }

        public static ProductParameterModel Map(this Models.User.Product.ProductParameterModel model, int storeId)
        {
            return model == null
                ? null
                : new ProductParameterModel
                {
                    Id = model.Id,
                    StoreId = storeId,
                    StoreName = null,
                    Type = model.Type,
                    Name = model.Name,
                    Description = model.Description,
                    Price = model.Price,
                    Status = model.Status,
                    VendorCode = model.VendorCode,
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

        public static void Map(this ProductModel viewModel, Domain.Product.ProductModel domainModel, int storeId)
        {
            domainModel.Id = viewModel.Id;
            domainModel.Name = viewModel.Name;
            domainModel.StoreId = storeId;
            domainModel.StoreName = null;
            domainModel.Type = viewModel.Type;
            domainModel.Name = viewModel.Name;
            domainModel.Description = viewModel.Description;
            domainModel.Price = viewModel.Price;
            domainModel.Status = viewModel.Status;
            domainModel.VendorCode = viewModel.VendorCode;
            domainModel.ParentId = viewModel.ParentId;
            domainModel.ParentName = viewModel.ParentName;
            domainModel.CreateDate = viewModel.CreateDate;
        }
    }
}