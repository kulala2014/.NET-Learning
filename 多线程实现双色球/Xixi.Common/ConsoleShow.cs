using System;
using System.Reflection;
using Xixi.Common.AttributeExtension;
using Xixi.Common.XixiAttribute;

namespace Xixi.Common
{
    public static class ConsoleShow
    {
        public static void ShowProperty<T>(this T t)
        {
            Type type = t.GetType();
            
            foreach (var prop in type.GetProperties())
            {
                Console.WriteLine($"{type.Name}--Property:{prop.GetDiplayName()}:    {prop.GetValue(t)}");
            }
        }
    }
}
