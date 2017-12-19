using System.Collections.Generic;
using System.Linq;
using Aklion.Crm.Models;
using Aklion.Crm.Models.User.Store;
using Aklion.Infrastructure.DateTime;
using Aklion.Infrastructure.Storage.DataBaseExecutor.Pagingation;
using StoreParameterModel = Aklion.Crm.Models.Administration.Store.StoreParameterModel;

namespace Aklion.Crm.Mappers.User.Store
{
    public static class StoreMapper
    {
        public static PagingModel<StoreModel> Map(this Paging<Models.Administration.Store.StoreModel> model, int storeId, int page, int size)
        {
            return model == null
                ? null
                : new PagingModel<StoreModel>(model.List.Map(storeId), model.TotalCount, page, size);
        }

        private static List<StoreModel> Map(this IEnumerable<Models.Administration.Store.StoreModel> models, int storeId)
        {
            return models?.Select(x => x.Map(storeId)).ToList();
        }

        public static StoreModel Map(this Models.Administration.Store.StoreModel model, int storeId)
        {
            return model == null
                ? null
                : new StoreModel
                {
                    Id = model.Id,
                    Name = model.Name,
                    ApiSecret = model.ApiSecret,
                    CreateDate = model.CreateDate
                };
        }

        public static Models.Administration.Store.StoreModel Map(this StoreModel model, int userId, int storeId)
        {
            return model == null
                ? null
                : new Models.Administration.Store.StoreModel
                {
                    Id = model.Id,
                    CreateUserId = userId,
                    CreateUserLogin = null,
                    Name = model.Name,
                    ApiSecret = model.ApiSecret,
                    IsLocked = false,
                    IsDeleted = false,
                    CreateDate = model.CreateDate,
                    ModifyDate = null
                };
        }

        public static StoreParameterModel Map(this Models.User.Store.StoreParameterModel model, int userId, int storeId)
        {
            return model == null
                ? null
                : new StoreParameterModel
                {
                    Id = model.Id,
                    Name = model.Name,
                    CreateUserId = userId,
                    CreateUserLogin = null,
                    ApiSecret = model.ApiSecret,
                    IsLocked = false,
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

        public static void Map(this StoreModel viewModel, Models.Administration.Store.StoreModel domainModel, int userId, int storeId)
        {
            domainModel.Id = viewModel.Id;
            domainModel.Name = viewModel.Name;
            domainModel.CreateUserId = userId;
            domainModel.CreateUserLogin = null;
            domainModel.ApiSecret = viewModel.ApiSecret;
            domainModel.CreateDate = viewModel.CreateDate;
        }
    }
}