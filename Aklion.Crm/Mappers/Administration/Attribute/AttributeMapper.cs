using System.Collections.Generic;
using System.Linq;
using Aklion.Crm.Models;
using Aklion.Crm.Models.Administration.Attribute;
using Aklion.Infrastructure.Storage.DataBaseExecutor.Models;
using Aklion.Infrastructure.Utils.DateTime;
using AttributeParameterModel = Aklion.Crm.Domain.Attribute.AttributeParameterModel;

namespace Aklion.Crm.Mappers.Administration.Attribute
{
    public static class AttributeMapper
    {
        public static PagingModel<AttributeModel> Map(this Paging<Domain.Attribute.AttributeModel> model, int page, int size)
        {
            return model == null
                ? null
                : new PagingModel<AttributeModel>(model.List.Map(), model.TotalCount, page, size);
        }

        private static List<AttributeModel> Map(this IEnumerable<Domain.Attribute.AttributeModel> models)
        {
            return models?.Select(Map).ToList();
        }

        public static AttributeModel Map(this Domain.Attribute.AttributeModel model)
        {
            return model == null
                ? null
                : new AttributeModel
                {
                    Id = model.Id,
                    StoreId = model.StoreId,
                    StoreName = model.StoreName,
                    Name = model.Name,
                    IsDeleted = model.IsDeleted,
                    CreateDate = model.CreateDate,
                    ModifyDate = model.ModifyDate
                };
        }

        public static Domain.Attribute.AttributeModel Map(this AttributeModel model)
        {
            return model == null
                ? null
                : new Domain.Attribute.AttributeModel
                {
                    Id = model.Id,
                    StoreId = model.StoreId,
                    StoreName = model.StoreName,
                    Name = model.Name,
                    IsDeleted = model.IsDeleted,
                    CreateDate = model.CreateDate,
                    ModifyDate = model.ModifyDate
                };
        }

        public static AttributeParameterModel Map(this Models.Administration.Attribute.AttributeParameterModel model)
        {
            return model == null
                ? null
                : new AttributeParameterModel
                {
                    Id = model.Id,
                    Name = model.Name,
                    StoreId = model.StoreId,
                    StoreName = model.StoreName,
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

        public static void Map(this AttributeModel viewModel, Domain.Attribute.AttributeModel domainModel)
        {
            domainModel.Id = viewModel.Id;
            domainModel.Name = viewModel.Name;
            domainModel.StoreId = viewModel.StoreId;
            domainModel.StoreName = viewModel.StoreName;
            domainModel.IsDeleted = viewModel.IsDeleted;
            domainModel.CreateDate = viewModel.CreateDate;
            domainModel.ModifyDate = viewModel.ModifyDate;
        }
    }
}