using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xixi.Common.XixiAttribute
{
    [AttributeUsage(AttributeTargets.Class, Inherited=true)]
    public class TableNameAttribute : Attribute
    {
        private string _name;
        public TableNameAttribute(string name) => _name = name;

        public string GetTableName() => _name;
    }
}
