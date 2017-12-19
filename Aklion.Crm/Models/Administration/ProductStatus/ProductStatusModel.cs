namespace Aklion.Crm.Models.Administration.ProductStatus
{
    public class ProductStatusModel
    {
        public int Id { get; }

        public int StoreId { get; set; }

        public string StoreName { get; }

        public string Name { get; set; }

        public string CreateDate { get; }

        public string ModifyDate { get; }
    }
}