using Crm.Enums;

namespace Crm.Storages.Models
{
    public class UserPermission
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public int StoreId { get; set; }

        public Permission Permission { get; set; }
    }
}