using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xixi.Common.XixiAttribute
{
    public abstract class ValidateAbstractAttribute : Attribute
    {
        public abstract ValidateModel Validate(object value);

        public ValidateModel DoValidate(object oValue)
        {
            var result = BaseValidate(oValue);
            if (result.Result)
            {
                return Validate(oValue);
            }
            else
            {
                return result;
            }
        }

        protected ValidateModel BaseValidate(object oValue)
        {
            if (oValue == null)
            {
                return new ValidateModel
                {
                    Result = false,
                    ErrorMsg = new List<string> { $"{nameof(EmailAttribute)}: {StaticConstant.MAILEMPTY}" }
                };
            }
            else if (string.IsNullOrWhiteSpace(oValue.ToString()))
            {
                return new ValidateModel
                {
                    Result = false,
                    ErrorMsg = new List<string> { $"{nameof(EmailAttribute)}: {StaticConstant.MAILEMPTY}" }
                };
            }
            return new ValidateModel
            {
                Result = true
            };
        }
    }
}
