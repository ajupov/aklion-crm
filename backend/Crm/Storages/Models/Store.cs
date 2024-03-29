﻿using System;

namespace Crm.Storages.Models
{
    public class Store
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string ApiSecret { get; set; }

        public bool IsLocked { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime? ModifyDate { get; set; }
    }
}