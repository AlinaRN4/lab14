using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClassLibrary10lab;

namespace lab14
{
    public class MyList<T> : IEnumerable<T> where T : IInit, ICloneable, new()
    {
        public Point<T>? beg = null;
        public Point<T>? end = null;

        int count = 0;

        public int Count => count;

        public Point<T> MakeRandomData()
        {
            T data = new T();
            data.RandomInit();
            return new Point<T>(data);
        }

        public T MakeRandomItem()
        {
            T data = new T();
            data.RandomInit();
            return data;
        }

        public void AddToBegin(T item)
        {
            T newData = (T)item.Clone(); // глубокое копирование
            Point<T> newItem = new Point<T>(newData);
            count++;
            if (beg != null)
            {
                beg.Prev = newItem;
                newItem.Next = beg;
                beg = newItem;
            }
            else
            {
                beg = newItem;
                end = beg;
            }
        }

        public void AddToEnd(T item)
        {
            T newData = (T)item.Clone(); // глубокое копирование
            Point<T> newItem = new Point<T>(newData);
            count++;
            if (end != null)
            {
                end.Next = newItem;
                newItem.Prev = end;
                end = newItem;
            }
            else
            {
                beg = newItem;
                end = beg;
            }
        }

        public MyList() { }

        public MyList(int size)
        {
            if (size <= 0) throw new Exception("size меньше нуля");
            beg = MakeRandomData();
            end = beg;
            for (int i = 1; i < size; i++)
            {
                T newItem = MakeRandomItem();
                AddToEnd(newItem);
            }
            count = size;
        }

        public MyList(T[] collection)
        {
            if (collection == null)
                throw new Exception("empty collection: null");
            if (collection.Length == 0)
                throw new Exception("empty collection");
            T newData = (T)collection[0].Clone();
            beg = new Point<T>(newData);
            end = beg;
            for (int i = 1; i < collection.Length; i++)
            {
                AddToEnd(collection[i]);
            }
        }

        public void PrintList()
        {
            if (count == 0)
                Console.WriteLine("the list is empty");
            Point<T>? current = beg;
            for (int i = 0; current != null; i++)
            {
                Console.WriteLine(current);
                current = current.Next;
            }
        }

        public Point<T>? FindItem(T item)
        {
            Point<T>? current = beg;
            while (current != null)
            {
                if (current.Data == null)
                    throw new Exception("Data is null");
                if (current.Data.Equals(item))
                    return current;
                current = current.Next;
            }
            return null;
        }

        

        public IEnumerator<T> GetEnumerator()
        {
            Point<T>? current = beg;
            while (current != null)
            {
                yield return current.Data;
                current = current.Next;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
