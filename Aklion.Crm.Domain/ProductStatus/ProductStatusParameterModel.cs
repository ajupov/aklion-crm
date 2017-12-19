using System;
using Aklion.Infrastructure.Dao.Attributes;

namespace Aklion.Crm.Domain.ProductStatus
{
    [WhereCombination("and")]
    public class ProductStatusParameterModel
    {
        [Where("@Id is null or ps.Id = @Id")]
        public int? Id { get; set; }

        [Where("@StoreId is null or ps.StoreId = @StoreId")]
        public int? StoreId { get; set; }

        [Where("@StoreName is null or s.Name = @StoreName")]
        public string StoreName { get; set; }

        [Where("@Name is null or ps.Name = @Name")]
        public string Name { get; set; }

        [Where("@CreateDate is null or convert(date, ps.CreateDate) = convert(date, @CreateDate)")]
        public DateTime? CreateDate { get; set; }

        [Where("@ModifyDate is null or convert(date, ps.ModifyDate) = convert(date, @ModifyDate)")]
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