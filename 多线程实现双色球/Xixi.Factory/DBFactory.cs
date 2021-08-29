using System;
using System.Reflection;
using Xixi.Common;
using Xixi.IDBL;

namespace Xixi.Factory
{
    public static class DBFactory
    {
        readonly static string DBLib = StaticConstant.IBaseDALConfig.Split(',')[1];
        readonly static string _dbClassName = StaticConstant.IBaseDALConfig.Split(',')[0];

        public static IDBBase CreateInstance()
        {
            var assembly = Assembly.Load(DBLib);
            var type = assembly.GetType(_dbClassName);

            var Intance = Activator.CreateInstance(type);

            return (IDBBase)Intance;
        }
    }
}
