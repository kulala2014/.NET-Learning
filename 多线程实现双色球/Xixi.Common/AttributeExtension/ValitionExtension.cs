using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Xixi.Common.XixiAttribute;

namespace Xixi.Common.AttributeExtension
{
    public static class ValitionExtension
    {
        public static ValidateModel Validate<T>(this T t)
        {
            var result = new ValidateModel{ Result=true, ErrorMsg= new List<string>()};
            Type type = t.GetType();
            foreach (var prop in type.GetProperties())
            {
                if (prop.IsDefined( typeof(ValidateAbstractAttribute), true))
                {
                    foreach (ValidateAbstractAttribute attribute in prop.GetCustomAttributes(typeof(ValidateAbstractAttribute)))
                    {
                        var validateResult = attribute.DoValidate(prop.GetValue(t));
                        if (!validateResult.Result)
                        {
                            result.Result = false;
                            result.ErrorMsg.Add(validateResult.ErrorMsg[0]);
                        }
                    }
                }
            }
            return result;
        }
    }
}
