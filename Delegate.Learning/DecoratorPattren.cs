using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delegate.Learning
{
    public abstract class BabyAction
    {
        public abstract void Operation();
    }


    public class Baby : BabyAction
    {
        string _Name;
        public Baby(string name)
        {
            _Name = name;
        }

        public override void Operation()
        {
            Console.WriteLine($"I am a baby, my name is {_Name}, now I can cry."); ;
        }
    }

    public abstract class BabyGrowing : BabyAction
    {
        protected BabyAction baby;

        public BabyGrowing() { }

        public void SetBabyGrowing(BabyAction baby)
        {
            this.baby = baby;
        }

        public override void Operation()
        {
            if (this.baby != null)
            {
                baby.Operation();
            }
        }
    }

    public class BabyGrowingEating : BabyGrowing
    {
        public override void Operation()
        {
            base.Operation();
            Console.WriteLine("Now I can start eating something.");
        }
    }

    public class BabyGrowingSmiling : BabyGrowing
    {
        public override void Operation()
        {
            base.Operation();
            Console.WriteLine("Now I can start smiling.");
        }
    }
}
