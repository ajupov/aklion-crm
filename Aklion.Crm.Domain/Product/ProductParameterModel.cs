namespace Aklion.Crm.Domain.Product
{
    public class ProductParameterModel : BaseParameterModel
    {
        public int? StoreId { get; set; }

        public string StoreName { get; set; }

        public int? ParentId { get; set; }

        public string ParentName { get; set; }

        public string Name { get; set; }

        public decimal? Price { get; set; }

        public int? StatusId { get; set; }

        public string StatusName { get; set; }

        public string VendorCode { get; set; }

        public bool? IsDeleted { get; set; }
    }
}