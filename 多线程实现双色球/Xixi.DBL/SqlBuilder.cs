using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Xixi.Common.AttributeExtension;
using Xixi.Model;

namespace Xixi.DBL
{
   public class SqlBuilder<T>: BaseModel
    {
        public static string findSql { get; protected set; }
        public static string findAllSql { get; protected set; }
        public static string InsertSql { get; protected set; }
        public static string DeleteSql { get; protected set; }
        public static string UpdateSql { get; protected set; }
        static SqlBuilder()
        {
            Type type = typeof(T);
            object oInstance = Activator.CreateInstance(type);
            findSql = $"SELECT{string.Join(",", type.GetProperties().Select(p => $"[{p.GetColumnName()}] AS [{p.Name}]"))} FROM [{((T)oInstance).GetTableName()}] AS [{type.Name}] WHERE 1=1";

            findAllSql = $"SELECT{string.Join(",", type.GetProperties().Select(p => $"[{p.GetColumnName()}] AS [{p.Name}]"))} FROM [{((T)oInstance).GetTableName()}] AS [{type.Name}]";

            var items = string.Join(",", type.GetProperties(BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Public).Select(p => $"[{p.GetColumnName()}]"));
            var param = string.Join(",", type.GetProperties(BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Public).Select(p => $"@{p.GetColumnName()}"));
            InsertSql = $"INSERT INTO [{((T)oInstance).GetTableName()}] ({items}) VALUES({param})";

            DeleteSql = $"DELETE FROM [{((T)oInstance).GetTableName()}] WHERE ID=@ID";

            UpdateSql = $"Update [{((T)oInstance).GetTableName()}] SET {string.Join(",", type.GetProperties(BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Public).Select(p => $"[{p.GetColumnName()}]=@{p.GetColumnName()}"))} WHERE ID=@ID";
        }
    }
}
