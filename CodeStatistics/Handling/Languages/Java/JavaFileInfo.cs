using System.Collections.Generic;
using CodeStatistics.Handling.Languages.Java.Elements;

namespace CodeStatistics.Handling.Languages.Java{
    class JavaFileInfo{
        public string Package;
        public readonly HashSet<Import> Imports;

        public JavaFileInfo(){
            Package = string.Empty;
            Imports = new HashSet<Import>();
        }
    }
}
