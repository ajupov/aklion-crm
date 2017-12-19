namespace Aklion.Crm.Models.Administration.ProductAttribute
{
    public class ProductAttributeModel
    {
        public int Id { get; }

        public int StoreId { get; set; }

        public string StoreName { get; }

        public string Name { get; set; }

        public string Description { get; set; }

        public bool IsDeleted { get; set; }

        public string CreateDate { get; }

        public string ModifyDate { get; }
    }
}