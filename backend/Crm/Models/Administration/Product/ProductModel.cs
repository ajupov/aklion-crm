namespace Crm.Models.Administration.Product
{
    public class ProductModel
    {
        public int Id { get; set; }

        public int StoreId { get; set; }

        public string StoreName { get; set; }

        public int? ParentId { get; set; }

        public string ParentName { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }

        public int StatusId { get; set; }

        public string StatusName { get; set; }

        public string VendorCode { get; set; }

        public bool IsDeleted { get; set; }

        public string CreateDate { get; set; }

        public string ModifyDate { get; set; }
    }
}