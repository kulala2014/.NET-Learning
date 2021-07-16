using System;

namespace Delegate.Learning
{
    public delegate object MyCallBack(ArgumentException e);
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            //委托类型的逆变和协变
            Counter(new ArgumentException(), new MyCallBack(MyMethod));


            {
                //委托嵌套 + 特性实现俄罗斯套娃，来实现ASP.net core的管道处理模型，AOP的思想
                //在执行最内层的核心业务之前，层层嵌套，追加业务逻辑，执行顺序从外层到内层。
                var showInfoService = new ShowInfoService<User>();
                showInfoService.ShowInfo();

                //个人认为委托嵌套+特性很像装饰模式
                Baby myBaby = new Baby("Xixi");
                BabyGrowingSmiling smilingAction = new BabyGrowingSmiling();
                BabyGrowingEating eatingAction = new BabyGrowingEating();
                smilingAction.SetBabyGrowing(myBaby);
                eatingAction.SetBabyGrowing(smilingAction);
                eatingAction.Operation();
                Console.ReadLine();

            }
        }

        private static string MyMethod(Exception e)
        {
            return "call back result";
        }

        private static void Counter(ArgumentException e, MyCallBack callBack)
        {
            if (callBack != null)
                Console.WriteLine(callBack(e)); 
        }

    }
}
