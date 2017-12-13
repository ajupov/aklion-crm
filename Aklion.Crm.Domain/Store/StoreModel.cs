namespace Aklion.Crm.Domain.Store
{
    public class StoreModel : BaseModel
    {
        public string Name { get; set; }

        public string ApiSecret { get; set; }

        public bool IsLocked { get; set; }

        public bool IsDeleted { get; set; }
    }
}