using System;
using System.Collections.Generic;
using System.Text;

namespace Generical
{
    public class GenericCache<T>
    {
        static GenericCache()
        {
            _type = typeof(T) + "cache";
        }
        private static string _type;

        public static string GetCache()
        {
            return _type;
        }
    }

    public class DictionaryCache
    {
        private static Dictionary<Type, string> _TypeTimeDictionary = null;

        static DictionaryCache()
        {
            _TypeTimeDictionary = new Dictionary<Type, string>();
        }

        public static string GetCache<T>()
        {
            Type type = typeof(T);
            if (!_TypeTimeDictionary.ContainsKey(type))
            {
                _TypeTimeDictionary[type] = $"{typeof(T).FullName}";
            }
            return _TypeTimeDictionary[type];
        }
    }
}
