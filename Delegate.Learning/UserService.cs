using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Delegate.Learning
{
   public class User
   {
        private string _FirstName;
        private string _LastName;

        public User(string firstName, string LastName) => (_FirstName, _LastName) = (firstName, LastName);

        [BeforeAction]
        [MiddleAction]
        [AfterAction]
        [AfterActionLast]
        public void ShowUserInfor()
        {
            Console.WriteLine($"User fullName is {_FirstName}-{_LastName}");
        }
   }

    public class ShowInfoService<T> where T : User
    {

        public void ShowInfo()
        {
            Type type = typeof(T);
            object oIstance = Activator.CreateInstance(type, new object[] { "Clyde", "Gao"});
            MethodInfo method = type.GetMethod("ShowUserInfor");
            Action action = () => method.Invoke(oIstance, null);
            if (method.IsDefined(typeof(BaseAttribute), true))
            {
                foreach (BaseAttribute attribute in method.GetCustomAttributes<BaseAttribute>())
                {
                    action = attribute.Do(action);
                }
            }
            action.Invoke();
        }
        
    }
}
