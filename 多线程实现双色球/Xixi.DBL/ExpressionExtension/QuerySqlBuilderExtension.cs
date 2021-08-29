using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Xixi.DBL
{
    public static class QuerySqlBuilderExtension
    {
        public static string Query<T>(this Expression<Func<T,bool>> expression)
        {
           var queryStr =  SqlBuilder<T>.findSql;
            ExpressionConditionVisitor visitor = new ExpressionConditionVisitor();
            visitor.Visit(expression);
           var queryItem =  visitor.Condition();

            return queryStr + "AND" + queryItem;
        }
    }
}
