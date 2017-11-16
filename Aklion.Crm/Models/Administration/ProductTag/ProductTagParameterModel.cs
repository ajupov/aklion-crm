namespace Aklion.Crm.Models.Administration.ProductTag
{
    public class ProductTagParameterModel : ParameterModel
    {
        public int? Id { get; set; }

        public int? StoreId { get; set; }

        public string StoreName { get; set; }

        public int? ProductId { get; set; }

        public string ProductName { get; set; }

        public int? TagId { get; set; }

        public string TagName { get; set; }

        public bool? IsDeleted { get; set; }

        public string CreateDate { get; set; }

        public string ModifyDate { get; set; }
    }
}