using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kulala.Learning.IOC.kulala
{
    public interface IKulalaContainer
    {
        void RegisterType<TFrom, TTo>(LifeCycleType lifeCycleType=LifeCycleType.Transient, string Alias=null);
        T Resolve<T>(string Alias = null);
    }
}
