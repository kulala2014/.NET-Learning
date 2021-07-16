using Business.DB.Model;
using Learning.CustomAttributes.Extends;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Business.DB.SQLServer
{
     public  class SQLCache<T> where T : BaseModel
    {
        private static string sqlQueryStr = null;
        private static string sqlInsertStr = null;
        private static string sqlDeleteStr = null;
        private static string sqlUpdateStr = null;
        static SQLCache()
        {
            Type type = typeof(T);
            string tableName = type.Name;
            List<string> propList = new List<string>();
            Object? oInstance = Activator.CreateInstance(type);

            if (type.IsDefined(typeof(AliasAbstractAttribute), true))
            {
                AliasAbstractAttribute attribute = type.GetCustomAttribute<AliasAbstractAttribute>();
                tableName =  attribute.AliasName(type.Name);
            }

            foreach (PropertyInfo prop in type.GetProperties())
            {
                if (type.IsDefined(typeof(AliasAbstractAttribute), true))
                {
                    AliasAbstractAttribute attribute = type.GetCustomAttribute<AliasAbstractAttribute>();
                    propList.Add(attribute.AliasName(type.Name));
                }
                else
                {
                    propList.Add(type.Name);
                }
            }


            List<string> proplist = propList
                .Select(x => $"[{x}]")
                .ToList();
            List<string> parmList = propList
                .Select(x => $"@{x}")
                .ToList();
            string typePropStr = string.Join(",", proplist);
            string typeParamStr = string.Join(",", parmList);
            //string propStr = typeof(T).GetProperties()
            sqlQueryStr = $"SELECT {typePropStr} FROM [{tableName}] WHERE Id=@Id";
            sqlInsertStr = $"INSERT INTO {tableName}({typePropStr}) VALUES({typeParamStr})";
        }

        public static string GetQueryStr()
        {
            return sqlQueryStr;
        }
        public static string GetInserttr()
        {
            return sqlInsertStr;
        }
        public static string GetDeleteStr()
        {
            return sqlDeleteStr;
        }
        public static string GetUpdateStr()
        {
            return sqlUpdateStr;
        }
    }
}
