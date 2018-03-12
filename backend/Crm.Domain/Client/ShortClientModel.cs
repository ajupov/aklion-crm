using System;
using Infrastructure.Dao.Attributes;

namespace Crm.Domain.Client
{
    [Table("dbo.Client as c")]
    public class ShortClientModel
    {
        [Column("c.Id")]
        [Identificator]
        public int Id { get; set; }

        [Column("c.Name")]
        public string Name { get; set; }

        [Column("c.IsDeleted")]
        public bool IsDeleted { get; set; }

        [Column("c.CreateDate")]
        [CreateDate]
        public DateTime CreateDate { get; set; }

        [Column("c.ModifyDate")]
        [ModifyDate]
        public DateTime? ModifyDate { get; set; }
    }
}