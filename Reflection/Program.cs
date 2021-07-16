using Business.DB.Interface;
using Business.DB.Model;
using Business.DB.SQLServer;
using System;
using System.Reflection;

namespace Reflection
{
    class Program
    {
        static void Main(string[] args)
        {
            //ORM 
            SQLServerHelper sqlHelper = new SQLServerHelper();
            var result = sqlHelper.NormalQuery<People>(1);
            var result2 = sqlHelper.NormalQuery<User>(1);

            var person = new People { Name="Xixi", Gender="female", Height="124"};

            var result5 = sqlHelper.Query<HouseModel>(1);

            var house = new HouseModel { HouseName = "Xixi", Gender = "female", Id = 123 };
            var result6 = sqlHelper.InsertQuery<HouseModel>(house);

            var result3 = sqlHelper.InsertQuery<People>(person);

            //Relection Emit
            ReflectionEmit.CreateAssembly();

            //加载程序集和反射

            Assembly a1 = Assembly.Load("Generical");
            Assembly a3 = Assembly.Load("Generical, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null");
            Assembly aa = Assembly.LoadFrom("Generical");
            //Assembly a4 = Assembly.LoadFrom(@"D:\Learning\.net\Solution1\Reflection\bin\Debug\netcoreapp3.1\Generical.dll");
            //Assembly a5 = Assembly.LoadFile(@"D:\Learning\.net\Solution1\Reflection\bin\Debug\netcoreapp3.1\Generical.dll");

            Assembly assembly = Assembly.Load("Business.DB.SQLServer");
            Type type = assembly.GetType("Business.DB.SQLServer.SQLServerHelper");
            Type type1 = assembly.GetType("Business.DB.SQLServer.Singlton");
            Type type2 = assembly.GetType("Business.DB.SQLServer.ReflectionTest");

            object? o = Activator.CreateInstance(type);
            object? o1 = Activator.CreateInstance(type, new object[] { 1243});
            object? o2 = Activator.CreateInstance(type, new object[] { "kulala" });
            object? o3 = Activator.CreateInstance(type, new object[] { 1243, "kulala" });

            IDBHelper helper = o as IDBHelper;
            helper.Query();

            var helper1 = o1 as SQLServerHelper;


            helper1.Query();
            //反射加工厂加配置文件动态加载数据库配置
            var dbHelper = DBFactory.CreateInstance();
            dbHelper.Query();

            //反射会破坏单例
            object? o4 = Activator.CreateInstance(type1, true);
            object? o5 = Activator.CreateInstance(type1, true);
            object? o6 = Activator.CreateInstance(type1, true);
            Console.WriteLine(o4.ToString());
            Console.WriteLine(o5.ToString());
            Console.WriteLine(o6.ToString());

            //反射创建实例对象
            //1.非泛型类
            object? test01 = Activator.CreateInstance(type2);
            object? test02 = Activator.CreateInstance(type2, new object[] { "kulala"});
            object? test03 = Activator.CreateInstance(type2, new object[] {123});
            object? test04 = Activator.CreateInstance(type2, new object[] {123, "kulala" });
            object? test05 = Activator.CreateInstance(type2, new object[] { "kulala",123 });

            //2.泛型类
            Type type3 = assembly.GetType("Business.DB.SQLServer.GenericDouble`1");
            Type closedType = type3.MakeGenericType(new Type[] { typeof(string)});
            object? generic01 = Activator.CreateInstance(closedType);
            MethodInfo method2 = closedType.GetMethod("Show");
            MethodInfo closedMethod = method2.MakeGenericMethod(new Type[] { typeof(int), typeof(int) });
            closedMethod.Invoke(generic01, new object[] { "kulala", 123,123});

            //反射调用方法
            //非泛型无参方法
            MethodInfo method = type2.GetMethod("Show1");
            method.Invoke(test01, new object[0]);
            method.Invoke(test01,null);
            method.Invoke(test01, new object[] { });

            MethodInfo method3 = type2.GetMethod("Show2");
            method3.Invoke(test01, new object[] { 123});

            //重载方法
            MethodInfo method4 = type2.GetMethod("Show3", new Type[] { typeof(int), typeof(string)});
            method4.Invoke(test01, new object[] { 123 ,"123"});

            MethodInfo method5 = type2.GetMethod("Show3", new Type[] { typeof(string), typeof(int) });
            method5.Invoke(test01, new object[] { "123", 123 });

            MethodInfo method6 = type2.GetMethod("Show3", new Type[] {});
            method6.Invoke(test01, new object[] { });


            //泛型方法
            Type type4 = assembly.GetType("Business.DB.SQLServer.GenericMethod");
            object? ogm = Activator.CreateInstance(type4);

            MethodInfo gm1 = type4.GetMethod("Show");
            MethodInfo gmClosed = gm1.MakeGenericMethod(new Type[] { typeof(int), typeof(string), typeof(int)});

            gmClosed.Invoke(ogm, new object[] {123,"123",123 });


            Type type5 = assembly.GetType("Business.DB.SQLServer.GenericClass`3");
            Type type5Closed = type5.MakeGenericType(new Type[] { typeof(int), typeof(string), typeof(int) });
            object? gc1 = Activator.CreateInstance(type5Closed);
            MethodInfo method7 = type5Closed.GetMethod("Show");
            method7.Invoke(gc1, new object[] { 123, "123", 123 });

            //foreach (Type t in a1.ExportedTypes)
            //{
            //    Console.WriteLine(t.FullName);
            //}

            //foreach (Type t in a1.GetTypes())
            //{
            //    Console.WriteLine(t.FullName);

            //    foreach (MemberInfo m in t.GetTypeInfo().DeclaredMembers)
            //    {
            //        Console.WriteLine(m.Name);
            //        Console.WriteLine(m.DeclaringType);
            //        Console.WriteLine(m.Module);
            //        Console.WriteLine(m.CustomAttributes);
            //    }
            //}


            //string dataAssembly = "System.Data";
            //Assembly a6 = Assembly.Load(dataAssembly);
            //foreach (Type t in a6.ExportedTypes)
            //{
            //    Console.WriteLine(t.FullName);

            //    foreach (MemberInfo m in t.GetTypeInfo().DeclaredFields)
            //    {
            //        Console.WriteLine(m.Name);
            //        Console.WriteLine(m.DeclaringType);
            //        Console.WriteLine(m.Module);
            //        Console.WriteLine(m.CustomAttributes);
            //    }
            //}

            //MemberInfoDemo.ShowMemberInfo();
            Console.ReadLine();
        }
    }
}
