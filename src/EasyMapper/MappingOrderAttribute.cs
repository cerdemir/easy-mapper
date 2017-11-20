using System;
using System.Collections.Generic;
using System.Text;

namespace EasyMapper
{
    [AttributeUsage(AttributeTargets.Property, Inherited = false, AllowMultiple = false)]
    public class MappingOrderAttribute : Attribute
    {
        private readonly int _order;
        public MappingOrderAttribute(int order = 0)
        {
            _order = order;
        }

        public int Order { get { return _order; } }
    }
}
