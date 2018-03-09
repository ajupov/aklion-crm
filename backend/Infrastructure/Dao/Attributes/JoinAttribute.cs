using System;

namespace Infrastructure.Dao.Attributes
{
    [AttributeUsage(AttributeTargets.Class)]
    public class JoinAttribute : Attribute
    {
        public JoinAttribute(string value)
        {
            Value = value;
        }

        public string Value { get; }
    }
}