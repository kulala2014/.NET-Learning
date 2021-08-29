using System;
using System.Reflection;
using Xixi.Common.XixiAttribute;

namespace Xixi.Common.AttributeExtension
{
    public static class TableNameExtension
   {
        public static string GetTableName<T>(this T t)
        {
            Type type = t.GetType();
            if (type.IsDefined(typeof(TableNameAttribute), true))
            {
                TableNameAttribute attribute = type.GetCustomAttribute<TableNameAttribute>();
                return attribute.GetTableName();
            }
            return type.Name;
        }
    }
}
