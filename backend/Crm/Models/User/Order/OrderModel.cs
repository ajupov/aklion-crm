﻿using System.Collections.Generic;

namespace Crm.Models.User.Order
{
    public class OrderModel
    {
        public int Id { get; set; }

        public int ClientId { get; set; }

        public string ClientName { get; set; }

        public int SourceId { get; set; }

        public string SourceName { get; set; }

        public int StatusId { get; set; }

        public string StatusName { get; set; }

        public decimal TotalSum { get; set; }

        public decimal DiscountSum { get; set; }

        public bool IsDeleted { get; set; }

        public string CreateDate { get; set; }

        public List<Storages.Models.OrderAttributeLink> AttributeLinks { get; set; }

        public List<Storages.Models.OrderItem> OrderItems { get; set; }
    }
}