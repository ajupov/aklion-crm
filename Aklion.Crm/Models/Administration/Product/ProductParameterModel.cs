using Aklion.Crm.Enums;

namespace Aklion.Crm.Models.Administration.Product
{
    public class ProductParameterModel : ParameterModel
    {
        public int? Id { get; set; }

        public int? StoreId { get; set; }

        public string StoreName { get; set; }

        public ProductType? Type { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public decimal? Price { get; set; }

        public ProductStatus? Status { get; set; }

        public string VendorCode { get; set; }

        public int? ParentId { get; set; }

        public bool? IsDeleted { get; set; }

        public string CreateDate { get; set; }

        public string ModifyDate { get; set; }
    }
}