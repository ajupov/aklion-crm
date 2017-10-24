using System;

namespace Aklion.Crm.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class TableColumnAttribute : Attribute
    {
        public TableColumnAttribute(string label, int width)
        {
            Label = label;
            Width = width;
        }

        public string Label { get; set; }

        public int Width { get; set; }
    }
}