using Aklion.Crm.Enums;
using Aklion.Crm.Models.Base.Enums;

namespace Aklion.Crm.Models.Base
{
    public class BaseFilterModel
    {
        public string Name { get; set; }

        public OperandType Type { get; set; }

        public FilterOperation Operation { get; set; }

        public object Operand1 { get; set; }

        public object Operand2 { get; set; }
    }
}