using System;
using Infrastructure.Dao.Attributes;

namespace Crm.Domain.UserAttributeLink
{
    [WhereCombination("and")]
    public class UserAttributeLinkParameterModel
    {
        [Where("@Id is null or ual.Id = @Id")]
        public int? Id { get; set; }

        [Where("@StoreId is null or ual.StoreId = @StoreId")]
        public int? StoreId { get; set; }

        [Where("@StoreName is null or s.Name like @StoreName + '%'")]
        public string StoreName { get; set; }

        [Where("@UserId is null or ual.UserId = @UserId")]
        public int? UserId { get; set; }

        [Where("@UserLogin is null or u.Login like @UserLogin + '%'")]
        public string UserLogin { get; set; }

        [Where("@AttributeId is null or ual.AttributeId = @AttributeId")]
        public int? AttributeId { get; set; }

        [Where("@AttributeKey is null or ua.[Key] like @AttributeKey + '%'")]
        public string AttributeKey { get; set; }

        [Where("@AttributeName is null or ua.Name like @AttributeName + '%'")]
        public string AttributeName { get; set; }

        [Where("@Value is null or ual.Value like @Value + '%'")]
        public string Value { get; set; }

        [Where("@IsDeleted is null or ual.IsDeleted like @IsDeleted")]
        public bool? IsDeleted { get; set; }

        [Where("@CreateDate is null or convert(date, ual.CreateDate) = convert(date, @CreateDate)")]
        public DateTime? CreateDate { get; set; }

        [Where("@ModifyDate is null or convert(date, ual.ModifyDate) = convert(date, @ModifyDate)")]
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