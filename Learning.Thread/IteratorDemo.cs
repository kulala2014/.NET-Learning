using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Learning.Thread
{
   public static class IteratorDemo
    {

     static  List<Box> boxCollections = new List<Box>
        {
            new Box{ Name="box1", Type="type1"},
            new Box{ Name="box2", Type="type2"},
            new Box{ Name="box3", Type="type3"},
            new Box{ Name="box4", Type="type4"},
            new Box{ Name="box5", Type="type5"},
         };

        public static void Show()
        {
            var boxEnumerator = new BoxEnumerator<Box>(boxCollections);

            while (boxEnumerator.MoveNext())
            {
                var currentItem = boxEnumerator.Current;
                Console.WriteLine($"current Box info: {currentItem.Name}-{currentItem.Type}");
            }
        }
}

    internal class Box
    {
        public string Name { get; set; }
        public string Type { get; set; }
    }


    public sealed class BoxEnumerator<T> : IEnumerator<T>
    {
        private List<T> _collections;
        private int currentIndex;
        private T currentBox;

        public BoxEnumerator(List<T> collections)
        {
            (this._collections, this.currentBox, this.currentIndex) = (collections, default(T), -1);
        }

        public T Current => this.currentBox;

        object IEnumerator.Current => this.Current;

        public void Dispose()
        {
            
        }

        public bool MoveNext()
        {
            if ((++currentIndex) < _collections.Count)
            {
                currentBox = _collections[currentIndex];
            }
            else
            {
                return false;
            }
            return true;
        }

        public void Reset()
        {
            currentIndex = -1 ;
        }
    }
}
