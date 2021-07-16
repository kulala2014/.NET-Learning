using Business.DB.Interface;
using Business.DB.Model;
using Learning.CustomAttributes.Extends;
using System;
using System.Data.SqlClient;
using System.Reflection;

namespace Business.DB.SQLServer
{
    public class SQLServerHelper : IDBHelper
    {
        string connString = "server=.;database=learning;user id=sa;password=Admin123!@#";

        public SQLServerHelper()
        {
            Console.WriteLine($"create instance for {this.GetType().FullName}");
        }

        public SQLServerHelper(int i)
        {
            Console.WriteLine($"create instance for {this.GetType().FullName}");
        }

        public SQLServerHelper(string j)
        {
            Console.WriteLine($"create instance for {this.GetType().FullName}");
        }

        public SQLServerHelper(int i, string j)
        {
            Console.WriteLine($"create instance for {this.GetType().FullName}");
        }
        public void Query()
        {
            Console.WriteLine("SQLServer helper query");
        }

        public People NormalQuery(int Id)
        {

            SqlParameter[] parameters = new[] { new SqlParameter("@Id", Id), };
            //string propStr = typeof(T).GetProperties()
            string sqlStr = @"SELECT
                            [Id]
                            ,[Name]
                            ,[Gender]
                            FROM[learning].[dbo].[User] WHERE Id =@Id";
            using (SqlConnection connection = new SqlConnection(connString))
            {
                using (SqlCommand command = new SqlCommand(sqlStr, connection))
                {
                    try
                    {
                        connection.Open();
                        if (parameters != null)
                        {
                            command.Parameters.AddRange(parameters);
                        }

                        SqlDataReader reader = command.ExecuteReader();

                        if (reader.HasRows)
                        {
                            reader.Read();
                            People data = new People();
                            data.Id = Id;
                            data.Name = reader.GetString(1);
                            data.Gender = reader.GetString(2);
                            Console.WriteLine(data);
                        }
                    }
                    catch (Exception e)
                    {
                        
                    }
                }
            }
                Console.WriteLine("SQLServer helper query");
            return null;
        }

        public T NormalQuery<T>(int Id) where T : BaseModel
        {

            SqlParameter[] parameters = new[] { new SqlParameter("@Id", Id), };
            Type type = typeof(T);
            Object? oInstance = Activator.CreateInstance(type);
            //string propStr = typeof(T).GetProperties()
            string sqlStr = SQLCache<T>.GetQueryStr();
            using (SqlConnection connection = new SqlConnection(connString))
            {
                using (SqlCommand command = new SqlCommand(sqlStr, connection))
                {
                    try
                    {
                        connection.Open();
                        if (parameters != null)
                        {
                            command.Parameters.AddRange(parameters);
                        }

                        SqlDataReader reader = command.ExecuteReader();

                        if (reader.HasRows)
                        {
                            reader.Read();
                            foreach (PropertyInfo prop in type.GetProperties())
                            {
                                prop.SetValue(oInstance, reader[prop.Name]);
                            }
                        }
                    }
                    catch (Exception e)
                    {

                    }
                }
            }
            Console.WriteLine("SQLServer helper query");
            return oInstance as T;
        }

        public bool InsertQuery<T>(T item) where T : BaseModel
        {

            Type type = typeof(T);
            Object? oInstance = Activator.CreateInstance(type);
            var props = type.GetProperties();
            SqlParameter[] parameters = new SqlParameter[props.Length];
            for (int i=0; i< props.Length;i++)
            {
                string propName = props[i].Name;
                if (props[i].IsDefined(typeof(AliasAbstractAttribute), true))
                {
                    AliasAbstractAttribute attribute = props[i].GetCustomAttribute<AliasAbstractAttribute>();
                    propName =  attribute.AliasName(props[i].Name);
                }
                parameters[i] = new SqlParameter($"@{propName}", props[i].GetValue(item));
            }     
            string sqlStr = SQLCache<T>.GetInserttr();
            using (SqlConnection connection = new SqlConnection(connString))
            {
                using (SqlCommand command = new SqlCommand(sqlStr, connection))
                {
                    try
                    {
                        connection.Open();
                        if (parameters != null)
                        {
                            command.Parameters.AddRange(parameters);
                        }
                        int result = command.ExecuteNonQuery();
                    }
                    catch (Exception e)
                    {

                    }
                }
            }
            Console.WriteLine("SQLServer helper query");
            return true;
        }

        public T Query<T>(int Id) where T : BaseModel
        {

            SqlParameter[] parameters = new[] { new SqlParameter("@Id", Id), };
            Type type = typeof(T);
            Object? oInstance = Activator.CreateInstance(type);
            //string propStr = typeof(T).GetProperties()
            string sqlStr = SQLCache<T>.GetQueryStr();
            using (SqlConnection connection = new SqlConnection(connString))
            {
                using (SqlCommand command = new SqlCommand(sqlStr, connection))
                {
                    try
                    {
                        connection.Open();
                        if (parameters != null)
                        {
                            command.Parameters.AddRange(parameters);
                        }

                        SqlDataReader reader = command.ExecuteReader();

                        if (reader.HasRows)
                        {
                            reader.Read();
                            foreach (PropertyInfo prop in type.GetProperties())
                            {
                                if (prop.IsDefined(typeof(AliasAbstractAttribute), true))
                                {
                                    AliasAbstractAttribute attribute = prop.GetCustomAttribute<AliasAbstractAttribute>();
                                    prop.SetValue(oInstance, reader[attribute.AliasName(prop.Name)]);
                                }
                                else
                                {
                                    prop.SetValue(oInstance, reader[prop.Name]);
                                }
                            }
                        }
                    }
                    catch (Exception e)
                    {

                    }
                }
            }
            Console.WriteLine("SQLServer helper query");
            return oInstance as T;
        }
    }
}
