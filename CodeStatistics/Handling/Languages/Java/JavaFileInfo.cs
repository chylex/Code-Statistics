using System.Collections.Generic;

namespace CodeStatistics.Handling.Languages.Java{
    class JavaFileInfo{
        public string Package;
        public readonly HashSet<string> Imports;

        public JavaFileInfo(){
            Package = string.Empty;
            Imports = new HashSet<string>();
        }
    }
}
