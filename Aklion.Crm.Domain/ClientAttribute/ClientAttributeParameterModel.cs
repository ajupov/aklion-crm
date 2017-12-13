namespace Aklion.Crm.Domain.ClientAttribute
{
    public class ClientAttributeParameterModel : BaseParameterModel
    {
        public int? StoreId { get; set; }

        public string StoreName { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public bool? IsDeleted { get; set; }
    }
}