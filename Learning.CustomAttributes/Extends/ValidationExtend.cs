using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Learning.CustomAttributes.Extends
{
   public class RequiredAttribute : AbstractValidationAttribute
    {
        public string _ErrorMsg;

        public RequiredAttribute(string message)
        {
            this._ErrorMsg = message;
        }

        public override ApiResult Validate(object? value)
        {
             bool result = !string.IsNullOrWhiteSpace(value?.ToString());
            if (result)
            {
                return new ApiResult { Success= true};
            }
            else
            {
                return new ApiResult { Success = false, ErrorMsg=_ErrorMsg };
            }
        }
    }

    [AttributeUsage(AttributeTargets.Property)]
    public class AgeRangeAttribute : AbstractValidationAttribute
    {
        public string _ErrorMsg;
        public int _Max;
        public int _Min;

        public AgeRangeAttribute(string message)
        {
            this._ErrorMsg = message;
        }

        public override ApiResult Validate(object? value)
        {
            if (!string.IsNullOrWhiteSpace(value?.ToString()))
            {
                int? age = value as int?;

                if (age.HasValue)
                {
                    if (age.Value > _Max || age.Value < _Min)
                    {
                        return new ApiResult { Success = false, ErrorMsg = _ErrorMsg };
                    }
                    else
                    {
                        return new ApiResult { Success = true };
                    }
                }
                else
                {
                    return new ApiResult { Success = false, ErrorMsg = _ErrorMsg };
                }
            }
            else
            {
                return new ApiResult { Success = false, ErrorMsg = _ErrorMsg };
            }
        }
    }

    public class MyOwnValidationAttribute : ValidationAttribute
    {
        
    }
}
