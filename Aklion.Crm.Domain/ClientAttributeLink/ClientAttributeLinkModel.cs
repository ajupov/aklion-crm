using System;
using Aklion.Infrastructure.Dao.Attributes;

namespace Aklion.Crm.Domain.ClientAttributeLink
{
    [Table("dbo.ClientAttributeLink as cal")]
    [Join("inner join dbo.Store as s on cal.StoreId = s.Id " +
          "inner join dbo.Client as c on cal.ClientId = c.Id " +
          "inner join dbo.ClientAttribute as ca on cal.AttributeId = ca.Id")]
    public class ClientAttributeLinkModel
    {
        [Column("cal.Id")]
        [Identificator]
        public int Id { get; }

        [Column("cal.StoreId")]
        public int StoreId { get; set; }

        [Column("s.Name")]
        public string StoreName { get; }

        [Column("cal.ClientId")]
        public int ClientId { get; set; }

        [Column("c.Name")]
        public string ClientName { get; }

        [Column("cal.AttributeId")]
        public int AttributeId { get; set; }

        [Column("ca.Name")]
        public string AttributeName { get; }

        [Column("ca.Description")]
        public string AttributeDescription { get; }

        [Column("cal.Value")]
        public string Value { get; set; }

        [Column("cal.IsDeleted")]
        public bool IsDeleted { get; set; }

        [Column("cal.CreateDate")]
        [CreateDate]
        public DateTime CreateDate { get; }

        [Column("cal.ModifyDate")]
        [ModifyDate]
        public DateTime? ModifyDate { get; }
    }
}