using Infrastructure.Dao.Enums;

namespace Infrastructure.Dao.Models
{
    public class FilterModel
    {
        public FilterType Type { get; set; }

        public object Value { get; set; }

        public object DownValue { get; set; }

        public object UpValue { get; set; }
    }
}