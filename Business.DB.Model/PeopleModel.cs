using System;
using System.Collections.Generic;
using System.Text;

namespace Business.DB.Model
{
   public class People : BaseModel
    {
        public string Name { get; set; }
        public string Gender { get; set; }
        public int Age { get; set; }
        public string Height { get; set; }

        public override string ToString()
        {
            return $"{Name}-{Gender}-{Age}-{Height}";
        }
    }

    public class User : BaseModel
    {
        public string Name { get; set; }
        public string Gender { get; set; }

        public override string ToString()
        {
            return $"{Name}-{Gender}";
        }
    }
}
