using CodeStatistics.Input;
using System.Collections.Generic;
using CodeStatistics.Handling.Languages.Java.Elements;
using CodeStatistics.Handling.Languages.Java.Utils;
using CodeStatistics.Collections;
using System.Linq;

namespace CodeStatistics.Handling.Languages.Java{
    class JavaState{
        private readonly Dictionary<File,JavaFileInfo> fileInfo = new Dictionary<File,JavaFileInfo>();
        private readonly HashSet<string> packages = new HashSet<string>();
        private readonly Dictionary<string,int> annotationUses = new Dictionary<string,int>();

        public readonly TopElementList<TypeIdentifier> IdentifiersSimpleTop = new TopElementList<TypeIdentifier>(10,(x,y) => y.Name.Length-x.Name.Length);
        public readonly TopElementList<TypeIdentifier> IdentifiersSimpleBottom = new TopElementList<TypeIdentifier>(10,(x,y) => x.Name.Length-y.Name.Length);
        public readonly TopElementList<TypeIdentifier> IdentifiersFullTop = new TopElementList<TypeIdentifier>(10,(x,y) => y.FullName.Length-x.FullName.Length);
        public readonly TopElementList<TypeIdentifier> IdentifiersFullBottom = new TopElementList<TypeIdentifier>(10,(x,y) => x.FullName.Length-y.FullName.Length);

        public List<KeyValuePair<string,int>> AnnotationUses{
            get{
                List<KeyValuePair<string,int>> list = annotationUses.ToList();
                list.Sort((kvp1, kvp2) => kvp2.Value-kvp1.Value);
                return list;
            }
        }

        public int PackageCount { get { return packages.Count; } }

        public JavaFileInfo GetFile(File file){
            return fileInfo[file];
        }

        public JavaFileInfo Process(File file){
            JavaFileInfo info = new JavaFileInfo();
            fileInfo.Add(file,info);

            JavaCodeParser parser = new JavaCodeParser(JavaParseUtils.PrepareCodeFile(file.Contents));
            parser.AnnotationCallback += IncrementAnnotation;

            ReadPackage(parser,info);
            ReadImportList(parser,info);
            ReadTopLevelTypes(parser,info);

            UpdateLocalData(info);

            return info;
        }

        private void UpdateLocalData(JavaFileInfo info){
            packages.Add(info.Package);
        }

        private void IncrementAnnotation(Annotation annotation){
            if (annotationUses.ContainsKey(annotation.SimpleName)){
                ++annotationUses[annotation.SimpleName];
            }
            else{
                annotationUses[annotation.SimpleName] = 1;
            }
        }

        private static void ReadPackage(JavaCodeParser parser, JavaFileInfo info){
            parser.SkipSpaces();
            parser.SkipReadAnnotationList();
            parser.SkipSpaces();

            info.Package = parser.ReadPackageDeclaration();
        }

        private static void ReadImportList(JavaCodeParser parser, JavaFileInfo info){
            while(true){
                parser.SkipSpaces();

                Import? import = parser.ReadImportDeclaration();
                if (!import.HasValue)break;

                info.Imports.Add(import.Value);
            }
        }

        private static void ReadTopLevelTypes(JavaCodeParser parser, JavaFileInfo info){
            while(true){
                parser.SkipSpaces();
                if (parser.IsEOF)break;

                Type type = parser.ReadType();

                if (type != null)info.Types.Add(type);
                else break;
            }
        }
    }
}
