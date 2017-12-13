namespace Aklion.Crm.Domain.ClientAttribute
{
    public class ClientAttributeModel : BaseModel
    {
        public int StoreId { get; set; }

        public string StoreName { get; }

        public string Name { get; set; }

        public string Description { get; set; }

        public bool IsDeleted { get; set; }
    }
}