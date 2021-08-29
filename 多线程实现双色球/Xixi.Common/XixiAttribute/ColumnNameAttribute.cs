using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xixi.Common.XixiAttribute
{
    [AttributeUsage(AttributeTargets.Property, Inherited = true)]
   public class ColumnNameAttribute : Attribute
   {
        private string _name;
        public ColumnNameAttribute(string name) => _name = name;

        public string GetColumnName() => _name;
    }
}
