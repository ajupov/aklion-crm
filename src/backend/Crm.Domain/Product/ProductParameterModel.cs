using System;
using Infrastructure.Dao.Attributes;

namespace Crm.Domain.Product
{
    [WhereCombination("and")]
    public class ProductParameterModel
    {
        [Where("@Id is null or p.Id = @Id")]
        public int? Id { get; set; }

        [Where("@StoreId is null or p.StoreId = @StoreId")]
        public int? StoreId { get; set; }

        [Where("@StoreName is null or s.Name like @StoreName + '%'")]
        public string StoreName { get; set; }

        [Where("@ParentId is null or p.ParentId = @ParentId")]
        public int? ParentId { get; set; }

        [Where("@ParentName is null or pp.Name like @ParentName + '%'")]
        public string ParentName { get; set; }

        [Where("@Name is null or p.Name like @Name + '%'")]
        public string Name { get; set; }

        [Where("coalesce(@Price, 0) = 0 or p.Price = @Price")]
        public decimal? Price { get; set; }

        [Where("coalesce(@StatusId, 0) = 0 or p.StatusId = @StatusId")]
        public int? StatusId { get; set; }

        [Where("@StatusName is null or ps.Name like @StatusName + '%'")]
        public string StatusName { get; set; }

        [Where("@VendorCode is null or p.VendorCode like @VendorCode + '%'")]
        public string VendorCode { get; set; }

        [Where("@IsDeleted is null or p.IsDeleted = @IsDeleted")]
        public bool? IsDeleted { get; set; }

        [Where("@CreateDate is null or convert(date, p.CreateDate) = convert(date, @CreateDate)")]
        public DateTime? CreateDate { get; set; }

        [Where("@ModifyDate is null or convert(date, p.ModifyDate) = convert(date, @ModifyDate)")]
        public DateTime? ModifyDate { get; set; }

        [SortingColumn]
        public string SortingColumn { get; set; }

        [SortingOrder]
        public string SortingOrder { get; set; }

        [Page]
        public int? Page { get; set; }

        [Size]
        public int? Size { get; set; }
    }
}