using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Learning.Thread
{
    public static class AttributeDemo
    {
        public static void Show()
        {
            var user1 = new User { Name = "kulala", Age = 20, State = UserState.Admin};
            Console.WriteLine($"user1 's state is {user1.StateDescription()}");
        }
        
    }


    public enum UserState
    {
        [Remark("已经删除")]
        Delete = 1,
        [Remark("已冻结")]
        Forzen = 2,
        [Remark("正常")]
        Normal = 3,
        Admin
    }

    public class User
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public UserState State { get; set; }
        public string StateDescription() => State.Remark();
    }



}
