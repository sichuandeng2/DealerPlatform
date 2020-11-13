using System;
using System.Collections.Generic;
using System.Text;

namespace DealerPlatformApiDemo.Core
{
    [AttributeUsage(AttributeTargets.Property)]
    public class ColumnAttribute:Attribute
    {
        private readonly string _name;

        public ColumnAttribute(string name)
        {
            _name = name;
        }

        public string GetColumn => _name;
    }
}
