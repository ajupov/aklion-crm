using System.Collections.Generic;
using System.Linq;
using Aklion.Crm.Models;
using Aklion.Crm.Models.Administration.ProductTag;
using Aklion.Infrastructure.DateTime;
using Aklion.Infrastructure.Storage.DataBaseExecutor.Pagingation;
using ProductTagParameterModel = Aklion.Crm.Domain.ProductTag.ProductTagParameterModel;

namespace Aklion.Crm.Mappers.Administration.ProductTag
{
    public static class ProductTagMapper
    {
        public static PagingModel<ProductTagModel> Map(this Paging<Domain.ProductTag.ProductTagModel> model, int page, int size)
        {
            return model == null
                ? null
                : new PagingModel<ProductTagModel>(model.List.Map(), model.TotalCount, page, size);
        }

        private static List<ProductTagModel> Map(this IEnumerable<Domain.ProductTag.ProductTagModel> models)
        {
            return models?.Select(Map).ToList();
        }

        public static ProductTagModel Map(this Domain.ProductTag.ProductTagModel model)
        {
            return model == null
                ? null
                : new ProductTagModel
                {
                    Id = model.Id,
                    StoreId = model.StoreId,
                    StoreName = model.StoreName,
                    ProductId = model.ProductId,
                    ProductName = model.ProductName,
                    TagId = model.TagId,
                    TagName = model.TagName,
                    IsDeleted = model.IsDeleted,
                    CreateDate = model.CreateDate,
                    ModifyDate = model.ModifyDate
                };
        }

        public static Domain.ProductTag.ProductTagModel Map(this ProductTagModel model)
        {
            return model == null
                ? null
                : new Domain.ProductTag.ProductTagModel
                {
                    Id = model.Id,
                    StoreId = model.StoreId,
                    StoreName = model.StoreName,
                    ProductId = model.ProductId,
                    ProductName = model.ProductName,
                    TagId = model.TagId,
                    TagName = model.TagName,
                    IsDeleted = model.IsDeleted,
                    CreateDate = model.CreateDate,
                    ModifyDate = model.ModifyDate
                };
        }

        public static ProductTagParameterModel Map(this Models.Administration.ProductTag.ProductTagParameterModel model)
        {
            return model == null
                ? null
                : new ProductTagParameterModel
                {
                    Id = model.Id,
                    StoreId = model.StoreId,
                    StoreName = model.StoreName,
                    ProductId = model.ProductId,
                    ProductName = model.ProductName,
                    TagId = model.TagId,
                    TagName = model.TagName,
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

        public static void Map(this ProductTagModel viewModel, Domain.ProductTag.ProductTagModel domainModel)
        {
            domainModel.Id = viewModel.Id;
            domainModel.StoreId = viewModel.StoreId;
            domainModel.StoreName = viewModel.StoreName;
            domainModel.ProductId = viewModel.ProductId;
            domainModel.ProductName = viewModel.ProductName;
            domainModel.TagId = viewModel.TagId;
            domainModel.TagName = viewModel.TagName;
            domainModel.IsDeleted = viewModel.IsDeleted;
            domainModel.CreateDate = viewModel.CreateDate;
            domainModel.ModifyDate = viewModel.ModifyDate;
        }
    }
}