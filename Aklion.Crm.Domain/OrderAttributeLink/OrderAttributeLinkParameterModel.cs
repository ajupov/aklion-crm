using System;
using Aklion.Infrastructure.Dao.Attributes;

namespace Aklion.Crm.Domain.OrderAttributeLink
{
    [WhereCombination("and")]
    public class OrderAttributeLinkParameterModel
    {
        [Where("@Id is null or oal.Id = @Id")]
        public int? Id { get; set; }

        [Where("@StoreId is null or oal.StoreId = @StoreId")]
        public int? StoreId { get; set; }

        [Where("@StoreName is null or s.Name like @StoreName + '%'")]
        public string StoreName { get; set; }

        [Where("@OrderId is null or oal.OrderId = @OrderId")]
        public int? OrderId { get; set; }

        [Where("@AttributeId is null or oal.AttributeId = @AttributeId")]
        public int? AttributeId { get; set; }

        [Where("@AttributeKey is null or oa.[Key] like @AttributeKey + '%'")]
        public string AttributeKey { get; set; }

        [Where("@AttributeName is null or oa.Name like @AttributeName + '%'")]
        public string AttributeName { get; set; }

        [Where("@Value is null or oal.Value like @Value + '%'")]
        public string Value { get; set; }

        [Where("@IsDeleted is null or oal.IsDeleted = @IsDeleted")]
        public bool? IsDeleted { get; set; }

        [Where("@CreateDate is null or convert(date, oal.CreateDate) = convert(date, @CreateDate)")]
        public DateTime? CreateDate { get; set; }

        [Where("@ModifyDate is null or convert(date, oal.ModifyDate) = convert(date, @ModifyDate)")]
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