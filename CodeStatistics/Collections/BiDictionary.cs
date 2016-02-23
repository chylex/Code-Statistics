using System.Collections;
using System.Collections.Generic;

namespace CodeStatistics.Collections{
    public class BiDictionary<TKey,TValue> : IEnumerable<KeyValuePair<TKey,TValue>>{
        private readonly Dictionary<TKey,TValue> kv;
        private readonly Dictionary<TValue,TKey> vk;

        public ICollection<TKey> Keys { get { return kv.Keys; } }
        public ICollection<TValue> Values { get { return vk.Keys; } }
        public int Count { get { return kv.Count; } }

        public IEnumerable<KeyValuePair<TValue,TKey>> Reverse { get { return vk; } }

        public BiDictionary(){
            kv = new Dictionary<TKey,TValue>();
            vk = new Dictionary<TValue,TKey>();
        }

        public BiDictionary(int capacity){
            kv = new Dictionary<TKey,TValue>(capacity);
            vk = new Dictionary<TValue,TKey>(capacity);
        }

        public void Add(TKey key, TValue value){
            kv.Add(key,value);
            vk.Add(value,key);
        }

        public bool ContainsKey(TKey key){
            return kv.ContainsKey(key);
        }

        public bool ContainsValue(TValue value){
            return vk.ContainsKey(value);
        }

        public void Clear(){
            kv.Clear();
            vk.Clear();
        }

        public bool RemoveKey(TKey key){
            TValue value;
            
            if (kv.TryGetValue(key,out value) && kv.Remove(key)){
                vk.Remove(value);
                return true;
            }
            else return false;
        }

        public bool RemoveValue(TValue value){
            TKey key;
            
            if (vk.TryGetValue(value,out key) && vk.Remove(value)){
                kv.Remove(key);
                return true;
            }
            else return false;
        }

        public IEnumerator<KeyValuePair<TKey,TValue>> GetEnumerator(){
            return kv.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator(){
            return ((IEnumerable)kv).GetEnumerator();
        }
    }
}
