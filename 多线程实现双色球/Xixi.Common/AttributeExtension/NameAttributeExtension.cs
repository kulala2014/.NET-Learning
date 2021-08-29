using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Xixi.Common.XixiAttribute;

namespace Xixi.Common.AttributeExtension
{
   public static class NameAttributeExtension
    {
        public static string GetDiplayName(this PropertyInfo prop)
        {
            if (prop.IsDefined(typeof(NameAttribute), true))
            {
                var attribute = prop.GetCustomAttribute<NameAttribute>();
                return attribute.GetName();
             }
            return prop.Name;
        }
    }
}
