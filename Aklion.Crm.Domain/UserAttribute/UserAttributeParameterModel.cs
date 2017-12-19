using System;
using Aklion.Infrastructure.Dao.Attributes;

namespace Aklion.Crm.Domain.UserAttribute
{
    [WhereCombination("and")]
    public class UserAttributeParameterModel
    {
        [Where("@Id is null or ua.Id = @Id")]
        public int? Id { get; set; }

        [Where("@StoreId is null or ua.StoreId = @StoreId")]
        public int? StoreId { get; set; }

        [Where("@StoreName is null or s.Name like @StoreName + '%'")]
        public string StoreName { get; set; }

        [Where("@Name is null or ua.Name like @Name + '%'")]
        public string Name { get; set; }

        [Where("@Description is null or ua.Description like @Description + '%'")]
        public string Description { get; set; }

        [Where("@IsDeleted is null or ua.IsDeleted = @IsDeleted")]
        public bool? IsDeleted { get; set; }

        [Where("@CreateDate is null or convert(date, ua.CreateDate) = convert(date, @CreateDate)")]
        public DateTime? CreateDate { get; set; }

        [Where("@ModifyDate is null or convert(date, ua.ModifyDate) = convert(date, @ModifyDate)")]
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