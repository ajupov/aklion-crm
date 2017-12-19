using System;
using Aklion.Infrastructure.Dao.Attributes;

namespace Aklion.Crm.Domain.OrderAttributeLink
{
    [Table("dbo.OrderAttributeLink as oal")]
    [Join("inner join dbo.Store as s on oal.StoreId = s.Id " +
          "inner join dbo.Order as c on oal.OrderId = c.Id " +
          "inner join dbo.OrderAttribute as oa on oal.AttributeId = oa.Id")]
    public class OrderAttributeLinkModel
    {
        [Column("oal.Id")]
        [Identificator]
        public int Id { get; set; }

        [Column("oal.StoreId")]
        public int StoreId { get; set; }

        [Column("s.Name")]
        public string StoreName { get; }

        [Column("oal.OrderId")]
        public int OrderId { get; set; }

        [Column("oal.AttributeId")]
        public int AttributeId { get; set; }

        [Column("oa.Name")]
        public string AttributeName { get; }

        [Column("oa.Description")]
        public string AttributeDescription { get; }

        [Column("oal.Value")]
        public string Value { get; set; }

        [Column("oal.IsDeleted")]
        public bool IsDeleted { get; set; }

        [Column("oal.CreateDate")]
        [CreateDate]
        public DateTime CreateDate { get; set; }

        [Column("oal.ModifyDate")]
        [ModifyDate]
        public DateTime? ModifyDate { get; set; }
    }
}