using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xixi.Common.XixiAttribute
{
    public class StrLengthAttribute : ValidateAbstractAttribute
    {
        private int _minL;
        private int _maxL;

        public StrLengthAttribute(int min, int max) => (_minL, _maxL) = (min,max);

        public override ValidateModel Validate(object oValue)
        {
            if (oValue.ToString().Length < _minL)
            {
                return new ValidateModel
                {
                    Result = false,
                    ErrorMsg = new List<string> { $"{nameof(StrLengthAttribute)}: {StaticConstant.LESSTHANMINLENGTH}" }
                };
            }
            else if (oValue.ToString().Length > _maxL)
            {
                return new ValidateModel
                {
                    Result = false,
                    ErrorMsg = new List<string> { $"{nameof(StrLengthAttribute)}: {StaticConstant.GREATERTHANMAXLENGTH}" }
                };
            }
            return new ValidateModel
            {
                Result = true
            };
        }
    }
}
