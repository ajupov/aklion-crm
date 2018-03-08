using System;
using Aklion.Infrastructure.Dao.Attributes;

namespace Aklion.Crm.Domain.ProductImageKeyLink
{
    [WhereCombination("and")]
    public class ProductImageKeyLinkParameterModel
    {
        [Where("@Id is null or pikl.Id = @Id")]
        public int? Id { get; set; }

        [Where("@StoreId is null or pikl.StoreId = @StoreId")]
        public int? StoreId { get; set; }

        [Where("@StoreName is null or s.Name like @StoreName + '%'")]
        public string StoreName { get; set; }

        [Where("@ProductId is null or pikl.ProductId = @ProductId")]
        public int? ProductId { get; set; }

        [Where("@ProductName is null or p.Name like @ProductName + '%'")]
        public string ProductName { get; set; }

        [Where("@KeyId is null or pikl.KeyId = @KeyId")]
        public int? KeyId { get; set; }

        [Where("@KeyKey is null or pik.[Key] like @KeyKey + '%'")]
        public string KeyKey { get; set; }

        [Where("@KeyName is null or pik.Name like @KeyName + '%'")]
        public string KeyName { get; set; }

        [Where("@IsDeleted is null or pikl.IsDeleted = @IsDeleted")]
        public bool? IsDeleted { get; set; }

        [Where("@CreateDate is null or convert(date, pikl.CreateDate) = convert(date, @CreateDate)")]
        public DateTime? CreateDate { get; set; }

        [Where("@ModifyDate is null or convert(date, pikl.ModifyDate) = convert(date, @ModifyDate)")]
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