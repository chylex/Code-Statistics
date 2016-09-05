namespace CodeStatisticsCore.Handling.Utils{
    public static class StringUtils{
        /// <summary>
        /// Capitalizes the first character of a string and leaves the rest intact.
        /// </summary>
        public static string CapitalizeFirst(this string me){
            return me.Length == 0 ? string.Empty : me.Length == 1 ? me.ToUpperInvariant() : me.Substring(0, 1).ToUpperInvariant()+me.Substring(1);
        }
    }
}
