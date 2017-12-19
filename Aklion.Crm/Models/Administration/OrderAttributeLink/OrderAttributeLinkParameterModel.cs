﻿namespace Aklion.Crm.Models.Administration.OrderAttributeLink
{
    public class OrderAttributeLinkParameterModel
    {
        public int? Id { get; set; }

        public int? StoreId { get; set; }

        public string StoreName { get; set; }

        public int? OrderId { get; set; }

        public int? AttributeId { get; set; }

        public string AttributeName { get; set; }

        public string AttributeDescription { get; set; }

        public string Value { get; set; }

        public bool? IsDeleted { get; set; }

        public string CreateDate { get; set; }

        public string ModifyDate { get; set; }

        public string SortingColumn { get; set; }

        public string SortingOrder { get; set; }

        public int? Page { get; set; }

        public int? Size { get; set; }
    }
}