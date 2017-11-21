using System.Collections.Generic;
using Aklion.Crm.Enums;
using Aklion.Infrastructure.Utils.UserContext;

namespace Aklion.Crm.Models
{
    public class UserContext : IUserContext
    {
        public int UserId { get; set; }

        public int StoreId { get; set; }

        public List<Permission> Permissions { get; set; }
    }
}