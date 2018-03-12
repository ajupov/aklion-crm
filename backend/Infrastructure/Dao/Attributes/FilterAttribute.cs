using System;

namespace Infrastructure.Dao.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class FilterAttribute : Attribute
    {
        public FilterAttribute(string alias)
        {
            Alias = alias;
        }

        public string Alias { get; }
    }
}