using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace CodeStatisticsCore.Collections{
    public class CounterDictionary<TKey> : IEnumerable<KeyValuePair<TKey,int>>{
        private readonly Dictionary<TKey,int> innerDict;

        public int Count { get { return innerDict.Count; } }

        public CounterDictionary(){
            innerDict = new Dictionary<TKey,int>();
        }

        public CounterDictionary(int capacity){
            innerDict = new Dictionary<TKey,int>(capacity);
        }

        public void Increment(TKey key){
            if (innerDict.ContainsKey(key)){
                ++innerDict[key];
            }
            else{
                innerDict[key] = 1;
            }
        }

        public List<KeyValuePair<TKey,int>> ListFromTop(){
            List<KeyValuePair<TKey,int>> list = innerDict.ToList();
            list.Sort((kvp1, kvp2) => kvp2.Value-kvp1.Value);
            return list;
        }

        public IEnumerator<KeyValuePair<TKey,int>> GetEnumerator(){
            return innerDict.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator(){
            return innerDict.GetEnumerator();
        }
    }
}
