namespace Crm.Models.User.Product
{
    public class ProductModel
    {
        public int Id { get; set; }

        public int? ParentProductId { get; set; }

        public string ParentProductName { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }

        public int StatusId { get; set; }

        public string StatusName { get; set; }

        public string VendorCode { get; set; }

        public bool IsDeleted { get; set; }

        public string CreateDate { get; set; }
    }
}