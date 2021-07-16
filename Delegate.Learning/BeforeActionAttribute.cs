using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delegate.Learning
{
    public class BeforeActionAttribute : BaseAttribute
    {
        public override Action Do(Action action)
        {
            return new Action(() => {
                Console.WriteLine("Before doing kernel logic, doing something here");
                action.Invoke();
            });
        }
    }

    public class MiddleActionAttribute : BaseAttribute
    {
        public override Action Do(Action action)
        {
            return new Action(() => {
                Console.WriteLine("After before logic, doing something here");
                action.Invoke();
            });
        }
    }
}
