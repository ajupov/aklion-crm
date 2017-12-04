using System.Collections.Generic;
using System.Linq;
using Aklion.Crm.Models;
using Aklion.Crm.Models.User.ProductTag;
using Aklion.Infrastructure.Storage.DataBaseExecutor.Models;
using Aklion.Infrastructure.Utils.DateTime;
using ProductTagParameterModel = Aklion.Crm.Domain.ProductTag.ProductTagParameterModel;

namespace Aklion.Crm.Mappers.User.ProductTag
{
    public static class ProductTagMapper
    {
        public static PagingModel<ProductTagModel> Map(this Paging<Domain.ProductTag.ProductTagModel> model, int storeId, int page, int size)
        {
            return model == null
                ? null
                : new PagingModel<ProductTagModel>(model.List.Map(storeId), model.TotalCount, page, size);
        }

        private static List<ProductTagModel> Map(this IEnumerable<Domain.ProductTag.ProductTagModel> models, int storeId)
        {
            return models?.Select(x => x.Map(storeId)).ToList();
        }

        public static ProductTagModel Map(this Domain.ProductTag.ProductTagModel model, int storeId)
        {
            return model == null
                ? null
                : new ProductTagModel
                {
                    Id = model.Id,
                    ProductId = model.ProductId,
                    ProductName = model.ProductName,
                    TagId = model.TagId,
                    TagName = model.TagName,
                    CreateDate = model.CreateDate
                };
        }

        public static Domain.ProductTag.ProductTagModel Map(this ProductTagModel model, int storeId)
        {
            return model == null
                ? null
                : new Domain.ProductTag.ProductTagModel
                {
                    Id = model.Id,
                    StoreId = storeId,
                    StoreName = null,
                    ProductId = model.ProductId,
                    ProductName = model.ProductName,
                    TagId = model.TagId,
                    TagName = model.TagName,
                    IsDeleted = false,
                    CreateDate = model.CreateDate,
                    ModifyDate = null
                };
        }

        public static ProductTagParameterModel Map(this Models.User.ProductTag.ProductTagParameterModel model, int storeId)
        {
            return model == null
                ? null
                : new ProductTagParameterModel
                {
                    Id = model.Id,
                    StoreId = storeId,
                    StoreName = null,
                    ProductId = model.ProductId,
                    ProductName = model.ProductName,
                    TagId = model.TagId,
                    TagName = model.TagName,
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

        public static void Map(this ProductTagModel viewModel, Domain.ProductTag.ProductTagModel domainModel, int storeId)
        {
            domainModel.Id = viewModel.Id;
            domainModel.StoreId = storeId;
            domainModel.StoreName = null;
            domainModel.ProductId = viewModel.ProductId;
            domainModel.ProductName = viewModel.ProductName;
            domainModel.TagId = viewModel.TagId;
            domainModel.TagName = viewModel.TagName;
            domainModel.CreateDate = viewModel.CreateDate;
        }
    }
}