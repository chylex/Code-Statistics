using CodeStatistics.Handlers.Objects.Java.Enums;
using System.Collections.Generic;

namespace CodeStatistics.Handlers.Objects.Java{
    class JavaStatistics{
        public HashSet<string> Packages = new HashSet<string>();
        public HashSet<string> SimpleTypes = new HashSet<string>(); // TODO maybe useless?
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

        public int FieldsTotal = 0;
        public Dictionary<JavaModifiers.Visibility,int> FieldVisibility = new Dictionary<JavaModifiers.Visibility,int>(){
            { JavaModifiers.Visibility.Public, 0 }, { JavaModifiers.Visibility.Default, 0 }, { JavaModifiers.Visibility.Protected, 0 }, { JavaModifiers.Visibility.Private, 0 }
        };
        public Dictionary<JavaModifiers.Scope,int> FieldScope = new Dictionary<JavaModifiers.Scope,int>(){
            { JavaModifiers.Scope.Static, 0 }, { JavaModifiers.Scope.Instance, 0 }
        };
        public Dictionary<JavaModifiers.Finality,int> FieldFinality = new Dictionary<JavaModifiers.Finality,int>(){
            { JavaModifiers.Finality.Mutable, 0 }, { JavaModifiers.Finality.Final, 0 }, { JavaModifiers.Finality.Abstract, 0 }
        };

        public int MethodsTotal = 0;
        public Dictionary<JavaModifiers.Visibility,int> MethodVisibility = new Dictionary<JavaModifiers.Visibility,int>(){
            { JavaModifiers.Visibility.Public, 0 }, { JavaModifiers.Visibility.Default, 0 }, { JavaModifiers.Visibility.Protected, 0 }, { JavaModifiers.Visibility.Private, 0 }
        };
        public Dictionary<JavaModifiers.Scope,int> MethodScope = new Dictionary<JavaModifiers.Scope,int>(){
            { JavaModifiers.Scope.Static, 0 }, { JavaModifiers.Scope.Instance, 0 }
        };
        public Dictionary<JavaModifiers.Finality,int> MethodFinality = new Dictionary<JavaModifiers.Finality,int>(){
            { JavaModifiers.Finality.Mutable, 0 }, { JavaModifiers.Finality.Final, 0 }, { JavaModifiers.Finality.Abstract, 0 }
        };

        public Dictionary<JavaPrimitives,int> PrimitiveCounts = new Dictionary<JavaPrimitives,int>();

        public int SyntaxFor = 0;
        public int SyntaxForEach = 0;
        public int SyntaxWhile = 0;
        public int SyntaxDoWhile = 0;
        public int SyntaxSwitches = 0;
        public int SyntaxTry = 0;

        public Dictionary<string,JavaFileInfo> FileInfo = new Dictionary<string,JavaFileInfo>();

        public JavaStatistics(){
            foreach(JavaPrimitives primitive in JavaPrimitivesFunc.Values)PrimitiveCounts[primitive] = 0;
        }

        public JavaFileInfo CreateFileInfo(string fullFileType){
            JavaFileInfo info = new JavaFileInfo();
            return FileInfo[fullFileType] = info;
        }

        public class JavaFileInfo{
            public int Lines;
            public int Characters;
            public int Imports;
        }
    }
}
