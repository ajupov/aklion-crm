using System;
using System.Collections;
using System.Collections.Generic;

namespace Crm.Storages.Models
{
    public class Client
    {
        public int Id { get; set; }

        public int StoreId { get; set; }

        public Store Store { get; set; }

        public string Name { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime? ModifyDate { get; set; }

        public List<ClientAttributeLink> ClientAttributeLinks { get; set; }
    }
}