using System.Collections.Generic;
using LanguageJava.Elements;

namespace LanguageJava.Handling{
    public class JavaFileInfo{
        public string Package;
        public readonly HashSet<Import> Imports;
        public readonly List<Type> Types;

        public JavaFileInfo(){
            Package = string.Empty;
            Imports = new HashSet<Import>();
            Types = new List<Type>();
        }
    }
}
