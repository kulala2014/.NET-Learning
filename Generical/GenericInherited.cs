using System;
using System.Collections.Generic;
using System.Text;

namespace Generical
{
    interface IGenericInherited<T>
    {
        T Show();
    }

    public abstract class GenericAbtractClass<T>
    {
        public void Show(T t) { }
    }

    public abstract class ChildClass1 : GenericAbtractClass<int>
    {

    }

    public abstract class ChildClass2<T> : GenericAbtractClass<T>
    {

    }

    public class ChildClass3 : IGenericInherited<string>
    {
        public string Show()
        {
            throw new NotImplementedException();
        }

        public void Show(int text)
        {
            List<Animal> animals = null;
            List<Cat> cats = null;

            //适用范围：泛型接口和泛型委托
            //协变：out
            //只能出现在返回值
            //可以让右边用子类，左边用父类
            IEnumerable<Animal> animails1 = new List<Cat>();
            IAnimalClass<Cat, Animal> cats1 = new AnimalClass<Animal, Cat>();
            IAnimalClass<Cat, Animal> cats2 = new AnimalClass<Cat, Cat>();
            IAnimalClass<Cat, Animal> cats3 = new AnimalClass<Animal, Animal>();
            IAnimalClass<Cat, Animal> cats4 = new AnimalClass<Animal, Cat>();
            IAnimalClass<Cat, Animal> cats5 = new AnimalClass<Animal, Cat>();
            //逆变：in
            //智能出现在输入参数位置
            //可以让右边用父类，左边用子类
        }
    }

    class Animal
    {

    }

    class Cat : Animal
    {

    }

    public interface IAnimalClass<in T, out T2>
    {
        T2 Show(T t);
    }

    class AnimalClass<T,T2> : IAnimalClass<T, T2>
    {
        public T2 Show(T t)
        {
            return default(T2);
        }
    }



}
