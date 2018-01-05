using System;
using Aklion.Infrastructure.Dao.Attributes;

namespace Aklion.Crm.Domain.OrderSource
{
    [WhereCombination("and")]
    public class OrderSourceParameterModel
    {
        [Where("@Id is null or oso.Id = @Id")]
        public int? Id { get; set; }

        [Where("@StoreId is null or oso.StoreId = @StoreId")]
        public int? StoreId { get; set; }

        [Where("@StoreName is null or s.Name like @StoreName + '%'")]
        public string StoreName { get; set; }

        [Where("@Name is null or oso.Name like @Name + '%'")]
        public string Name { get; set; }

        [Where("@CreateDate is null or convert(date, oso.CreateDate) = convert(date, @CreateDate)")]
        public DateTime? CreateDate { get; set; }

        [Where("@ModifyDate is null or convert(date, oso.ModifyDate) = convert(date, @ModifyDate)")]
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