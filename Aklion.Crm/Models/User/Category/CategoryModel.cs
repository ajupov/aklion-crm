using System;

namespace Aklion.Crm.Models.User.Category
{
    public class CategoryModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int? ParentId { get; set; }

        public string ParentName { get; set; }

        public DateTime CreateDate { get; set; }
    }
}