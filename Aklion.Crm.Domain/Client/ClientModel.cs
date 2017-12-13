namespace Aklion.Crm.Domain.Client
{
    public class ClientModel : BaseModel
    {
        public int StoreId { get; set; }

        public string StoreName { get; }

        public string Name { get; set; }

        public bool IsDeleted { get; set; }
    }
}