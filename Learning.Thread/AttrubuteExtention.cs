using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Learning.Thread
{
    [AttributeUsage(AttributeTargets.Field, Inherited =true)]
   public abstract class AttrubuteExtentionAbstractAttrubute : Attribute
    {
        public abstract string Remark();
    }


    public class RemarkAttribute : AttrubuteExtentionAbstractAttrubute
    {
        private string remarkName;
        public RemarkAttribute(string remarkInfo) => this.remarkName = remarkInfo;

        public override string Remark()
        {
            return remarkName;
        }
    }


    public static class RemarkExtension
    {

        public static string Remark(this UserState userState)
        {
            string result = string.Empty;
            Type type = userState.GetType();

            var field = type.GetField(userState.ToString());
            if (field.IsDefined(typeof(AttrubuteExtentionAbstractAttrubute), true))
            {
                var attribute = field.GetCustomAttribute<AttrubuteExtentionAbstractAttrubute>();
                return attribute.Remark();
            }
            return field.Name;

        }
    }
}
