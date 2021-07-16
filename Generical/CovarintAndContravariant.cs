using System;
using System.Collections.Generic;
using System.Text;

namespace Generical
{

    //逆变量：意味着泛型类参数可以从一个类更改为它的某个派生类。用in关键字标记类型参数。
    //逆变量泛型参数只出现在输入位置


    //协变量： 意味着泛型类型参数可以从一个类更改它的某个基类。用out关键字标记类型参数。
    //协变量只出现在返回类型。

    public delegate TResult Func<in T, out TResult>(T arg);


    public class Person
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }

    public class Employee : Person { }

    public class CovarintAndContravariant
    {
        Func<Object, ArgumentException> fn1 = null;

        public CovarintAndContravariant(Func<Object, ArgumentException> fn2)
        {
            this.fn1 = fn2;
        }

        public static void PrintFullName(IEnumerable<Person> persons)
        {
            foreach (Person person in persons)
            {
                Console.WriteLine($"Name: {person.FirstName} {person.LastName}");
            }
        }

        public static void Test()
        {
            IEnumerable<Employee> employees = new List<Employee>()
            {
                new Employee{ FirstName="kulala", LastName="gao"},
            };
            IEnumerable<Person> persons = employees;
            PrintFullName(employees);
        }
    }
}
