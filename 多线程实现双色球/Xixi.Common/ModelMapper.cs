using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Xixi.Common
{
    public static class ModelMapper
    {
        public static TOut Trans<T, TOut>(this T t)
        {
            Type type = t.GetType();
            Type typeOut =typeof(TOut);
            object instance = Activator.CreateInstance(typeOut);
            foreach (var prop in typeOut.GetProperties())
            {
                var prop1 = type.GetProperty(prop.Name);
                if (prop1 != null)
                {
                    prop.SetValue(instance, prop1.GetValue(t));
                }
            }

            foreach (var field in typeOut.GetFields())
            {
                var field1 = type.GetField(field.Name);
                if (field1 != null)
                {
                    field.SetValue(instance, field1.GetValue(t));
                }
            }
            return (TOut)instance;
        }

        public static TOut TransExpression<T, TOut>(this T t)
        {
            Type type = t.GetType();
            Type typeOut = typeof(TOut);

            List<MemberBinding> members = new List<MemberBinding>();

            ParameterExpression p = Expression.Parameter(typeof(T), "p");
            
            foreach (var prop in typeOut.GetProperties())
            {
                MemberExpression pExp = Expression.Property(p, type.GetProperty(prop.Name));
                MemberBinding memberBinding = Expression.Bind(prop, pExp);
                members.Add(memberBinding);
            }

            foreach (var field in typeOut.GetFields())
            {
                MemberExpression pExp = Expression.Field(p, type.GetField(field.Name));
                MemberBinding memberBinding = Expression.Bind(field, pExp);
                members.Add(memberBinding);
            }
            MemberInitExpression initExp = Expression.MemberInit(Expression.New(typeOut), members);

            Expression<Func<T, TOut>> lambdaExp = Expression.Lambda<Func<T, TOut>>(initExp, new ParameterExpression[] { p });

            Func<T, TOut> lambda = lambdaExp.Compile();
            return lambda.Invoke(t);
        }
    }
}
