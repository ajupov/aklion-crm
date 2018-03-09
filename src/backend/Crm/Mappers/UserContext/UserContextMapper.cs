using System.Collections.Generic;
using Infrastructure.Mapper;
using DomainUserAvialableStoreModel = Crm.Domain.UserContext.UserAvialableStoreModel;
using UserAvialableStore = Crm.UserContext.UserAvialableStore;

namespace Crm.Mappers.UserContext
{
    public static class UserContextMapper
    {
        public static List<UserAvialableStore> Map(this List<DomainUserAvialableStoreModel> models)
        {
            return models.MapListNew<UserAvialableStore>();
        }
    }
}