using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection.Emit;
using System.Reflection;
using System.Globalization;

namespace Reflection
{
   public class ReflectionEmit
    {
        public static void CreateAssembly()
        {
            AssemblyName assemblyName = new AssemblyName("MyTest");
            assemblyName.Version = new Version("1.0.0");
            assemblyName.CultureName = CultureInfo.CurrentCulture.Name;
            assemblyName.SetPublicKeyToken(new Guid().ToByteArray());
            //Run and Collect：内存使用，如果停止使用自动回收。
            AssemblyBuilder assemblyBuilder = AssemblyBuilder.DefineDynamicAssembly(assemblyName, AssemblyBuilderAccess.RunAndCollect);

            ModuleBuilder moduleBuilder = assemblyBuilder.DefineDynamicModule("MyTest");

            TypeBuilder typeBuilder = moduleBuilder.DefineType("MyTest.MyClass", TypeAttributes.Public|TypeAttributes.Sealed);


            FieldBuilder fieldBuilder = typeBuilder.DefineField("NumberField", typeof(int), FieldAttributes.Public);


            Type[] paramsTypes = { typeof(int) };

            ConstructorBuilder ctor1 = typeBuilder.DefineConstructor(MethodAttributes.Public, CallingConventions.Standard, paramsTypes);

            ILGenerator iLGe = ctor1.GetILGenerator();

            iLGe.Emit(OpCodes.Ldarg_0);//把第一个参数追加到堆栈内存
            iLGe.Emit(OpCodes.Call, typeof(object).GetConstructor(Type.EmptyTypes));

            iLGe.Emit(OpCodes.Ldarg_0);
            iLGe.Emit(OpCodes.Ldarg_1);
            iLGe.Emit(OpCodes.Stfld, fieldBuilder);
            iLGe.Emit(OpCodes.Ret);



            MethodBuilder methodBuilder = typeBuilder.DefineMethod("ConsoleMethod", MethodAttributes.Public,CallingConventions.Standard,null, null);

            iLGe = methodBuilder.GetILGenerator();

            iLGe.Emit(OpCodes.Ldstr, "Kulala is learning reflection Emit");
            iLGe.Emit(OpCodes.Call, typeof(Console).GetMethod("WriteLine", new Type[] { typeof(string)}));
            iLGe.Emit(OpCodes.Ret);




            MethodBuilder methodBuilder1 = typeBuilder.DefineMethod("AddMethod", MethodAttributes.Public, CallingConventions.Standard, typeof(int), new Type[2] { typeof(int), typeof(int) });

            iLGe = methodBuilder1.GetILGenerator();
            iLGe.Emit(OpCodes.Ldarg_0);
            iLGe.Emit(OpCodes.Ldarg_1);
            iLGe.Emit(OpCodes.Add);
            iLGe.Emit(OpCodes.Ret);

            var type= typeBuilder.CreateType();
            Console.WriteLine($"DynamicClass is {type.Assembly.FullName} + {type.Namespace}:  + {type.FullName}");

            var oInstance = Activator.CreateInstance(type, new object[] { 123});
            FieldInfo field = type.GetField("NumberField");
            Console.WriteLine($"FieldName:{field.Name} Value: {field.GetValue(oInstance)}");

            MethodInfo method1 = type.GetMethod("ConsoleMethod");
            method1.Invoke(oInstance, null);


            MethodInfo method2 = type.GetMethod("AddMethod");
           var result = method2.Invoke(oInstance, new object[2] { 123,123});
            Console.WriteLine(result);


        }

    }
}
