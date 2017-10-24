using System;

namespace Aklion.Crm.Attributes
{
    [AttributeUsage(AttributeTargets.Class)]
    public class TableAttribute : Attribute
    {
        public TableAttribute(bool isEditable)
        {
            IsEditable = isEditable;
        }

        public bool IsEditable { get; set; }
    }
}