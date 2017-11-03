using System.Collections.Generic;
using System.Linq;
using Aklion.Crm.Models;
using Aklion.Crm.Models.Administration.Store;
using Aklion.Infrastructure.Storage.DataBaseExecutor.Models;
using Aklion.Infrastructure.Utils.DateTime;
using StoreParameterModel = Aklion.Crm.Domain.Store.StoreParameterModel;

namespace Aklion.Crm.Mappers.Store
{
    public static class StoreMapper
    {
        public static PagingModel<StoreModel> Map(this Paging<Domain.Store.StoreModel> model, int page, int size)
        {
            return model == null
                ? null
                : new PagingModel<StoreModel>(model.List.Map(), model.TotalCount, page, size);
        }

        private static List<StoreModel> Map(this IEnumerable<Domain.Store.StoreModel> models)
        {
            return models?.Select(Map).ToList();
        }

        public static StoreModel Map(this Domain.Store.StoreModel model)
        {
            return model == null
                ? null
                : new StoreModel
                {
                    Id = model.Id,
                    CreateUserId = model.CreateUserId,
                    Name = model.Name,
                    ApiSecret = model.ApiSecret,
                    IsLocked = model.IsLocked,
                    IsDeleted = model.IsDeleted,
                    CreateDate = model.CreateDate,
                    ModifyDate = model.ModifyDate
                };
        }

        public static StoreParameterModel Map(this Models.Administration.Store.StoreParameterModel model)
        {
            return model == null
                ? null
                : new StoreParameterModel
                {
                    Id = model.Id,
                    Name = model.Name,
                    ApiSecret = model.ApiSecret,
                    IsLocked = model.IsLocked,
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

        public static void Map(this StoreModel viewModel, Domain.Store.StoreModel domainModel)
        {
            domainModel.Id = viewModel.Id;
            domainModel.Name = viewModel.Name;
            domainModel.ApiSecret = viewModel.ApiSecret;
            domainModel.IsLocked = viewModel.IsLocked;
            domainModel.IsDeleted = viewModel.IsDeleted;
            domainModel.CreateDate = viewModel.CreateDate;
            domainModel.ModifyDate = viewModel.ModifyDate;
        }
    }
}