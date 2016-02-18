using System;

namespace CodeStatistics.Handling.Utils{
    public static class ParseUtils{
        private const StringComparison Cmp = StringComparison.Ordinal;

        /// <summary>
        /// If the string starts with searched string, it is removed and the rest is returned. If not, the original string is returned.
        /// </summary>
        public static string ExtractStart(this string me, string search){
            return me.StartsWith(search,Cmp) ? me.Substring(search.Length) : me;
        }

        /// <summary>
        /// If the string starts with searched string, it is removed, the rest is assigned to the result and the function returns true. If not, the function returns false and the result will be empty.
        /// </summary>
        public static bool ExtractStart(this string me, string search, out string result){
            if (me.StartsWith(search,Cmp)){
                result = me.Substring(search.Length);
                return true;
            }
            else{
                result = String.Empty;
                return false;
            }
        }
        
        /// <summary>
        /// If the string ends with searched string, it is removed and the rest is returned. If not, the original string is returned.
        /// </summary>
        public static string ExtractEnd(this string me, string search){
            return me.EndsWith(search,Cmp) ? me.Substring(0,me.Length-search.Length) : me;
        }

        /// <summary>
        /// If the string ends with searched string, it is removed, the rest is assigned to the result and the function returns true. If not, the function returns false and the result will be empty.
        /// </summary>
        public static bool ExtractEnd(this string me, string search, out string result){
            if (me.EndsWith(search,Cmp)){
                result = me.Substring(0,me.Length-search.Length);
                return true;
            }
            else{
                result = String.Empty;
                return false;
            }
        }
        
        /// <summary>
        /// Tries to remove searched strings from both start and end of the string and returns the rest.
        /// </summary>
        public static string ExtractBoth(this string me, string searchStart, string searchEnd){
            return me.ExtractStart(searchStart).ExtractEnd(searchEnd);
        }

        /// <summary>
        /// If the string starts and ends with searched strings, they are removed, the rest is assigned to the result and the function returns true. If not, the function returns false and the result will be empty.
        /// </summary>
        public static bool ExtractBoth(this string me, string searchStart, string searchEnd, out string result){
            if (me.StartsWith(searchStart,Cmp) && me.EndsWith(searchEnd,Cmp)){
                result = me.Substring(searchStart.Length,me.Length-searchEnd.Length-searchStart.Length);
                return true;
            }
            else{
                result = String.Empty;
                return false;
            }
        }

        /// <summary>
        /// Removes all text before the first occurrence of searched string, including the searched string
        /// </summary>
        public static string RemoveTo(this string me, string search){
            int index = me.IndexOf(search,StringComparison.Ordinal);
            return index == -1 ? me : me.Substring(index+search.Length,me.Length-index-search.Length);
        }

        /// <summary>
        /// Removes all text after the first occurrence of searched string, including the searched string
        /// </summary>
        public static string RemoveFrom(this string me, string search){
            int index = me.IndexOf(search,StringComparison.Ordinal);
            return index == -1 ? me : me.Substring(0,index);
        }
    }
}
