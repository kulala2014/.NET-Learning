using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Xixi.Common.AttributeExtension;
using Xixi.IDBL;
using Xixi.Model;

namespace Xixi.DBL
{
    public class DBBase: IDBBase
    {
        private string _dbConStr = ConfigurationManager.ConnectionStrings["DBConnectionString"].ConnectionString;

        public bool Add<T>(T t) where T : BaseModel
        {
            Type type = t.GetType();
            string sql = SqlBuilder<T>.InsertSql;
            SqlParameter[] paramArray = type.GetProperties(BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Public).Select(p => new SqlParameter($"@{p.GetColumnName()}", p.GetValue(t)??DBNull.Value)).ToArray();

            using (SqlConnection conn = new SqlConnection(_dbConStr))
            {
                using (SqlCommand command = new SqlCommand(sql, conn))
                {
                    conn.Open();
                    if (paramArray != null)
                    {
                        command.Parameters.AddRange(paramArray);
                    }
                    return command.ExecuteNonQuery() == 1;
                }
            }
            
        }

        public bool Delete<T>(int id) where T : BaseModel
        {
            Type type = typeof(T);
            object t = Activator.CreateInstance(type);
            string sql = SqlBuilder<T>.DeleteSql;
            SqlParameter[] paramArray = new SqlParameter[] { new SqlParameter("@ID", id)};

            using (SqlConnection conn = new SqlConnection(_dbConStr))
            {
                using (SqlCommand command = new SqlCommand(sql, conn))
                {
                    conn.Open();
                    if (paramArray != null)
                    {
                        command.Parameters.AddRange(paramArray);
                    }
                    return command.ExecuteNonQuery() == 1;
                }
            }
        }

        public T Find<T>(int id) where T: BaseModel
        {
            Type type = typeof(T);
            object oInstance = Activator.CreateInstance(type);
            string sql = SqlBuilder<T>.findSql;
            SqlParameter[] paramArray = new SqlParameter[] { new SqlParameter("@ID", id) };

            using (SqlConnection conn = new SqlConnection(_dbConStr))
            {
                using (SqlCommand command = new SqlCommand(sql, conn))
                {
                    conn.Open();

                    if (paramArray != null)
                    {
                        command.Parameters.AddRange(paramArray);
                    }
                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.HasRows)
                    {
                        reader.Read();
                        foreach (var prop in type.GetProperties())
                        {
                            prop.SetValue(oInstance, reader[prop.Name] is DBNull ? null : reader[prop.Name]);
                        }
                    }
                    else
                    {
                        return null;
                    }
                }
            }
                return (T)oInstance;
        }

        public List<T> FindQuery<T>(Expression<Func<T,bool>> queryExp) where T : BaseModel
        {
            List<T> result = new List<T>();
            Type type = typeof(T);
            object oInstance = Activator.CreateInstance(type);

            using (SqlConnection conn = new SqlConnection(_dbConStr))
            {
                using (SqlCommand command = new SqlCommand(queryExp.Query(), conn))
                {
                    conn.Open();

                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            foreach (var prop in type.GetProperties())
                            {
                                prop.SetValue(oInstance, reader[prop.Name] is DBNull ? null : reader[prop.Name]);
                            }
                            result.Add((T)oInstance);
                            oInstance = Activator.CreateInstance(type);
                        }
                    }
                    else
                    {
                        return null;
                    }
                }
            }
            return result;
        }

        public List<T> FindAll<T>() where T : BaseModel
        {
            List<T> result = new List<T>();
            Type type = typeof(T);
            object oInstance = Activator.CreateInstance(type);
            string sql = SqlBuilder<T>.findAllSql;

            using (SqlConnection conn = new SqlConnection(_dbConStr))
            {
                using (SqlCommand command = new SqlCommand(sql, conn))
                {
                    conn.Open();

                    SqlDataReader reader = command.ExecuteReader();
                     if(reader.HasRows)
                     {
                        while (reader.Read())
                        {
                            foreach (var prop in type.GetProperties())
                            {
                                prop.SetValue(oInstance, reader[prop.Name] is DBNull?null: reader[prop.Name]);
                            }
                            result.Add((T)oInstance);
                            oInstance = Activator.CreateInstance(type);
                        }
                     }
                }
            }
            return result;
        }

        public bool Update<T>(T t) where T : BaseModel
        {
            Type type = typeof(T);
            var updateItem = Find<T>(t.Id);
            if (updateItem != null)
            {
                string sql = SqlBuilder<T>.UpdateSql;
                SqlParameter[] paramArray = type.GetProperties(BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Public).Select(p => new SqlParameter($"@{p.GetColumnName()}", p.GetValue(t) ?? DBNull.Value)).ToArray();

                using (SqlConnection conn = new SqlConnection(_dbConStr))
                {
                    using (SqlCommand command = new SqlCommand(sql, conn))
                    {
                        conn.Open();
                        if (paramArray != null)
                        {
                            command.Parameters.AddRange(paramArray);
                        }
                        return command.ExecuteNonQuery() == 1;
                    }
                }
            }
            else
            {
                return false;
            }
        }
    }
}
