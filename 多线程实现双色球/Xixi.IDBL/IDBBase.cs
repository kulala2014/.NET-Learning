using System;
using System.Collections.Generic;
using Xixi.Model;

namespace Xixi.IDBL
{
    public interface IDBBase
    { 
        T Find<T>(int id) where T: BaseModel;
        List<T> FindAll<T>() where T: BaseModel;
        bool Add<T>(T t) where T: BaseModel;
        bool Delete<T>(int id) where T : BaseModel;
        bool Update<T>(T t) where T : BaseModel;
    }
}
