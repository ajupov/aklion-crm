using System.Collections.Generic;
using Aklion.Crm.Enums;

namespace Aklion.Crm.UserContext
{
    public class UserContext
    {
        public int UserId { get; set; }

        public string UserLogin { get; set; }

        public bool IsEmailConfirmed { get; set; }

        public bool IsPhoneConfirmed { get; set; }

        public bool IsLocked { get; set; }

        public bool IsDeleted { get; set; }

        public string AvatarUrl { get; set; }

        public int StoreId { get; set; }

        public string StoreName { get; set; }

        public bool StoreIsLocked { get; set; }

        public bool StoreIsDeleted { get; set; }

        public List<Permission> Permissions { get; set; }
    }
}