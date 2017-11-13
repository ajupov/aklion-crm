using System;
using Aklion.Crm.Enums;

namespace Aklion.Crm.Domain.UserPermission
{
    public class UserPermissionModel
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public string UserLogin { get; set; }

        public int StoreId { get; set; }

        public string StoreName { get; set; }

        public Permission Permission { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime? ModifyDate { get; set; }
    }
}