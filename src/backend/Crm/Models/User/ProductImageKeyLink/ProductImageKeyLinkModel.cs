namespace Crm.Models.User.ProductImageKeyLink
{
    public class ProductImageKeyLinkModel
    {
        public int Id { get; set; }

        public int ProductId { get; set; }

        public string ProductName { get; set; }

        public int KeyId { get; set; }

        public string KeyKey { get; set; }

        public string KeyName { get; set; }

        public string Base64Value { get; set; }

        public bool IsDeleted { get; set; }

        public string CreateDate { get; set; }

        public string ModifyDate { get; set; }
    }
}