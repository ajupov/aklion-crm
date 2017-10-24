using Aklion.Crm.Models.Base.Enums;

namespace Aklion.Crm.Models.Base
{
    public class BaseSortingModel
    {
        public string Name { get; set; }

        public SortingOrder Order { get; set; }

        public byte Priority { get; set; }
    }
}