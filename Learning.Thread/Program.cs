using LearningThread;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Learning.Thread
{
    class Program
    {

        internal Action<int> updateCapturedLocalVariable;
        //internal Func<int, bool> isEqualToCapturedLocalVariable;
        static void Main(string[] args)
        {

            dynamic str = "aaa";
            Console.WriteLine(str);

            //LINQdemo
            Linq_Learning.Show();
            //new Program().updateCapturedLocalVariable += x => Console.WriteLine(3);
            //int[] scores = new int[] { 97, 92, 81, 60 };

            //// Define the query expression.
            //IEnumerable<int> scoreQuery =
            //    from score in scores
            //    where score > 80
            //    select score;


            //Extendtion 
            ExtentionMethod.Show();

            //yield test
            YieldTest.Show();


            //迭代器 demo
            IteratorDemo.Show();

            //特性获取额外信息
            AttributeDemo.Show();

            //action delegate
            DelegateAttributeDemo.Show();

            //Event demo
            MailEvent.Show();

            //Write a event
            WriteEvent.Show();

            //CustomWhere
            CustomWhere.Show();
            //new Program().Run(3);
            Console.Read();
        }

        //public void Run(int input)
        //{
        //    int j = 0;

        //    updateCapturedLocalVariable = x =>
        //    {
        //        j = x;
        //        bool result = j > input;
        //        Console.WriteLine($"{j} is greater than {input}: {result}");
        //    };

        //    isEqualToCapturedLocalVariable = x => x == j;

        //    Console.WriteLine($"Local variable before lambda invocation: {j}");
        //    updateCapturedLocalVariable(10);
        //    Console.WriteLine($"Local variable after lambda invocation: {j}");
        //}
    }
}
