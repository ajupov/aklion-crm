using System;
using Aklion.Infrastructure.Dao.Attributes;

namespace Aklion.Crm.Domain.OrderStatus
{
    [WhereCombination("and")]
    public class OrderStatusParameterModel
    {
        [Where("@Id is null or ost.Id = @Id")]
        public int? Id { get; set; }

        [Where("@StoreId is null or ost.StoreId = @StoreId")]
        public int? StoreId { get; set; }

        [Where("@StoreName is null or s.Name = @StoreName")]
        public string StoreName { get; set; }

        [Where("@Name is null or ost.Name = @Name")]
        public string Name { get; set; }

        [Where("@CreateDate is null or convert(date, ost.CreateDate) = convert(date, @CreateDate)")]
        public DateTime? CreateDate { get; set; }

        [Where("@ModifyDate is null or convert(date, ost.ModifyDate) = convert(date, @ModifyDate)")]
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