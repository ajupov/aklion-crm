using System;
using Aklion.Crm.Enums;
using Aklion.Infrastructure.Dao.Attributes;

namespace Aklion.Crm.Domain.AuditLog
{
    [WhereCombination("and")]
    public class AuditLogParameterModel
    {
        [Where("@Id is null or al.Id = @Id")]
        public int? Id { get; set; }

        [Where("@UserId is null or al.UserId = @UserId")]
        public int? UserId { get; set; }

        [Where("@UserLogin is null or u.Login like @UserLogin + '%'")]
        public string UserLogin { get; set; }

        [Where("@StoreId is null or al.StoreId = @StoreId")]
        public int? StoreId { get; set; }

        [Where("@StoreName is null or s.Name like @StoreName + '%'")]
        public string StoreName { get; set; }

        [Where("coalesce(@ActionType, 0) = 0 or al.ActionType = @ActionType")]
        public AuditLogActionType? ActionType { get; set; }

        [Where("@OldValue is null or al.OldValue like @OldValue + '%'")]
        public string OldValue { get; set; }

        [Where("@NewValue is null or al.NewValue like @NewValue + '%'")]
        public string NewValue { get; set; }

        [Where("@TimeStamp is null or convert(date, al.TimeStamp) = convert(date, @TimeStamp)")]
        public DateTime? TimeStamp { get; set; }

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