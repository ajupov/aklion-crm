using System;
using Aklion.Infrastructure.Dao.Attributes;

namespace Aklion.Crm.Domain.Product
{
    [Table("dbo.Product as p")]
    [Join("inner join dbo.Store as s on p.StoreId = s.Id " +
          "left outer join dbo.Product as pp on p.ParentId = pp.Id " +
          "inner join dbo.ProductStatus as ps on p.StatusId = ps.Id")]
    public class ProductModel
    {
        [Column("p.Id")]
        [Identificator]
        public int Id { get; set; }

        [Column("p.StoreId")]
        public int StoreId { get; set; }

        [Column("s.Name")]
        public string StoreName { get; }

        [Column("p.ParentId")]
        public int? ParentId { get; set; }

        [Column("pp.Name")]
        public string ParentName { get; }

        [Column("p.Name")]
        [AutocompleteOrSelect("p.Name")]
        public string Name { get; set; }

        [Column("p.Price")]
        public decimal Price { get; set; }

        [Column("p.StatusId")]
        public int StatusId { get; set; }

        [Column("ps.Name")]
        public string StatusName { get; }

        [Column("p.VendorCode")]
        public string VendorCode { get; set; }

        [Column("p.IsDeleted")]
        public bool IsDeleted { get; set; }

        [Column("p.CreateDate")]
        public DateTime CreateDate { get; set; }

        [Column("p.ModifyDate")]
        public DateTime? ModifyDate { get; set; }
    }
}