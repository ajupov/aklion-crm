using System;

namespace Aklion.Crm.Models.Administration.ProductTag
{
    public class ProductTagModel
    {
        public int Id { get; set; }

        public int StoreId { get; set; }

        public string StoreName { get; set; }

        public int ProductId { get; set; }

        public string ProductName { get; set; }

        public int TagId { get; set; }

        public string TagName { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime? ModifyDate { get; set; }
    }
}