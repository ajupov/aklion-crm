namespace Aklion.Crm.Models.Administration.Store
{
    public class StoreParameterModel : ParameterModel
    {
        public int? Id { get; set; }

        public int? CreateUserId { get; set; }

        public string Name { get; set; }

        public string ApiSecret { get; set; }

        public bool? IsLocked { get; set; }

        public bool? IsDeleted { get; set; }

        public string CreateDate { get; set; }

        public string ModifyDate { get; set; }
    }
}