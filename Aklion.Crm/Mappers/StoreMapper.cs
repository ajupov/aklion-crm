using System.Collections.Generic;
using System.Linq;
using Aklion.Crm.Models.Stores;
using AppStore = Aklion.Crm.Models.Stores.Store;
using DaoStore = Aklion.Crm.Dao.Store.Models.Store;

namespace Aklion.Crm.Mappers
{
    public static class StoreMapper
    {
        public static List<AppStore> Map(this List<DaoStore> models)
        {
            return models?.Select(Map).ToList();
        }

        public static AppStore Map(this DaoStore model)
        {
            return model == null
                ? null
                : new AppStore
                {
                    Id = model.Id,
                    Name = model.Name,
                    ApiKey = model.ApiKey,
                    ApiSecret = model.ApiSecret,
                    IsLocked = model.IsLocked,
                    IsDeleted = model.IsDeleted,
                    CreateDate = model.CreateDate,
                    ModifyDate = model.ModifyDate
                };
        }

        public static DaoStore Map(this AddStore model)
        {
            return model == null
                ? null
                : new DaoStore
                {
                    Name = model.Name,
                    IsLocked = false,
                    IsDeleted = false,
                    ApiKey = null,
                    ApiSecret = null
                };
        }

        public static EditStore MapToEdit(this DaoStore model)
        {
            return model == null
                ? null
                : new EditStore
                {
                    Id = model.Id,
                    Name = model.Name
                };
        }
    }
}