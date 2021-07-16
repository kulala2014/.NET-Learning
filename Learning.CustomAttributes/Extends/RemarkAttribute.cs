using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Learning.CustomAttributes.Extends
{
    [AttributeUsage(AttributeTargets.Field, Inherited =true)]
    public class RemarkAttribute : Attribute
    {
        private string _des;
        public RemarkAttribute(string description) => this._des = description;

        public string GetRemark()
        {
            return this._des;
        }
    }
}
