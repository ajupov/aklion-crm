using System;
using Aklion.Infrastructure.Dao.Attributes;

namespace Aklion.Crm.Domain.ClientAttributeLink
{
    [WhereCombination("and")]
    public class ClientAttributeLinkParameterModel
    {
        [Where("@Id is null or cal.Id = @Id")]
        public int? Id { get; set; }

        [Where("@StoreId is null or cal.StoreId = @StoreId")]
        public int? StoreId { get; set; }

        [Where("@StoreName is null or s.Name like @StoreName + '%'")]
        public string StoreName { get; set; }

        [Where("@ClientId is null or cal.ClientId = @ClientId")]
        public int? ClientId { get; set; }

        [Where("@ClientName is null or c.Name like @ClientName + '%'")]
        public string ClientName { get; set; }

        [Where("@AttributeId is null or cal.AttributeId = @AttributeId")]
        public int? AttributeId { get; set; }

        [Where("@AttributeName is null or ca.Name like @AttributeName + '%'")]
        public string AttributeName { get; set; }

        [Where("@AttributeKey is null or ca.[Key] like @AttributeKey + '%'")]
        public string AttributeKey { get; set; }

        [Where("@Value is null or cal.Value like @Value + '%'")]
        public string Value { get; set; }

        [Where("@IsDeleted is null or cal.IsDeleted = @IsDeleted")]
        public bool? IsDeleted { get; set; }

        [Where("@CreateDate is null or convert(date, cal.CreateDate) = convert(date, @CreateDate)")]
        public DateTime? CreateDate { get; set; }

        [Where("@ModifyDate is null or convert(date, cal.ModifyDate) = convert(date, @ModifyDate)")]
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