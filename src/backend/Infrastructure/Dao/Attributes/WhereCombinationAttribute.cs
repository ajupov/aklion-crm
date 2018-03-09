using System;

namespace Infrastructure.Dao.Attributes
{
    [AttributeUsage(AttributeTargets.Class)]
    public class WhereCombinationAttribute : Attribute
    {
        public WhereCombinationAttribute(string value = null)
        {
            Value = value;
        }

        public string Value { get; }
    }
}