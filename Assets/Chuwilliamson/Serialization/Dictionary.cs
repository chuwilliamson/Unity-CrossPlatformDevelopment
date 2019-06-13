using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Chuwilliamson.Serialization
{
    [Serializable]
    public class Dictionary : ISerializationCallbackReceiver, IDictionary<string, string>
    {
        public Dictionary()
        {

        }

        public Dictionary(List<string> keys, List<string> values)
        {

        }

        public Dictionary(Dictionary<string, string> dictionary)
        {
            foreach (var kvp in dictionary)
            {
                _items.Add(kvp.Key, kvp.Value);
                
            }
        }
        private Dictionary<string, string> _items = new Dictionary<string, string>();

        [SerializeField] private List<string> keys;

        [SerializeField] private List<string> values;

        

        public void OnBeforeSerialize()
        {
            keys = new List<string>();
            values = new List<string>();

            foreach (var kvp in _items)
            {
                keys.Add(kvp.Key);
                values.Add(kvp.Value);
            }
        }

        public void OnAfterDeserialize()
        {
            _items = new Dictionary<string, string>();
            for (var i = 0; i < keys.Count; i++) _items.Add(keys[i], values[i]);
        }

        public IEnumerator<KeyValuePair<string, string>> GetEnumerator()
        {
            return _items.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable)_items).GetEnumerator();
        }

        public void Add(KeyValuePair<string, string> item)
        {
            _items.Add(item.Key, item.Value);
        }

        public void Clear()
        {
            _items.Clear();
        }

        public bool Contains(KeyValuePair<string, string> item)
        {
            return _items.Contains(item);
        }

        public void CopyTo(KeyValuePair<string, string>[] array, int arrayIndex)
        {
            foreach (var kvp in array)
            {

            }
        }

        public bool Remove(KeyValuePair<string, string> item)
        {
            return _items.Remove(item.Key);
        }

        public int Count => _items.Count;

        public bool IsReadOnly
        {
            get { return false; }
        }
    

        public void Add(string key, string value)
        {
            _items.Add(key, value);
        }

        public bool ContainsKey(string key)
        {
            return _items.ContainsKey(key);
        }

        public bool Remove(string key)
        {
            return _items.Remove(key);
        }

        public bool TryGetValue(string key, out string value)
        {
            return _items.TryGetValue(key, out value);
        }

        public string this[string key]
        {
            get { return _items[key]; }
            set { _items[key] = value; }
        }

        public ICollection<string> Keys => _items.Keys;


        public ICollection<string> Values => _items.Values;
    }
}