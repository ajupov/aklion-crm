namespace Infrastructure.Dao.Models
{
    public class BaseParameterModel
    {
        public string SortingColumn { get; set; }

        public string SortingOrder { get; set; }

        public bool IsDescSortingOrder => SortingOrder == "desc";

        public int Page { get; set; }

        public int Size { get; set; }

        public int TakeCount => Size > 0 ? Size : int.MaxValue;

        public int SkipCount => (Page > 0 ? Page - 1 : 0) * TakeCount;
    }
}