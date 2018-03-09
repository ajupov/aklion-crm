using System;

namespace Infrastructure.Dao.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class SortingColumnAttribute : Attribute
    {
    }
}