using System;
using Aklion.Infrastructure.Dao.Attributes;

namespace Aklion.Crm.Domain.Client
{
    [Table("dbo.Client as c")]
    [Join("inner join dbo.Store as s on c.StoreId = s.Id")]
    public class ClientModel : ICloneable
    {
        [Column("c.Id")]
        [Identificator]
        public int Id { get; set; }

        [Column("c.StoreId")]
        public int StoreId { get; set; }

        [Column("s.Name")]
        public string StoreName { get; }

        [Column("c.Name")]
        [AutocompleteOrSelect("c.Name")]
        public string Name { get; set; }

        [Column("c.IsDeleted")]
        public bool IsDeleted { get; set; }

        [Column("c.CreateDate")]
        [CreateDate]
        public DateTime CreateDate { get; set; }

        [Column("c.ModifyDate")]
        [ModifyDate]
        public DateTime? ModifyDate { get; set; }

        public object Clone()
        {
            return MemberwiseClone();
        }
    }
}