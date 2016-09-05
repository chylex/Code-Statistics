using System;
using System.Collections;
using System.Collections.Generic;

namespace CodeStatisticsCore.Collections {
    public class TopElementList<T> : IList<T>{
        private readonly List<T> internalList;
        private readonly Comparison<T> sorter;
        private readonly int maxSize;

        public int Count { get { return internalList.Count; } }
        public bool IsReadOnly { get { return false; } }

        public T this[int index]{
            get { return internalList[index]; }
            set { Add(value); } // ignore index
        }

        public TopElementList(int maxSize, Comparison<T> sorter){
            this.internalList = new List<T>(maxSize+1);
            this.sorter = sorter;
            this.maxSize = maxSize;
        }

        public void Add(T item){
            if (internalList.Count == 0){
                internalList.Add(item);
            }
            else if (sorter(item, internalList[internalList.Count-1]) <= 0){ // replace values that are smaller/equal than the new value
                internalList.Add(item);
                internalList.Sort(sorter);

                if (internalList.Count > maxSize){
                    internalList.RemoveAt(internalList.Count-1);
                }
            }
        }

        public void Insert(int index, T item){ // ignore index
            Add(item);
        }

        public void Clear(){
            internalList.Clear();
        }

        public bool Contains(T item){
            return internalList.Contains(item);
        }

        public int IndexOf(T item){
            return internalList.IndexOf(item);
        }

        public void CopyTo(T[] array, int arrayIndex){
            internalList.CopyTo(array, arrayIndex);
        }

        public bool Remove(T item){
            return internalList.Remove(item);
        }

        public void RemoveAt(int index){
            internalList.RemoveAt(index);
        }

        public IEnumerator<T> GetEnumerator(){
            return internalList.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator(){
            return internalList.GetEnumerator();
        }
    }
}
