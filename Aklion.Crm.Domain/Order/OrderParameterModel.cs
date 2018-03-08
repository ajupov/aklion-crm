using System;
using Aklion.Infrastructure.Dao.Attributes;

namespace Aklion.Crm.Domain.Order
{
    [WhereCombination("and")]
    public class OrderParameterModel
    {
        [Where("@Id is null or o.Id = @Id")]
        public int? Id { get; set; }

        [Where("@StoreId is null or o.StoreId = @StoreId")]
        public int? StoreId { get; set; }

        [Where("@StoreName is null or s.Name like @StoreName + '%'")]
        public string StoreName { get; set; }

        [Where("@ClientId is null or o.ClientId = @ClientId")]
        public int? ClientId { get; set; }

        [Where("@ClientName is null or c.Name like @ClientName + '%'")]
        public string ClientName { get; set; }

        [Where("coalesce(@SourceId, 0) = 0 or o.SourceId = @SourceId")]
        public int? SourceId { get; set; }

        [Where("@SourceName is null or oso.Name like @SourceName + '%'")]
        public string SourceName { get; set; }

        [Where("coalesce(@StatusId, 0) = 0 or o.StatusId = @StatusId")]
        public int? StatusId { get; set; }

        [Where("@StatusName is null or ost.Name like @StatusName + '%'")]
        public string StatusName { get; set; }

        [Where("coalesce(@TotalSum, 0) = 0 or i.Sum = @TotalSum")]
        public decimal? TotalSum { get; set; }

        [Where("coalesce(@DiscountSum, 0) = 0 or o.DiscountSum = @DiscountSum")]
        public decimal? DiscountSum { get; set; }

        [Where("@IsDeleted is null or o.IsDeleted = @IsDeleted")]
        public bool? IsDeleted { get; set; }

        [Where("@CreateDate is null or convert(date, o.CreateDate) = convert(date, @CreateDate)")]
        public DateTime? CreateDate { get; set; }

        [Where("@ModifyDate is null or convert(date, o.ModifyDate) = convert(date, @ModifyDate)")]
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