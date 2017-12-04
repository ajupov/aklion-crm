using System.Collections.Generic;
using System.Linq;
using Aklion.Crm.Models;
using Aklion.Crm.Models.User.Attribute;
using Aklion.Infrastructure.Storage.DataBaseExecutor.Models;
using Aklion.Infrastructure.Utils.DateTime;
using AttributeParameterModel = Aklion.Crm.Domain.Attribute.AttributeParameterModel;

namespace Aklion.Crm.Mappers.User.Attribute
{
    public static class AttributeMapper
    {
        public static PagingModel<AttributeModel> Map(this Paging<Domain.Attribute.AttributeModel> model, int storeId, int page, int size)
        {
            return model == null
                ? null
                : new PagingModel<AttributeModel>(model.List.Map(storeId), model.TotalCount, page, size);
        }

        private static List<AttributeModel> Map(this IEnumerable<Domain.Attribute.AttributeModel> models, int storeId)
        {
            return models?.Select(x => x.Map(storeId)).ToList();
        }

        public static AttributeModel Map(this Domain.Attribute.AttributeModel model, int storeId)
        {
            return model == null
                ? null
                : new AttributeModel
                {
                    Id = model.Id,
                    Name = model.Name,
                    CreateDate = model.CreateDate
                };
        }

        public static Domain.Attribute.AttributeModel Map(this AttributeModel model, int storeId)
        {
            return model == null
                ? null
                : new Domain.Attribute.AttributeModel
                {
                    Id = model.Id,
                    StoreId = storeId,
                    StoreName = null,
                    Name = model.Name,
                    IsDeleted = false,
                    CreateDate = model.CreateDate,
                    ModifyDate = null
                };
        }

        public static AttributeParameterModel Map(this Models.User.Attribute.AttributeParameterModel model, int storeId)
        {
            return model == null
                ? null
                : new AttributeParameterModel
                {
                    Id = model.Id,
                    Name = model.Name,
                    StoreId = storeId,
                    StoreName = null,
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

        public static void Map(this AttributeModel viewModel, Domain.Attribute.AttributeModel domainModel, int storeId)
        {
            domainModel.Id = viewModel.Id;
            domainModel.StoreId = storeId;
            domainModel.StoreName = null;
            domainModel.Name = viewModel.Name;
            domainModel.CreateDate = viewModel.CreateDate;
        }
    }
}