namespace Aklion.Crm.Models.User.Category
{
    public class CategoryParameterModel : ParameterModel
    {
        public int? Id { get; set; }

        public string Name { get; set; }

        public int? ParentId { get; set; }

        public string ParentName { get; set; }

        public string CreateDate { get; set; }
    }
}