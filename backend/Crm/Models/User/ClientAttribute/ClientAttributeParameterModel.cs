namespace Crm.Models.User.ClientAttribute
{
    public class ClientAttributeParameterModel
    {
        public int? Id { get; set; }

        public string Key { get; set; }

        public string Name { get; set; }

        public bool? IsDeleted { get; set; }

        public string CreateDate { get; set; }

        public string ModifyDate { get; set; }

        public string SortingColumn { get; set; }

        public string SortingOrder { get; set; }

        public int? Page { get; set; }

        public int? Size { get; set; }
    }
}