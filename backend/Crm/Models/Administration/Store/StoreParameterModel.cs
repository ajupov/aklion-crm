namespace Crm.Models.Administration.Store
{
    public class StoreParameterModel
    {
        public int? Id { get; set; }

        public string Name { get; set; }

        public string ApiSecret { get; set; }

        public bool? IsLocked { get; set; }

        public bool? IsDeleted { get; set; }

        public string CreateDate { get; set; }

        public string ModifyDate { get; set; }

        public string SortingColumn { get; set; }

        public string SortingOrder { get; set; }

        public int? Page { get; set; }

        public int? Size { get; set; }
    }
}