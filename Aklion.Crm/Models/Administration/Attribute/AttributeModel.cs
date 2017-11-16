using System;

namespace Aklion.Crm.Models.Administration.Attribute
{
    public class AttributeModel
    {
        public int Id { get; set; }

        public int StoreId { get; set; }

        public string StoreName { get; set; }

        public string Name { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime? ModifyDate { get; set; }
    }
}