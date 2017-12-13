namespace Aklion.Crm.Domain.Client
{
    public class ClientParameterModel : BaseParameterModel
    {
        public int? StoreId { get; set; }

        public string StoreName { get; set; }

        public string Name { get; set; }

        public bool? IsDeleted { get; set; }
    }
}