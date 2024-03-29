﻿using System;

namespace Infrastructure.Dao.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class WhereAttribute : Attribute
    {
        public WhereAttribute(string value)
        {
            Value = value;
        }

        public string Value { get; }
    }
}