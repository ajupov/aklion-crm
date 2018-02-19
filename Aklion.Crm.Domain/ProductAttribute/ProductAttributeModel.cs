using System;
using Aklion.Infrastructure.Dao.Attributes;

namespace Aklion.Crm.Domain.ProductAttribute
{
    [Table("dbo.ProductAttribute as pa")]
    [Join("inner join dbo.Store as s on pa.StoreId = s.Id")]
    public class ProductAttributeModel
    {
        [Column("pa.Id")]
        [Identificator]
        public int Id { get; set; }

        [Column("pa.StoreId")]
        public int StoreId { get; set; }

        [Column("s.Name")]
        public string StoreName { get; }

        [Column("pa.Key")]
        public string Key { get; set; }

        [Column("pa.Name")]
        [AutocompleteOrSelect("pa.Name")]
        public string Name { get; set; }

        [Column("pa.IsDeleted")]
        public bool IsDeleted { get; set; }

        [Column("pa.CreateDate")]
        [CreateDate]
        public DateTime CreateDate { get; set; }

        [Column("pa.ModifyDate")]
        [ModifyDate]
        public DateTime? ModifyDate { get; set; }
    }
}