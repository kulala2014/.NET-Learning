using Learning.CustomAttributes.Extends;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Learning.CustomAttributes
{
    public class UserInfo
    {
        public int Id { get; set; }
        [Required("不能为空")]
        public string Name { get; set; }
        [AgeRange("Age is out of range", _Max =120,_Min =0)]
        [Required("不能为空")]
        public int Age { get; set; }
        public UserStateEnum State { get; set; }

        public string UserStateDescribtion
        {
            get => this.State.GetRemark();
        }
    }
}
