using System;

namespace Aklion.Crm.Models.User.Tag
{
    public class TagModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime CreateDate { get; set; }
    }
}