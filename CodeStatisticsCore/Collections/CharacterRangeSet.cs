using System.Collections;
using System.Collections.Generic;

namespace CodeStatisticsCore.Collections{
    public class CharacterRangeSet : IEnumerable<KeyValuePair<int,int>>{
        private readonly List<KeyValuePair<int,int>> innerList = new List<KeyValuePair<int,int>>();

        public void Add(int lowerBound, int upperBound){
            innerList.Add(new KeyValuePair<int,int>(lowerBound,upperBound));
        }

        public bool Contains(int character){
            foreach(KeyValuePair<int,int> kvp in innerList){
                if (character < kvp.Key)return false; // all following KVPs are higher
                if (character >= kvp.Key && character <= kvp.Value)return true;
            }

            return false;
        }

        public IEnumerator<KeyValuePair<int,int>> GetEnumerator(){
            return innerList.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator(){
            return innerList.GetEnumerator();
        }
    }
}
