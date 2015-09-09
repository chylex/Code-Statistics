using System.Collections.Generic;

namespace CodeStatistics.Handlers.Objects.Java{
    class JavaStatistics{
        public HashSet<string> Packages = new HashSet<string>();

        public int LinesTotal = 0;
        public long CharactersTotal = 0;
    }
}
