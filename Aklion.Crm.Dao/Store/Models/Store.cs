﻿using System;

namespace Aklion.Crm.Dao.Store.Models
{
    public class Store
    {
        public int Id { get; set; }

        public int CreateUserId { get; set; }

        public string Name { get; set; }

        public string ApiKey { get; set; }

        public string ApiSecret { get; set; }

        public bool IsLocked { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime? ModifyDate { get; set; }
    }
}