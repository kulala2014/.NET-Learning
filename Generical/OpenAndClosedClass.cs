using System;
using System.Collections.Generic;
using System.Text;

namespace Generical
{
    class OpenAndClosedClass
    {
        public static void show()
        {
            Object o = default;
            //开放类型：具有泛型类型参数的类型
            Type t = typeof(Dictionary<,>);
            Console.WriteLine(t);
            o = CreateInstance(t);

            //开放类型：具有泛型类型参数的类型
            t = typeof(DictionaryStringKey<>);
            Console.WriteLine(t);
            o = CreateInstance(t);

            //封闭类型：所有类型参数都传递了屎级的数据类型
            t = typeof(DictionaryStringKey<Guid>);
            Console.WriteLine(t);
            o = CreateInstance(t);
        }

        private static object CreateInstance(Type t)
        {
            Object o = default;
            try
            {
                o = Activator.CreateInstance(t);
            }
            catch (ArgumentException e)
            {
                Console.WriteLine(e.Message);
            }
            return o;
        }
    }

    internal sealed class DictionaryStringKey<TValue> : Dictionary<string, TValue>
    {

    }
}
