using System;
using System.Collections.Generic;

namespace CodeStatistics.Collections{
    public static class EnumDictionary{
        /// <summary>
        /// Creates a Dictionary&lt;Enum,TValue&gt; and prefills it with <paramref name="initializeTo"/> assigned to each element of the Enum.
        /// </summary>
        public static Dictionary<TKey,TValue> Create<TKey,TValue>(TValue initializeTo = default(TValue)) where TKey : struct{
            Array values = Enum.GetValues(typeof(TKey));
            Dictionary<TKey,TValue> dict = new Dictionary<TKey,TValue>(values.Length);

            foreach(object value in values){
                dict[(TKey)value] = initializeTo;
            }

            return dict;
        }
    }
}
