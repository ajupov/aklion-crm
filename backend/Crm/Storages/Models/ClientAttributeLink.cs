using System;

namespace Crm.Storages.Models
{
    public class ClientAttributeLink
    {
        public int Id { get; set; }

        public int StoreId { get; set; }

        public Store Store { get; set; }

        public int ClientId { get; set; }

        public Client Client { get; set; }

        public int AttributeId { get; set; }

        public ClientAttribute Attribute { get; set; }

        public string Value { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime? ModifyDate { get; set; }
    }
}