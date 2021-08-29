using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Learning.Thread
{
   public static class ExtentionMethod
    {
        public static void Show()
        {
            StringBuilder sb = new StringBuilder("Hi, Kulala!");
            if (sb.IndexOf('!') != -1)
            {
                Console.WriteLine("sb contains '!'");
            }
            else
            {
                Console.WriteLine("sb doesn't contain '!'") ;
            }
        }
    }

    public static class StringBuilderExtention
    {
        public static int IndexOf(this StringBuilder sb, char c)
        {
            int result = -1;
            for(int index = 0; index < sb.Length; index++)
            {
                if (sb[index] == c)
                {
                    result =  index;
                    break;
                }
            }
            return result;
        }
    }
}
