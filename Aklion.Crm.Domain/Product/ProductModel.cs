namespace Aklion.Crm.Domain.Product
{
    public class ProductModel : BaseModel
    {
        public int StoreId { get; set; }

        public string StoreName { get; }

        public int? ParentId { get; set; }

        public string ParentName { get; }

        public string Name { get; set; }

        public decimal Price { get; set; }

        public int StatusId { get; set; }

        public string StatusName { get; }

        public string VendorCode { get; set; }

        public bool IsDeleted { get; set; }
    }
}