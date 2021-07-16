using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delegate.Learning
{
    public class AfterActionAttribute : BaseAttribute
    {
        public override Action Do(Action action)
        {
            return new Action(() => {
                action.Invoke();
                Console.WriteLine("After doing kernel logic, doing something here");
            });
        }
    }

    public class AfterActionLastAttribute : BaseAttribute
    {
        public override Action Do(Action action)
        {
            return new Action(() => {
                action.Invoke();
                Console.WriteLine("last doing something here");
            });
        }
    }
}
