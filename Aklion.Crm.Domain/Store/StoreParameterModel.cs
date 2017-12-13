namespace Aklion.Crm.Domain.Store
{
    public class StoreParameterModel : BaseParameterModel
    {
        public string Name { get; set; }

        public string ApiSecret { get; set; }

        public bool? IsLocked { get; set; }

        public bool? IsDeleted { get; set; }
    }
}