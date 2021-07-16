using Business.DB.Interface;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;
using System;
using System.Reflection;

namespace Reflection
{
    public class DBFactory
    {
        public static IDBHelper CreateInstance()
        {
            string dbInfo = CustomConfigManager.GetConfig("DBConfig");
            string typeName = dbInfo.Split(',')[1];
            string moduleName = dbInfo.Split(',')[0];

            Assembly assembly = Assembly.Load(moduleName);
            Type type = assembly.GetType(typeName);

            object? instance = Activator.CreateInstance(type);
            IDBHelper dbhelper = instance as IDBHelper;
            return dbhelper;

        }
    }

    public static class CustomConfigManager
    {
        public static string GetConfig(string key)
        {
            var builder = new ConfigurationBuilder().AddJsonFile("appsettings.json");
            IConfiguration configuration = builder.Build();
            string configValue = configuration.GetSection(key).Value;
            return configValue;
        }
    }
}
