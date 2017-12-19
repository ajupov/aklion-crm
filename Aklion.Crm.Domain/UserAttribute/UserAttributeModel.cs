using System;
using Aklion.Infrastructure.Dao.Attributes;

namespace Aklion.Crm.Domain.UserAttribute
{
    [Table("dbo.UserAttribute as ua")]
    [Join("inner join dbo.Store as s on ua.StoreId = s.Id")]
    public class UserAttributeModel
    {
        [Column("ua.Id")]
        [Identificator]
        public int Id { get; }

        [Column("ua.StoreId")]
        public int StoreId { get; set; }

        [Column("s.Name")]
        public string StoreName { get; }

        [Column("ua.Name")]
        public string Name { get; set; }

        [Column("ua.Description")]
        public string Description { get; set; }

        [Column("ua.IsDeleted")]
        public bool IsDeleted { get; set; }

        [Column("ua.CreateDate")]
        [CreateDate]
        public DateTime CreateDate { get; }

        [Column("ua.ModifyDate")]
        [ModifyDate]
        public DateTime? ModifyDate { get; }
    }
}