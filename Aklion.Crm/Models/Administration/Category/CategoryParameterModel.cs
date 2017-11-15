﻿namespace Aklion.Crm.Models.Administration.Category
{
    public class CategoryParameterModel : ParameterModel
    {
        public int? Id { get; set; }

        public int? StoreId { get; set; }

        public string StoreName { get; set; }

        public string Name { get; set; }

        public int? ParentId { get; set; }

        public string ParentName { get; set; }

        public bool? IsDeleted { get; set; }

        public string CreateDate { get; set; }

        public string ModifyDate { get; set; }
    }
}