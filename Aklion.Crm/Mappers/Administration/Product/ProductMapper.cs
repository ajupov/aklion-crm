using System.Collections.Generic;
using System.Linq;
using Aklion.Crm.Models;
using Aklion.Crm.Models.Administration.Product;
using Aklion.Infrastructure.Storage.DataBaseExecutor.Models;
using Aklion.Infrastructure.Utils.DateTime;
using ProductParameterModel = Aklion.Crm.Domain.Product.ProductParameterModel;

namespace Aklion.Crm.Mappers.Administration.Product
{
    public static class ProductMapper
    {
        public static PagingModel<ProductModel> Map(this Paging<Domain.Product.ProductModel> model, int page, int size)
        {
            return model == null
                ? null
                : new PagingModel<ProductModel>(model.List.Map(), model.TotalCount, page, size);
        }

        private static List<ProductModel> Map(this IEnumerable<Domain.Product.ProductModel> models)
        {
            return models?.Select(Map).ToList();
        }

        public static ProductModel Map(this Domain.Product.ProductModel model)
        {
            return model == null
                ? null
                : new ProductModel
                {
                    Id = model.Id,
                    StoreId = model.StoreId,
                    StoreName = model.StoreName,
                    Type = model.Type,
                    Name = model.Name,
                    Description = model.Description,
                    Price = model.Price,
                    Status = model.Status,
                    VendorCode = model.VendorCode,
                    ParentId = model.ParentId,
                    ParentName = model.ParentName,
                    IsDeleted = model.IsDeleted,
                    CreateDate = model.CreateDate,
                    ModifyDate = model.ModifyDate
                };
        }

        public static Domain.Product.ProductModel Map(this ProductModel model)
        {
            return model == null
                ? null
                : new Domain.Product.ProductModel
                {
                    Id = model.Id,
                    StoreId = model.StoreId,
                    StoreName = model.StoreName,
                    Type = model.Type,
                    Name = model.Name,
                    Description = model.Description,
                    Price = model.Price,
                    Status = model.Status,
                    VendorCode = model.VendorCode,
                    ParentId = model.ParentId,
                    ParentName = model.ParentName,
                    IsDeleted = model.IsDeleted,
                    CreateDate = model.CreateDate,
                    ModifyDate = model.ModifyDate
                };
        }

        public static ProductParameterModel Map(this Models.Administration.Product.ProductParameterModel model)
        {
            return model == null
                ? null
                : new ProductParameterModel
                {
                    Id = model.Id,
                    StoreId = model.StoreId,
                    StoreName = model.StoreName,
                    Type = model.Type,
                    Name = model.Name,
                    Description = model.Description,
                    Price = model.Price,
                    Status = model.Status,
                    VendorCode = model.VendorCode,
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

        public static void Map(this ProductModel viewModel, Domain.Product.ProductModel domainModel)
        {
            domainModel.Id = viewModel.Id;
            domainModel.Name = viewModel.Name;
            domainModel.StoreId = viewModel.StoreId;
            domainModel.StoreName = viewModel.StoreName;
            domainModel.Type = viewModel.Type;
            domainModel.Name = viewModel.Name;
            domainModel.Description = viewModel.Description;
            domainModel.Price = viewModel.Price;
            domainModel.Status = viewModel.Status;
            domainModel.VendorCode = viewModel.VendorCode;
            domainModel.ParentId = viewModel.ParentId;
            domainModel.ParentName = viewModel.ParentName;
            domainModel.IsDeleted = viewModel.IsDeleted;
            domainModel.IsDeleted = viewModel.IsDeleted;
            domainModel.CreateDate = viewModel.CreateDate;
            domainModel.ModifyDate = viewModel.ModifyDate;
        }
    }
}