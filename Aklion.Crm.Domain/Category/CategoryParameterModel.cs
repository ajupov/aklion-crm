﻿using System;
using Aklion.Infrastructure.Storage.DataBaseExecutor.Models;

namespace Aklion.Crm.Domain.Category
{
    public class CategoryParameterModel : ParameterModel
    {
        public int? Id { get; set; }

        public int? StoreId { get; set; }

        public string StoreName { get; set; }

        public string Name { get; set; }

        public bool? IsLocked { get; set; }

        public bool? IsDeleted { get; set; }

        public DateTime? CreateDate { get; set; }

        public DateTime? ModifyDate { get; set; }
    }
}