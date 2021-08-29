using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xixi.Common.XixiAttribute
{
   public class NameAttribute : Attribute
    {
        private string _Name;
        public NameAttribute(string name) => _Name = name;

        public string GetName() => _Name;
    }
}
