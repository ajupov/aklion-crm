using System;
using Crm.Enums;
using Infrastructure.Dao.Attributes;

namespace Crm.Domain.UserPermission
{
    [WhereCombination("and")]
    public class UserPermissionParameterModel
    {
        [Where("@Id is null or up.Id = @Id")]
        public int? Id { get; set; }

        [Where("@UserId is null or up.UserId = @UserId")]
        public int? UserId { get; set; }

        [Where("@UserLogin is null or u.Login like @UserLogin + '%'")]
        public string UserLogin { get; set; }

        [Where("@StoreId is null or up.StoreId = @StoreId")]
        public int? StoreId { get; set; }

        [Where("@StoreName is null or s.Name like @StoreName + '%'")]
        public string StoreName { get; set; }

        [Where("coalesce(@Permission, 0) = 0 or up.Permission = @Permission")]
        public Permission? Permission { get; set; }

        [Where("@CreateDate is null or convert(date, up.CreateDate) = convert(date, @CreateDate)")]
        public DateTime? CreateDate { get; set; }

        [Where("@ModifyDate is null or convert(date, up.ModifyDate) = convert(date, @ModifyDate)")]
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