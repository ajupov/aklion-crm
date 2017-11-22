using System.Collections.Generic;
using Aklion.Crm.Enums;
using Aklion.Infrastructure.Utils.UserContext;

namespace Aklion.Crm.UserContext
{
    public class UserContext : IUserContext
    {
        public int UserId { get; set; }

        public string UserLogin { get; set; }

        public bool IsEmailConfirmed { get; set; }

        public bool IsPhoneConfirmed { get; set; }

        public bool IsLocked { get; set; }

        public bool IsDeleted { get; set; }

        public string AvatarUrl { get; set; }

        public int StoreId { get; set; }

        public int StoreName { get; set; }

        public bool StoreIsLocked { get; set; }

        public bool StoreIsDeleted { get; set; }

        public List<Permission> Permissions { get; set; }

        public Dictionary<int, string> AvialableStores { get; set; }
    }
}