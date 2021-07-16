using System;
using System.Collections.Generic;
using System.Text;

namespace Business.DB.SQLServer
{
    public class ReflectionTest
    {
        #region Identity
        /// <summary>
        /// 无参构造函数
        /// </summary>
        public ReflectionTest()
        {

            Console.WriteLine($"这里是{this.GetType()}无参数构造函数");
        }

        /// <summary>
        /// 带参数构造函数
        /// </summary>
        /// <param name="name"></param>
        public ReflectionTest(string name)
        {
            Console.WriteLine($"这里是{this.GetType()} 有参数构造函数");
        }

        public ReflectionTest(int id)
        {
            Console.WriteLine($"这里是{this.GetType()} 有参数构造函数");
        }


        public ReflectionTest(int id, string name)
        {

            //typeof(int);

            //Type //id.GetType();

            Console.WriteLine($"这里是{this.GetType()} 有参数构造函数");
        }

        public ReflectionTest(string name, int id)
        {

            //typeof(int);

            //Type //id.GetType();

            Console.WriteLine($"这里是{this.GetType()} 有参数构造函数");
        }

        #endregion

        #region Method
        /// <summary>
        /// 无参方法
        /// </summary>
        public void Show1()
        {
            Console.WriteLine($"这里是{this.GetType()}的Show1");
        }
        /// <summary>
        /// 有参数方法
        /// </summary>
        /// <param name="id"></param>
        public void Show2(int id)
        {

            Console.WriteLine($"这里是{this.GetType()}的Show2");
        }
        /// <summary>
        /// 重载方法之一
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        public void Show3(int id, string name)
        {
            Console.WriteLine($"这里是{this.GetType()}的Show3");
        }
        /// <summary>
        /// 重载方法之二
        /// </summary>
        /// <param name="name"></param>
        /// <param name="id"></param>
        public void Show3(string name, int id)
        {
            Console.WriteLine($"这里是{this.GetType()}的Show3_2");
        }
        /// <summary>
        /// 重载方法之三
        /// </summary>
        /// <param name="id"></param>
        public void Show3(int id)
        {

            Console.WriteLine($"这里是{this.GetType()}的Show3_3");
        }
        /// <summary>
        /// 重载方法之四
        /// </summary>
        /// <param name="name"></param>
        public void Show3(string name)
        {

            Console.WriteLine("这里是{this.GetType()}的Show3_4");
        }
        /// <summary>
        /// 重载方法之五
        /// </summary>
        public void Show3()
        {

            Console.WriteLine($"这里是{this.GetType()}的Show3_1");
        }
        /// <summary>
        /// 私有方法
        /// </summary>
        /// <param name="name"></param>
        private void Show4(string name)  //肯定是可以的
        {
            Console.WriteLine($"这里是{this.GetType()}的Show4");
        }
        /// <summary>
        /// 静态方法
        /// </summary>
        /// <param name="name"></param>
        public static void Show5(string name)
        {
            Console.WriteLine($"这里是{typeof(ReflectionTest)}的Show5");
        }
        #endregion
    }

   public class GenericClass1<T>
    {
        public GenericClass1(int count, string type)
        {
            
        }

        public int GetCount<T1, T2>(int num1, int num2)
        {
            return num1 + num2;
        }
    }

    public class GenericMethod
    {
        public void Show<T, W, X>(T t, W w, X x)
        {
            Console.WriteLine($"t.type={t.GetType().Name},w.type={ w.GetType().Name},x.type={x.GetType().Name}");
        }
    }


    public class GenericClass<T, W, X>
    {
        public void Show(T t, W w, X x)
        {
            Console.WriteLine($"t.type={t.GetType().Name},w.type={w.GetType().Name},x.type={x.GetType().Name}");
        }
    }

    public class GenericDouble<T>
    {
        public void Show<W, X>(T t, W w, X x)
        {
            Console.WriteLine($"t.type={t.GetType().Name},w.type={w.GetType().Name},x.type={x.GetType().Name}");
        }
    }
}
