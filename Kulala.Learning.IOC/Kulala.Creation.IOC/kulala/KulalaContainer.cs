using Kulala.Learning.IOC.kulala.InjectionFlag;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace Kulala.Learning.IOC.kulala
{
    public class KulalaContainer : IKulalaContainer
    {
        private Dictionary<string, ContainerInfo> _containerDict = new Dictionary<string, ContainerInfo>();
        private Dictionary<string, object> ContainerDictForInstance = new Dictionary<string, object>();
        public void RegisterType<TFrom, TTo>(LifeCycleType lifeCycleType, string Alias)
        {
            if (typeof(TFrom).IsAssignableFrom(typeof(TTo)))
            {
                _containerDict.Add(typeof(TFrom).FullName, new ContainerInfo { type = typeof(TTo), lifeCycleType = lifeCycleType });
            }
            else
            {
                throw new ArgumentException($"{typeof(TFrom).Name} is not assigned from {typeof(TTo).Name}");
            }
        }

        public T Resolve<T>(string Alias)
        {
            if (_containerDict.TryGetValue(typeof(T).FullName, out ContainerInfo containerInfo))
            {
                object result = null;
                Type targetType = containerInfo.type;
                result = GetInstance(targetType, containerInfo.lifeCycleType);
                return (T)result;
            }
            return default(T);
        }

        private object CreateInstance(Type type)
        {
            List<object> paramList = new List<object>();
            ConstructorInfo ctor = GetConstructor(type);

            paramList = CreateParamterInstance(ctor.GetParameters());
            object oInstance = Activator.CreateInstance(type, paramList.ToArray());


            foreach (var property in type.GetProperties().Where(x => x.IsDefined(typeof(InjectionAttribute))))
            {
                (Type targetType, LifeCycleType lifeCycleType) = GetRegisterInfo(property.PropertyType);
                object result = GetInstance(targetType, lifeCycleType);
                property.SetValue(result, oInstance);
            }

            foreach (var method in type.GetMethods().Where(x => x.IsDefined(typeof(InjectionAttribute))))
            {
                List<object> methodParamList = CreateParamterInstance(method.GetParameters());
                method.Invoke(oInstance, methodParamList.ToArray());
            }
            return oInstance;



        }

        private List<object> CreateParamterInstance(ParameterInfo[] paramters)
        {
            List<object> paramList = new List<object>();
            foreach (var param in paramters)
            {
                object result = null;
                (Type targetType, LifeCycleType lifeCycleType) = GetRegisterInfo(param.ParameterType);
                result = GetInstance(targetType, lifeCycleType);
                paramList.Add(result);
            }
            return paramList;
        }

        private (Type, LifeCycleType) GetRegisterInfo(Type type)
        {
            var registerInfo = _containerDict[type.FullName];
            if (registerInfo is null) 
                throw new Exception($"No type is assigned from type:{type.FullName}");
            return (registerInfo.type, registerInfo.lifeCycleType);
        }

        private object GetInstance( Type targetType, LifeCycleType lifeCycleType)
        {
            object result = null;
            switch (lifeCycleType)
            {
                case LifeCycleType.Singleton:
                    if (ContainerDictForInstance.TryGetValue(targetType.FullName, out object value))
                    {
                        result = value;
                    }
                    else
                    {
                        result = CreateInstance(targetType);
                        ContainerDictForInstance.Add(targetType.FullName, result);
                    }
                    break;
                case LifeCycleType.Transient:
                    result = CreateInstance(targetType);
                    break;
                case LifeCycleType.PerThread:
                    object oValue = CallContext.GetData(targetType.FullName);
                    if (oValue is null)
                    {
                        result = CreateInstance(targetType);
                        CallContext.SetData(targetType.FullName, result);
                    }
                    else
                    {
                        result = oValue;
                    }
                    break;
                default:
                    break;
            }

            return result;
        }

        private static ConstructorInfo GetConstructor(Type type)
        {
            var ctor = type.GetConstructors().Where(x => x.IsDefined(typeof(InjectionAttribute))).FirstOrDefault();
            if (ctor is null)
            {
                ctor = type.GetConstructors().OrderByDescending(x => x.GetParameters().Length).FirstOrDefault();
            }

            return ctor;
        }

    }
}
