using System;
using System.Collections.Generic;

namespace Crm.Storages.Models
{
    public class Order
    {
        public int Id { get; set; }

        public int StoreId { get; set; }

        public Store Store { get; set; }

        public int ClientId { get; set; }

        public Client Client { get; set; }

        public int SourceId { get; set; }

        public OrderSource Source { get; set; }

        public int StatusId { get; set; }

        public OrderStatus Status { get; set; }

        public decimal DiscountSum { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime? ModifyDate { get; set; }

        public List<OrderAttributeLink> AttributeLinks { get; set; }

        public List<OrderItem> OrderItems { get; set; }
    }
}