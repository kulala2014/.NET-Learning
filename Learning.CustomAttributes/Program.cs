using Learning.CustomAttributes.Extends;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Learning.CustomAttributes
{
    [Some]
    class Program
    {
        [Some] string customField = null;

        public Program()
        {

        }

        [Some]
        static void Main([Some] string[] args)
        {
            //特性获取额外信息：
            UserStateEnum userState = UserStateEnum.Normal;
           var userStateStr =  RemarkAttributeExtention.GetRemark(userState);
            Console.WriteLine(userStateStr);

            //通过扩展方法和特性给类增加额外的功能
            var userStateStr1 = userState.GetRemark();

            Console.WriteLine(userStateStr1);

            //模拟实际使用场景

            var user1 = new UserInfo 
            {
                Id=123,
                Name="kulala",
                Age=20,
                State=UserStateEnum.Deleted
            };
            Console.WriteLine(user1.UserStateDescribtion);

            var user2 = new UserInfo
            {
                Id = 123,
                Name = "kulala",
                Age = 20,
                State = UserStateEnum.Normal
            };
            Console.WriteLine(user2.UserStateDescribtion);

            var user3 = new UserInfo
            {
                Id = 123,
                Age = -20,
                Name="kulala",
                State = UserStateEnum.Frozen
            };
            Console.WriteLine(user3.UserStateDescribtion);

            //特性获取额外功能--新增一个功能
            //WebAPI 例子做validation

            var apiResult = user3.Validate<UserInfo>();





            //特性attribute:
            //能给声明式的为代码添加注解来实现特殊的功能。
            //特性本质上上特性类的实例。
            //编译器在编译的时候


            //FCL内置了各种custom attribute
            {
                //DllImport, Serializable, AssemblyVersion, FLags

                //[Serializable][Flags]
                //public enum Color
                //{
                //    red,
                //    orange,
                //    blue,
                //    green,
                //    white
                //}

                // common enum
                Color colors = Color.red | Color.white;
                Console.WriteLine(colors.ToString());

                Animal animals = Animal.Dog | Animal.Cat;
                Console.WriteLine(animals.ToString());
            }

            //创建自己的attribute
            {
                //1.特性直接或者间接继承字System.Atrribute抽象类。
                //2.特性类名字以attribute结尾
                //3.特性类至少要有一个公共构造函数
                //4.特性类使用时可以省略 attribute 后缀
                //5.使用AtrributeUsageAttribute可以对创建的attribute指定使用的约束
                //AttributeUsageAttribute:可以指定三个参数:
                //AttributeTargets: 枚举类型，指定attribute的使用类型，默认ALL, 多个值可以使用 | 分割
                //AllMultiple属性：指定特性是不是可以对目标元素多次使用，默认为false
                //Inherited:指定特性是不是可以被子类继承，默认为true

                //6.可以在设置特性类的时候，赋值参数给有参构造函数赋值，并且同时可以给公共字段或属性赋值。

                //7.应用特性时，可以用一个前缀明确指定特性要应用于的目标元素。

                //
                //[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false, Inherited =true)]
                //public class SomeAttribute : Attribute
                //{
                //    public SomeAttribute()
                //    {
                //    }
                //}

                //[Custom1]
                //[Some("kulala", Color.red, new Type[] { typeof(int), typeof(string) }, Name = "Sarah", Gender = "Male")]
                //internal class BaseType
                //{
                //    [Custom1] protected virtual void DoSomething() { Console.WriteLine("Base class"); }
                //}

                var typeDT = new DerivedType();
                typeDT.DoSomething();

            }

            //检测定制特性：利用反射
            {
                //1.IsDefined:检测类型是否使用了指定的特性,返回ture | false, 不构造实例
                if (typeof(Student).IsDefined(typeof(SomeAttribute), false))
                {
                    Console.WriteLine($"Student class using attribute:{nameof(SomeAttribute)}");
                }

                if (typeof(DerivedType).IsDefined(typeof(Custom1Attribute), true))
                {
                    Console.WriteLine($"DerivedType or its base class using attribute:{nameof(Custom1Attribute)}");
                }

                if (typeof(DerivedType).IsDefined(typeof(Custom1Attribute), false))
                {
                    Console.WriteLine($"DerivedType class using attribute:{nameof(Custom1Attribute)}");
                }
                else
                {
                    Console.WriteLine($"DerivedType class is not using attribute:{nameof(Custom1Attribute)}");
                }


                var members = from m in typeof(DerivedType).GetTypeInfo().DeclaredMembers.OfType<MethodBase>()
                              where m.IsPublic
                              select m;
                //2.GetCustomAttributes 返回类型使用的特性的集合object[], 返回应用于目标的特性实例对象的集合。1.所有特性集合；2.制定了AllowMultiple为ttrue的特性
                //OfTtpe<TResult>:为linq中的过滤类型的条件，操作的类型为IEnumerable

                foreach (MemberInfo member in members)
                {
                    ShowAttributes(member);
                    //3.GetCustomAttribute 返回应用与目标的指定特性类的实例。object
                    var attribute = member.GetCustomAttribute<SerializableAttribute>();
                    if (attribute is null)
                    {
                        Console.WriteLine($"{nameof(SerializableAttribute)} is inherited = false, so child class can't use it");
                    }
                }
            }

            //因为GetCustomAttribute,GetCustomAttributes都会构造特性类的实例，所以性能不好
            //可以使用Assembly 的ReflectionOnlyLoad来重新load 程序集然后用CustomAttributeData类分析这个程序集的元属性特性。
            Assembly assembly = Assembly.Load("RectionLoadAttribute");
            Type type = assembly.GetType("RectionLoadAttribute.Customer");

            var loadedMembers = type.GetTypeInfo().GetMembers();
            foreach (MemberInfo member in loadedMembers)
            {
                ShowAttributesLoadReflection(member);
            }

            Console.WriteLine("Hello World!");
            Console.WriteLine();
        }

        private static void ShowAttributes(MemberInfo member)
        {
            var attributes = member.GetCustomAttributes<Attribute>();
            Console.WriteLine($"Attributes applied to {member.Name} : {attributes.Count()}");

            foreach (Attribute attribute in attributes)
            {
                Console.WriteLine(attribute.GetType().ToString());
                if (attribute is SomeAttribute)
                {
                    Console.WriteLine($"MemberName={((SomeAttribute)attribute).Name}");
                }

                if (attribute is Custom1Attribute)
                {
                    Console.WriteLine($"MemberName={((Custom1Attribute)attribute).Name}");
                }

            }
        }

        private static void ShowAttributesLoadReflection(MemberInfo member)
        {
            IList<CustomAttributeData> attributes = CustomAttributeData.GetCustomAttributes(member);

            Console.WriteLine($"Attributes applied to {member.Name} : {attributes.Count()}");


            foreach (CustomAttributeData attribute in attributes)
            {
                Type t = attribute.Constructor.DeclaringType;
                Console.WriteLine($"{t.ToString()}");
                Console.WriteLine($"Constructor called={attribute.Constructor}");

                IList<CustomAttributeTypedArgument> posArgs = attribute.ConstructorArguments;
                Console.WriteLine($"Positional arguments passed to constructor: {posArgs.Count}");
                foreach (CustomAttributeTypedArgument argument in posArgs)
                {
                    Console.WriteLine($"Type ={argument.ArgumentType}, Value = {argument.Value}");
                }

                IList<CustomAttributeNamedArgument> namedArgs = attribute.NamedArguments;
                Console.WriteLine($"Named arguments set after construction: {namedArgs.Count}");

                foreach (CustomAttributeNamedArgument na in namedArgs)
                {
                    Console.WriteLine($"Name={na.MemberInfo.Name}, Type={na.TypedValue.ArgumentType}, Value={na.TypedValue.Value}");
                }

            }
        }

        [Some]
        [return: Some]
        public static object GetNumber([Some] string i)
        {
            var test = new object();
            return  test;
        }
    }

    public enum Color
    {
        red,
        orange,
        blue,
        green,
        white
    }

    [Flags]
  public  enum Animal
  {   
    Dog = 0x0001,
    Cat = 0x0002,
    Duck = 0x0004,
    Chicken = 0x0008
  }

    [Serializable][Some]
    public class Student
    {
        [Some] string _name;
        [Some] string _gender;

        [Some]
        public Student()
        {
            
        }
        [Some]
        public string Name
        {
            get => _name;
            set => _name = value;
        }

        [Some]
        public string Gender
        {
            get => _gender;
            set => _gender = value;
        }

    }

    [AttributeUsage(AttributeTargets.All, Inherited =false)]
    public class SomeAttribute : Attribute
    {
        [Some] string _name;
        [Some] string _gender;
        public SomeAttribute()
        {
        }

        public SomeAttribute(string name, object o, Type[] types)
        {

        }

        public string Name
        {
            get => _name;
            set => _name = value;
        }

        public string Gender
        {
            get => _gender;
            set => _gender = value;
        }
    }

    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false, Inherited =true)]
    public class Custom1Attribute : Attribute
    {
        public string Name = "Custom1Attribute";
        public Custom1Attribute()
        {
        }
    }

    [Custom1][Serializable]
    [Some("kulala", Color.red, new Type[] { typeof(int), typeof(string)}, Name = "Sarah", Gender ="Male")]
    internal class BaseType
    {
        [Custom1] public virtual void DoSomething() { Console.WriteLine("Base class"); }
    }

    internal class DerivedType : BaseType
    {

        public override void DoSomething()
        {
            base.DoSomething();
        }
    }






}
