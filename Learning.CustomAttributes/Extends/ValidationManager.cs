using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Learning.CustomAttributes.Extends
{
   public static class ValidationManager
    {
        public static ApiResult Validate<T>(this T t) where T : class
        {
            Type type = typeof(T);

            foreach (PropertyInfo prop in type.GetProperties())
            {
                if (prop.IsDefined(typeof(AbstractValidationAttribute), true))
                {
                    foreach (AbstractValidationAttribute attribute in prop.GetCustomAttributes<AbstractValidationAttribute>())
                    {
                        var result = attribute.Validate(prop.GetValue(t));
                        if (result.Success)
                        {
                            continue;
                        }
                        else
                        {
                            return result;
                        }

                    }
                }
            }
            return new ApiResult { Success=true};
        }

        public static string AliasTableName<T>(this T t) where T : class
        {
            Type type = typeof(T);

            if (type.IsDefined(typeof(AliasAbstractAttribute), true))
            {
                AliasAbstractAttribute attribute = type.GetCustomAttribute<AliasAbstractAttribute>();
                return  attribute.AliasName(type.Name);
            }
            return type.Name;
        }

        public static string AliasColumnName<T>(this T t) where T : class
        {
            string result = string.Empty;
            Type type = typeof(T);

            foreach (PropertyInfo prop in type.GetProperties())
            {
                if (type.IsDefined(typeof(AliasAbstractAttribute), true))
                {
                    AliasAbstractAttribute attribute = type.GetCustomAttribute<AliasAbstractAttribute>();
                    result = attribute.AliasName(type.Name);
                }
                else
                {
                    result = type.Name;
                }
            }

            return type.Name;
        }

    }
}
