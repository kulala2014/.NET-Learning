using System;
using System.Collections.Generic;
using System.Text;

namespace RectionLoadAttribute
{
   [DoSomething][CustomItem][Serializable]
   public class Customer
    {
        [DoSomething] string _name;
        [DoSomething] string _gender;
        public Customer()
        {
        }

        [DoSomething]
        public string Name
        {
            get => _name;
            set => _name = value;
        }

        [DoSomething]
        public string Gender
        {
            get => _gender;
            set => _gender = value;
        }
        [DoSomething][CustomItem]
        public string GetInfo() => $"Name is {_name}, Gender is {_gender}";
    }

    public class Consumer : Customer
    {
        
    }

    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public class CustomItemAttribute : Attribute
    {
        public string Name = "Custom1Attribute";
        public CustomItemAttribute()
        {

        }
    }

    public class DoSomethingAttribute : Attribute
    {
        public DoSomethingAttribute()
        {
            
        }
    }
}
