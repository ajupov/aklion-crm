using System;
using Infrastructure.Dao.Attributes;

namespace Crm.Domain.OrderItem
{
    [WhereCombination("and")]
    public class OrderItemParameterModel
    {
        [Where("@Id is null or o.Id = @Id")]
        public int? Id { get; set; }

        [Where("@StoreId is null or oi.StoreId = @StoreId")]
        public int? StoreId { get; set; }

        [Where("@StoreName is null or s.Name like @StoreName + '%'")]
        public string StoreName { get; set; }

        [Where("@OrderId is null or oi.OrderId = @OrderId")]
        public int? OrderId { get; set; }

        [Where("@ProductId is null or oi.ProductId = @ProductId")]
        public int? ProductId { get; set; }

        [Where("@ProductName is null or p.Name like @ProductName + '%'")]
        public string ProductName { get; set; }

        [Where("@Price is null or oi.Price = @Price")]
        public decimal? Price { get; set; }

        [Where("@Count is null or oi.Count = @Count")]
        public int? Count { get; set; }

        [Where("@IsDeleted is null or oi.IsDeleted = @IsDeleted")]
        public bool? IsDeleted { get; set; }

        [Where("@CreateDate is null or convert(date, oi.CreateDate) = convert(date, @CreateDate)")]
        public DateTime? CreateDate { get; set; }

        [Where("@ModifyDate is null or convert(date, oi.ModifyDate) = convert(date, @ModifyDate)")]
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