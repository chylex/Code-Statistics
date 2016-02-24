using CodeStatistics.Input;
using System.Collections.Generic;
using CodeStatistics.Handling.Languages.Java.Elements;
using CodeStatistics.Handling.Languages.Java.Utils;

namespace CodeStatistics.Handling.Languages.Java{
    class JavaState{
        private readonly Dictionary<File,JavaFileInfo> fileInfo = new Dictionary<File,JavaFileInfo>();
        private readonly HashSet<string> packages = new HashSet<string>();

        public int PackageCount { get { return packages.Count; } }

        public JavaFileInfo GetFile(File file){
            return fileInfo[file];
        }

        public JavaFileInfo Process(File file){
            JavaFileInfo info = new JavaFileInfo();
            fileInfo.Add(file,info);

            JavaCodeParser parser = new JavaCodeParser(JavaParseUtils.PrepareCodeFile(file.Contents));

            ReadPackage(parser,info);
            ReadImportList(parser,info);
            ReadTopLevelTypes(parser,info);

            UpdateLocalData(info);

            return info;
        }

        private void UpdateLocalData(JavaFileInfo info){
            packages.Add(info.Package);
        }

        private static void ReadPackage(JavaCodeParser parser, JavaFileInfo info){
            parser.SkipSpaces();

            List<Annotation> annotations = parser.SkipReadAnnotationList();
            // TODO handle annotation list
            
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
