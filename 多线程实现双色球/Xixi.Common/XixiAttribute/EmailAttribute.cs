using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Xixi.Common.XixiAttribute
{
    public class EmailAttribute : ValidateAbstractAttribute
    {
        private string _mailReg = @"[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?";


        public override ValidateModel Validate(object oValue)
        {
             if (!Regex.IsMatch(oValue.ToString(), _mailReg))
            {
                return new ValidateModel
                {
                    Result = false,
                    ErrorMsg = new List<string> { $"{nameof(EmailAttribute)}: {StaticConstant.MAILINVALID}" }
                };
            }
            return new ValidateModel
            {
                Result = true
            };
        }
    }
}
