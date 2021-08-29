using Kulala.Learning.AOP.IContract;
using Kulala.Learning.AOP.Model;
using System;

namespace Kulala.Learning.AOP.Service
{
    public class UserProcess : IUserProcess
    {
        public void Register(User user)
        {
            Console.WriteLine($"User: {user.Name} has registered successfully.");
        }
    }
}
