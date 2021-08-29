using System;
using System.Linq.Expressions;
using Xixi.Common;
using Xixi.Common.AttributeExtension;
using Xixi.DBL;
using Xixi.DBL.ExpressionExtension;
using Xixi.DTO;
using Xixi.Factory;
using Xixi.Model;

namespace Xixi.App
{
    class Program
    {
        static void Main(string[] args)
        {
            DBBase dbHelper = new DBBase();
            var user = dbHelper.Find<User>(4);

            user.ShowProperty();

            var userDto = user.Trans<User, UserDTO>();

            var userDto1 = user.TransExpression<User, UserDTO>();

            userDto.ShowProperty();
            userDto1.ShowProperty();

            var validateResult = userDto1.Validate();
            
            var result = DBFactory.CreateInstance();

            Console.WriteLine("*******Test Expression Query*********");
            Console.WriteLine("Input userAccount");
            string account = "Admin";
            Expression<Func<User, bool>> exp1 = p => p.Account == "Admin";

            Console.WriteLine("Input user [Email]");
            var mail = "12";
            Expression<Func<User, bool>> exp2 = p => p.Email == "12";

            var queryResult = dbHelper.FindQuery<User>(exp1.And(exp2));
            foreach(var item in queryResult)
            {
                Console.WriteLine($"Query Item{item.Name}: info:{item.Account}-{item.CompanyName}-{item.Status}");
            }
            Console.Read();
        }
    }
}
