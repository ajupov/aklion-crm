using System;
using Aklion.Infrastructure.Dao.Attributes;

namespace Aklion.Crm.Domain.ProductAttributeLink
{
    [Table("dbo.ProductAttributeLink as pal")]
    [Join("inner join dbo.Store as s on pal.StoreId = s.Id " +
          "inner join dbo.Product as p on pal.ProductId = p.Id " +
          "inner join dbo.ProductAttribute as pa on pal.AttributeId = pa.Id")]
    public class ProductAttributeLinkModel : ICloneable
    {
        [Column("pal.Id")]
        [Identificator]
        public int Id { get; set; }

        [Column("pal.StoreId")]
        public int StoreId { get; set; }

        [Column("s.Name")]
        public string StoreName { get; }

        [Column("pal.ProductId")]
        public int ProductId { get; set; }

        [Column("p.Name")]
        public string ProductName { get; }

        [Column("pal.AttributeId")]
        public int AttributeId { get; set; }

        [Column("pa.Name")]
        public string AttributeName { get; }

        [Column("pa.Description")]
        public string AttributeDescription { get; }

        [Column("pal.Value")]
        public string Value { get; set; }

        [Column("pal.IsDeleted")]
        public bool IsDeleted { get; set; }

        [Column("pal.CreateDate")]
        [CreateDate]
        public DateTime CreateDate { get; set; }

        [Column("pal.ModifyDate")]
        [ModifyDate]
        public DateTime? ModifyDate { get; set; }

        public object Clone()
        {
            return MemberwiseClone();
        }
    }
}