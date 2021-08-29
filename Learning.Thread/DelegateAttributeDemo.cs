using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Learning.Thread
{
    public static class DelegateAttributeDemo
    {
        public static void Show()
        {
            Baby baby = new Baby {Name = "Xixi", Age = 4 };
            baby.ShowBabyLearing();
        }
    }


    public class Baby
    {
        public Baby()
        {
            
        }
        private string _Name;
        private int _Age;
        public string Name 
        {
            get => this._Name;
            set => this._Name = value; 
        }
        public int Age 
        { 
            get => this._Age;
            set => this._Age = value; 
        }

        [BabyActionBeforeEating("Wash your hands")]
        [BabyActionAfterEating("Wash your mouth.")]
        public void LearningEating()
        {
            Console.WriteLine($"Baby: {_Name} is {_Age} years old, starting learning eating. ");
        }
    }

    [AttributeUsage(AttributeTargets.Method, Inherited =true)]
    public abstract class BabyActionAbstractAttribute : Attribute
    {
        public abstract Action Do(Action action);
    }

    public class BabyActionBeforeEatingAttribute : BabyActionAbstractAttribute
    {
        private string babyAction;
        public BabyActionBeforeEatingAttribute(string action) => this.babyAction = action;
        public override Action Do(Action action)
        {
            return new Action(() =>{
                Console.WriteLine($"Before eating, you need to learn {babyAction}.");
                action.Invoke();
            });
        }
    }

    public class BabyActionAfterEatingAttribute : BabyActionAbstractAttribute
    {
        private string babyAction;
        public BabyActionAfterEatingAttribute(string action) => this.babyAction = action;
        public override Action Do(Action action)
        {
            return new Action(() => {
                action.Invoke();
                Console.WriteLine($"After eating, you need to {babyAction}");
            });
        }
    }

    public static class BabyExtention
    {
        public static void ShowBabyLearing(this Baby baby)
        {

            Type type = baby.GetType();
            object instance = Activator.CreateInstance(type);
            PropertyInfo name = type.GetProperty("Name");
            name.SetValue(instance, baby.Name);
            PropertyInfo age = type.GetProperty("Age");
            age.SetValue(instance, baby.Age);
            MethodInfo method = type.GetMethod("LearningEating");
            Action action = new Action(()=>method.Invoke(instance, null));
            if (method.IsDefined(typeof(BabyActionAbstractAttribute), true))
            {
                foreach (var attribute in method.GetCustomAttributes<BabyActionAbstractAttribute>())
                {
                    action = attribute.Do(action);
                }
            }
            action.Invoke();  
        }
    }

}
