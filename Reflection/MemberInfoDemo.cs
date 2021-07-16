using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Reflection
{
    public class MemberInfoDemo
    {
        public static void ShowMemberInfo()
        {
            Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();
            foreach (Assembly a in assemblies)
            {
                show(0, "Assembly: {0}", a);


                foreach (Type t in a.ExportedTypes)
                {
                    show(1, "Type: {0}", t);

                    foreach (MemberInfo m in t.GetTypeInfo().DeclaredMembers)
                    {
                        string typeName = string.Empty;
                        if (m is Type) typeName = "Type";
                        if (m is FieldInfo) typeName = "FieldInfo";
                        if (m is MethodInfo) typeName = "MethodInfo";
                        if (m is ConstructorInfo) typeName = "ConstructorInfo";
                        if (m is PropertyInfo) typeName = "PropertyInfo";
                        if (m is EventInfo) typeName = "EventInfo";

                        show(2, "{0}:{1}", typeName, m);
                    }
                }
            }
        }

        //params 是C#的关键字， params主要是在声明方法时参数类型或者个数不确定时使用,关于params 参数数组
        //参数数组必须是以为数组
        //不允许将params修饰符与ref和out修饰符组合起来使用
        //
        private static void show(int indent, string format, params object[] args)
        {
            Console.WriteLine(new String(' ', 3 * indent) + format, args);
        }
    }
}
