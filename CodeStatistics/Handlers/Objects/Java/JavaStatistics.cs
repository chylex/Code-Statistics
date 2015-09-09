using System;
using System.Collections.Generic;

namespace CodeStatistics.Handlers.Objects.Java{
    class JavaStatistics{
        public HashSet<string> Packages = new HashSet<string>();
        public HashSet<string> SimpleTypes = new HashSet<string>();
        public HashSet<string> FullTypes = new HashSet<string>();

        public Dictionary<JavaType,short> TypeFileCounts = new Dictionary<JavaType,short>(){
            { JavaType.Class, 0 }, { JavaType.Interface, 0 }, { JavaType.Enum, 0 }, { JavaType.Annotation, 0 }
        };

        public Dictionary<JavaType,short> TypeCounts = new Dictionary<JavaType,short>(){
            { JavaType.Class, 0 }, { JavaType.Interface, 0 }, { JavaType.Enum, 0 }, { JavaType.Annotation, 0 }
        };

        public int LinesTotal = 0;
        public long CharactersTotal = 0;
        public int ImportsTotal = 0;

        public Dictionary<string,JavaFileInfo> FileInfo = new Dictionary<string,JavaFileInfo>();

        public JavaFileInfo CreateFileInfo(string fullFileType){
            JavaFileInfo info = new JavaFileInfo();
            FileInfo.Add(fullFileType,info);
            return info;
        }

        public class JavaFileInfo{
            public int Lines;
            public int Characters;
            public int Imports;
        }
    }
}
