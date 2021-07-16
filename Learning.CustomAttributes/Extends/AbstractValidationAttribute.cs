using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Learning.CustomAttributes.Extends
{
    [AttributeUsage(AttributeTargets.Property)]
    public abstract class AbstractValidationAttribute : Attribute
    {
        public abstract ApiResult Validate(object? value);
    }
}
