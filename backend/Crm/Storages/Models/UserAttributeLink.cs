using System;

namespace Crm.Storages.Models
{
    public class UserAttributeLink
    {
        public int Id { get; set; }

        public int StoreId { get; set; }

        public Store Store { get; set; }

        public int UserId { get; set; }

        public User User { get; set; }

        public int AttributeId { get; set; }

        public UserAttribute Attribute { get; set; }

        public string Value { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime? ModifyDate { get; set; }
    }
}