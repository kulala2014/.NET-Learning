using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Xixi.DBL.ExpressionExtension
{
    public static class ExpressionExtension
    {
        public static Expression<Func<T, bool>> And<T>(this Expression<Func<T, bool>> exp1, Expression<Func<T, bool>> exp2)
        {
            if (exp1 == null)
            {
                return exp2;
            }
            if (exp2 == null)
            {
                return exp1;
            }
            ParameterExpression parameter = Expression.Parameter(typeof(T), "c");
            NewExpressionVisitor visitor = new NewExpressionVisitor(parameter);

            var left = visitor.Replace(exp1.Body);
            var right = visitor.Replace(exp2.Body);

            var body =  Expression.And(left, right);
            return Expression.Lambda<Func<T, bool>>(body, new ParameterExpression[] { parameter});
        }

        public static Expression<Func<T, bool>> Or<T>(this Expression<Func<T, bool>> exp1, Expression<Func<T, bool>> exp2)
        {
            if (exp1 == null)
            {
                return exp2;
            }
            if (exp2 == null)
            {
                return exp1;
            }
            ParameterExpression parameter = Expression.Parameter(typeof(T), "c");
            NewExpressionVisitor visitor = new NewExpressionVisitor(parameter);

            var left = visitor.Replace(exp1.Body);
            var right = visitor.Replace(exp2.Body);

            var body = Expression.Or(left, right);
            return Expression.Lambda<Func<T, bool>>(body, new ParameterExpression[] { parameter });
        }

        public static Expression<Func<T, bool>> Not<T>(this Expression<Func<T, bool>> expr)
        {
            if (expr == null) return null;
            var candidateExpr = expr.Parameters[0];
            var body = Expression.Not(expr.Body);

            return Expression.Lambda<Func<T, bool>>(body, candidateExpr);
        }

    }
}
