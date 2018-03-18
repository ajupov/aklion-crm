using Infrastructure.Dao.Enums;

namespace Infrastructure.Dao.Models
{
    public class FilterModel<T>
    {
        public FilterType Type { get; set; }

        public T Value { get; set; }

        public T DownValue { get; set; }

        public T UpValue { get; set; }
    }
}