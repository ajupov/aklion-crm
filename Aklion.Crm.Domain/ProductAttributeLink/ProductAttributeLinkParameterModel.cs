using System;
using Aklion.Infrastructure.Dao.Attributes;

namespace Aklion.Crm.Domain.ProductAttributeLink
{
    [WhereCombination("and")]
    public class ProductAttributeLinkParameterModel
    {
        [Where("@Id is null or pal.Id = @Id")]
        public int? Id { get; set; }

        [Where("@StoreId is null or pal.StoreId = @StoreId")]
        public int? StoreId { get; set; }

        [Where("@StoreName is null or s.Name like @StoreName + '%'")]
        public string StoreName { get; set; }

        [Where("@ProductId is null or pal.ProductId = @ProductId")]
        public int? ProductId { get; set; }

        [Where("@ProductName is null or p.Name like @ProductName + '%'")]
        public string ProductName { get; set; }

        [Where("@AttributeId is null or pal.AttributeId = @AttributeId")]
        public int? AttributeId { get; set; }

        [Where("@AttributeName is null or pa.Name like @AttributeName + '%'")]
        public string AttributeName { get; set; }

        [Where("@AttributeKey is null or pa.Key like @AttributeKey + '%'")]
        public string AttributeKey { get; set; }

        [Where("@Value is null or pal.Value like @Value + '%'")]
        public string Value { get; set; }

        [Where("@IsDeleted is null or pal.IsDeleted = @IsDeleted")]
        public bool? IsDeleted { get; set; }

        [Where("@CreateDate is null or convert(date, pal.CreateDate) = convert(date, @CreateDate)")]
        public DateTime? CreateDate { get; set; }

        [Where("@ModifyDate is null or convert(date, pal.ModifyDate) = convert(date, @ModifyDate)")]
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