using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Learning.CustomAttributes.Extends
{
    public  class RemarkAttributeExtention
    {
        public static string GetRemark(UserStateEnum userState)
        {
            string result = string.Empty;
            Type type = userState.GetType();
            FieldInfo field = type.GetField(userState.ToString());
            if (field.IsDefined(typeof(RemarkAttribute), true))
            {
                RemarkAttribute attribute = field.GetCustomAttribute<RemarkAttribute>();
                Console.WriteLine(attribute.GetRemark());
                result = attribute.GetRemark();
            }
            return result;
        }
    }
    public static class RemarkAttributeExtention1
    {
        public static string GetRemark(this UserStateEnum userState)
        {
            string result = string.Empty;
            Type type = userState.GetType();
            FieldInfo field = type.GetField(userState.ToString());
            if (field.IsDefined(typeof(RemarkAttribute), true))
            {
                RemarkAttribute attribute = field.GetCustomAttribute<RemarkAttribute>();
                Console.WriteLine(attribute.GetRemark());
                result = attribute.GetRemark();
            }
            return result;
        }
    }

}
