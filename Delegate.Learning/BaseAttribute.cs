using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delegate.Learning
{
    public abstract class BaseAttribute : Attribute
    {
        public abstract Action Do(Action action);
    }
}
