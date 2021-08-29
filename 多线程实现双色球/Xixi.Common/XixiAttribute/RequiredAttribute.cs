using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xixi.Common.XixiAttribute
{
    public class RequiredAttribute : ValidateAbstractAttribute
    {
        public RequiredAttribute()
        {
            
        }
        public override ValidateModel Validate(object value)
        {
            return BaseValidate(value);
        }
    }
}
