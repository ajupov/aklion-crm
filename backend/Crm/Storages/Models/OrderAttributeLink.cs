using System;

namespace Crm.Storages.Models
{
    public class OrderAttributeLink
    {
        public int Id { get; set; }

        public int StoreId { get; set; }

        public Store Store { get; set; }

        public int OrderId { get; set; }

        public Order Order { get; set; }

        public int AttributeId { get; set; }

        public OrderAttribute Attribute { get; set; }

        public string Value { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime? ModifyDate { get; set; }
    }
}