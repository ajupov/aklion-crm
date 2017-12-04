using System;

namespace Aklion.Crm.Models.User.ProductTag
{
    public class ProductTagModel
    {
        public int Id { get; set; }

        public int ProductId { get; set; }

        public string ProductName { get; set; }

        public int TagId { get; set; }

        public string TagName { get; set; }

        public DateTime CreateDate { get; set; }
    }
}