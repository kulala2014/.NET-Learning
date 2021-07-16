using Learning.CustomAttributes.Extends;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Learning.CustomAttributes
{
    public enum UserStateEnum
    {
        [Remark("正常状态")]
        Normal =1,
        [Remark("冻结状态")]
        Frozen =2,
        [Remark("删除状态")]
        Deleted = 3
    }
}
