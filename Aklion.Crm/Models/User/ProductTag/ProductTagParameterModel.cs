namespace Aklion.Crm.Models.User.ProductTag
{
    public class ProductTagParameterModel : ParameterModel
    {
        public int? Id { get; set; }

        public int? ProductId { get; set; }

        public string ProductName { get; set; }

        public int? TagId { get; set; }

        public string TagName { get; set; }

        public string CreateDate { get; set; }
    }
}