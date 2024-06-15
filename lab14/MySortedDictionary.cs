using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab14
{
    public class MySortedDictionary<TKey, TValue> : IEnumerable<KeyValuePair<TKey, TValue>> where TKey : notnull
    {
        private SortedDictionary<TKey, TValue> sortedDictionary = new SortedDictionary<TKey, TValue>();

        public int Count => sortedDictionary.Count;

        public void Add(TKey key, TValue value)
        {
            sortedDictionary.Add(key, value);
        }

        
        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
        {
            return sortedDictionary.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public IEnumerable<TKey> Keys => sortedDictionary.Keys;
        public IEnumerable<TValue> Values => sortedDictionary.Values;
    }
}
