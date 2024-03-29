﻿using Crm.Enums;

namespace Crm.Models.Administration.UserPermission
{
    public class UserPermissionModel
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public string UserLogin { get; set; }

        public int StoreId { get; set; }

        public string StoreName { get; set; }

        public Permission Permission { get; set; }

        public string CreateDate { get; set; }

        public string ModifyDate { get; set; }
    }
}