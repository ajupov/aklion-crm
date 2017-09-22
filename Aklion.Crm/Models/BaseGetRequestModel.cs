namespace Aklion.Crm.Models
{
    public class BaseGetRequestModel
    {
        public int Page { get; set; }

        public int Size { get; set; }

        public string SortingColumn { get; set; }

        public string SortingOrder { get; set; }
    }
}