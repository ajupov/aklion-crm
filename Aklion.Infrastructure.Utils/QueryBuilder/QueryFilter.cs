namespace Aklion.Infrastructure.Utils.QueryBuilder
{
    public class QueryFilter
    {
        public string Name { get; set; }

        public string Type { get; set; }

        public string Operation { get; set; }

        public object Operand1 { get; set; }

        public object Operand2 { get; set; }
    }
}