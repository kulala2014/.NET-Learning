using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xixi.Common
{
   public static class StaticConstant
    {
        public static readonly string IBaseDALConfig = ConfigurationManager.AppSettings["IBaseDALConfig"];



        //Validation message
        public static readonly string MAILEMPTY = "Email Address is empty";
        public static readonly string MAILINVALID = "Email Address is Invalid";

        public static readonly string LESSTHANMINLENGTH = "Field Lenth is less than min length";
        public static readonly string GREATERTHANMAXLENGTH = "Field Lenth is greater than min length";

        public static readonly string NULLFIELD = "Field IS NULL";
        public static readonly string EMPTY = "Field IS EMPTY";
    }
}
