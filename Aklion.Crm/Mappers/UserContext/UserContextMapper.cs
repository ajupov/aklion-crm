using System.Collections.Generic;
using Aklion.Infrastructure.Mapper;
using DomainUserAvialableStoreModel = Aklion.Crm.Domain.UserContext.UserAvialableStoreModel;
using UserAvialableStore = Aklion.Crm.UserContext.UserAvialableStore;

namespace Aklion.Crm.Mappers.UserContext
{
    public static class UserContextMapper
    {
        public static List<UserAvialableStore> Map(this List<DomainUserAvialableStoreModel> models)
        {
            return models.MapListNew<UserAvialableStore>();
        }
    }
}