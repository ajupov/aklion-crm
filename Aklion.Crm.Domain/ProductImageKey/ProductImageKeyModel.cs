using System;
using Aklion.Infrastructure.Dao.Attributes;

namespace Aklion.Crm.Domain.ProductImageKey
{
    [Table("dbo.ProductImageKey as pik")]
    [Join("inner join dbo.Store as s on pik.StoreId = s.Id")]
    public class ProductImageKeyModel
    {
        [Column("pik.Id")]
        [Identificator]
        public int Id { get; set; }

        [Column("pik.StoreId")]
        public int StoreId { get; set; }

        [Column("s.Name")]
        public string StoreName { get; }

        [Column("pik.[Key]")]
        public string Key { get; set; }

        [Column("pik.Name")]
        [AutocompleteOrSelect("pik.Name")]
        public string Name { get; set; }

        [Column("pik.IsDeleted")]
        public bool IsDeleted { get; set; }

        [Column("pik.CreateDate")]
        [CreateDate]
        public DateTime CreateDate { get; set; }

        [Column("pik.ModifyDate")]
        [ModifyDate]
        public DateTime? ModifyDate { get; set; }
    }
}