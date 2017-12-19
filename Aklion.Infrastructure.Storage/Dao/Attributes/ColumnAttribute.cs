﻿using System;

namespace Aklion.Infrastructure.Dao.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class ColumnAttribute : Attribute
    {
        public ColumnAttribute(string value)
        {
            Value = value;
        }

        public string Value { get; }
    }
}