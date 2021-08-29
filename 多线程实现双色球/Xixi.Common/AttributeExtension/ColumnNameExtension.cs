using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Xixi.Common.XixiAttribute;

namespace Xixi.Common.AttributeExtension
{
   public static class ColumnNameExtension
   {
        public static string GetColumnName(this PropertyInfo prop)
        {
            if (prop.IsDefined(typeof(ColumnNameAttribute), true))
            {
                var attribute = prop.GetCustomAttribute<ColumnNameAttribute>();
                return attribute.GetColumnName();
            }
            return prop.Name;
        }
   }
}
