namespace Aklion.Crm.Models.Administration.UserAttribute
{
    public class UserAttributeModel
    {
        public int Id { get; set; }

        public int StoreId { get; set; }

        public string StoreName { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public bool IsDeleted { get; set; }

        public string CreateDate { get; set; }

        public string ModifyDate { get; set; }
    }
}