using System;
using Aklion.Infrastructure.Dao.Attributes;

namespace Aklion.Crm.Domain.ProductAttribute
{
    [WhereCombination("and")]
    public class ProductAttributeParameterModel
    {
        [Where("@Id is null or pa.Id = @Id")]
        public int? Id { get; set; }

        [Where("@StoreId is null or pa.StoreId = @StoreId")]
        public int? StoreId { get; set; }

        [Where("@StoreName is null or s.Name like @StoreName + '%'")]
        public string StoreName { get; set; }

        [Where("@Key is null or pa.Key like @Key + '%'")]
        public string Key { get; set; }

        [Where("@Name is null or pa.Name like @Name + '%'")]
        public string Name { get; set; }

        [Where("@IsDeleted is null or pa.IsDeleted = @IsDeleted")]
        public bool? IsDeleted { get; set; }

        [Where("@CreateDate is null or convert(date, pa.CreateDate) = convert(date, @CreateDate)")]
        public DateTime? CreateDate { get; set; }

        [Where("@ModifyDate is null or convert(date, pa.ModifyDate) = convert(date, @ModifyDate)")]
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