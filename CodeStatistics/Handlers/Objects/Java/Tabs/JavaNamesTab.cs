using System.Collections.Generic;
using System.Linq;

namespace CodeStatistics.Handlers.Objects.Java.Tabs{
    class JavaNamesTab : JavaTab{
        private const int ListSize = 5;

        private readonly List<KeyValuePair<string,string>> LongestSimpleNames = new List<KeyValuePair<string,string>>(ListSize);
        private readonly List<KeyValuePair<string,string>> ShortestSimpleNames = new List<KeyValuePair<string,string>>(ListSize);
        private readonly List<string> LongestFullNames = new List<string>(ListSize);
        private readonly List<string> ShortestFullNames = new List<string>(ListSize);

        public JavaNamesTab(JavaStatistics stats) : base("Names",stats){
            IEnumerable<KeyValuePair<string,string>> convertedNames = stats.FullTypes.Select(name => JavaParseUtils.GetSimpleName(name));

            LongestSimpleNames.AddRange(convertedNames.OrderByDescending(kvp => kvp.Key.Length).Take(ListSize));
            ShortestSimpleNames.AddRange(convertedNames.OrderBy(kvp => kvp.Key.Length).Take(ListSize));

            LongestFullNames.AddRange(stats.FullTypes.OrderByDescending(name => name.Length).Take(ListSize));
            ShortestFullNames.AddRange(stats.FullTypes.OrderBy(name => name.Length).Take(ListSize));
        }
    }
}
