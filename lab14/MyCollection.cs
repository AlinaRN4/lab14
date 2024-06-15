using ClassLibrary10lab;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab14
{
    public class MyCollection<T> : MyList<T>, IEnumerable<T> where T : IInit, ICloneable, new()
    {
        public MyCollection() : base() { }
        public MyCollection(int size) : base(size) { }
        public MyCollection(T[] collection) : base(collection) { }

        public IEnumerator<T> GetEnumerator()
        {
            return new MyEnumerator<T>(this);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }

        
    }

    internal class MyEnumerator<T> : IEnumerator<T> where T : IInit, ICloneable, new()
    {
        Point<T>? beg;
        Point<T>? current;

        public MyEnumerator(MyCollection<T> collection)
        {
            beg = collection.beg;
            current = beg;
        }

        public T Current => current.Data;

        object IEnumerator.Current => throw new NotImplementedException();

        public void Dispose()
        {

        }

        public bool MoveNext()
        {
            if (current.Next == null)
            {
                Reset();
                return false;
            }
            else
            {
                current = current.Next;
                return true;
            }
        }

        public void Reset()
        {
            current = beg;
        }
    }
}
