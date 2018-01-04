using System;

namespace Aklion.Infrastructure.Dao.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class AutocompleteOrSelectAttribute : Attribute
    {
        public AutocompleteOrSelectAttribute(string value)
        {
            Value = value;
        }

        public string Value { get; }
    }
}