namespace Aklion.Crm.Models.User.Store
{
    public class StoreParameterModel : ParameterModel
    {
        public int? Id { get; set; }

        public string Name { get; set; }

        public string ApiSecret { get; set; }

        public string CreateDate { get; set; }
    }
}