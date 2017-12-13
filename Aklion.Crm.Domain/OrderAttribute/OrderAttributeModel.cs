namespace Aklion.Crm.Domain.OrderAttribute
{
    public class OrderAttributeModel : BaseModel
    {
        public int StoreId { get; set; }

        public string StoreName { get; }

        public string Name { get; set; }

        public string Description { get; set; }

        public bool IsDeleted { get; set; }
    }
}